using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Config;

namespace BookStore.API.Loggers
{
  public class Log4NetAdapter : ILogger
  {
    private readonly Func<Process> _getCurrentProcess = Process.GetCurrentProcess;
    private readonly ILog _log;

    public Log4NetAdapter()
    {
      var log4NetConfig = new XmlDocument();
      log4NetConfig.Load(File.OpenRead("log4net.config"));
      var repo = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
      XmlConfigurator.Configure(repo, log4NetConfig["log4net"]);
      _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      _log.Info("Log start");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Verbose:
        case LogLevel.Debug:
          return _log.IsDebugEnabled;
        case LogLevel.Information:
          return _log.IsInfoEnabled;
        case LogLevel.Warning:
          return _log.IsWarnEnabled;
        case LogLevel.Error:
          return _log.IsErrorEnabled;
        case LogLevel.Critical:
          return _log.IsFatalEnabled;
        default:
          throw new ArgumentException($"Unknown log level {logLevel}.", nameof(logLevel));
      }
    }

    public void Log(LogLevel logLevel, int eventId, object state, Exception exception,
      Func<object, Exception, string> formatter)
    {
      if (!IsEnabled(logLevel)) return;

      if (formatter == null) throw new ArgumentNullException(nameof(formatter));
      string message = formatter(state, exception);
      if (!string.IsNullOrEmpty(message) || exception != null)
        switch (logLevel)
        {
          case LogLevel.Critical:
            _log.Fatal(message);
            break;
          case LogLevel.Debug:
          case LogLevel.Trace:
            _log.Debug(message);
            break;
          case LogLevel.Error:
            _log.Error(message);
            break;
          case LogLevel.Information:
            _log.Info(message);
            break;
          case LogLevel.Warning:
            _log.Warn(message);
            break;
          default:
            _log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
            _log.Info(message, exception);
            break;
        }
    }

    public void Info(string message)
    {
      Log(LogLevel.Information, _getCurrentProcess().Id, null, null, (s, e) => message);
    }

    public void Warning(string message)
    {
      Log(LogLevel.Warning, _getCurrentProcess().Id, null, null, (s, e) => message);
    }

    public void Error(Exception e)
    {
      Log(LogLevel.Error, _getCurrentProcess().Id, null, e, (s, ex) => ex.Message);
    }

    public void Error(Exception e, string message)
    {
      Log(LogLevel.Error, _getCurrentProcess().Id, null, e, (s, ex) => message);
    }
  }
}