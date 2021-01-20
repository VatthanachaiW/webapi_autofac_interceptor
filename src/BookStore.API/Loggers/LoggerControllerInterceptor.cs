using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace BookStore.API.Loggers
{
  public class LoggerControllerInterceptor : IInterceptor
  {
    private ILogger _log;
    private readonly string _guid;
    private string _guidShort;

    private string GuidShort
    {
      get { return _guidShort ??= _guid.Substring(0, 8); }
    }

    public LoggerControllerInterceptor(ILogger log)
    {
      _log = log;
      _guid = Guid.NewGuid().ToString();
    }

    private void LogStart()
    {
      _log.Info($"================ Begin Request {_guid} ================ >>> {nameof(LoggerControllerInterceptor)}");
    }

    private void LogEnd()
    {
      _log.Info($"================ End Request {_guid} ================ >>> {nameof(LoggerControllerInterceptor)}");
    }

    public void Intercept(IInvocation invocation)
    {
      var genericName = invocation.GenericArguments != null && invocation.GenericArguments.Length > 0 ? invocation.GenericArguments[0].Name : string.Empty;
      var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}{(string.IsNullOrWhiteSpace(genericName) ? string.Empty : $"<{genericName}>")}";
      var target = $"{(invocation.InvocationTarget ?? "-")}";
      var args = JsonConvert.SerializeObject(
        invocation.Arguments,
        Formatting.Indented,
        new JsonSerializerSettings
        {
          ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
          PreserveReferencesHandling = PreserveReferencesHandling.Objects
        });

      //var message = $"Args: {args} - method name:  {name} - Target: {target}";

      _log.Info($"({GuidShort}) Request args: {args} - method name:  {name} - Target: {target}");

      try
      {
        invocation.Proceed();

        if (invocation.ReturnValue is Task task)
        {
          Task.Run(async () => await task.ConfigureAwait(false));

          var result = task.GetType().GetProperty(nameof(Task<object>.Result))?.GetValue(task);
          if (result != null)
          {
            var resultJson = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
            {
              ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
              PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            if (string.IsNullOrWhiteSpace(resultJson))
            {
              _log.Info($"({GuidShort}) Result: {resultJson} - method name:  {name} - Target: {target}");
            }
          }
        }
      }
      catch (FormatException ex)
      {
        _log.Error(ex, $"Args: {args} - After Invoking Exception {name} - {ex.Message} - Target: {target}");
        throw new Exception(ex.Message);
      }
      catch (Exception ex)
      {
        _log.Error(ex, $"Args: {args} - After Invoking Exception {name} - {ex.Message} - Target: {target}");
        throw;
      }
    }
  }
}