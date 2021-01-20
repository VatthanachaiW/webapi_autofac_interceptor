using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace BookStore.API.Loggers
{
  public class LoggerInterceptor : IInterceptor
  {
    private readonly ILogger _log;

    public LoggerInterceptor(ILogger log)
    {
      _log = log;
    }

    public void Intercept(IInvocation invocation)
    {
      var traceId = Guid.NewGuid().ToString();
      var name = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
      var target = $"{(invocation.InvocationTarget ?? "-")}";
      var args = JsonConvert.SerializeObject(invocation.Arguments, Formatting.None, new JsonSerializerSettings
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
        PreserveReferencesHandling = PreserveReferencesHandling.Objects
      });

      try
      {
        log4net.LogicalThreadContext.Properties["ClassName"] = invocation.Method.DeclaringType;
        log4net.LogicalThreadContext.Properties["MethodName"] = name;
        log4net.LogicalThreadContext.Properties["Request"] = args;

        _log.Info($"Args: {args} - method name:  {name} - Target: {target}");

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
              log4net.LogicalThreadContext.Properties["Response"] = resultJson;
              _log.Info($"Result: {resultJson} - method name:  {name} - Target: {target}");
            }
          }
        }
      }
      catch (FaultException ex)
      {
        _log.Error(ex,
          $"{traceId} - Args: {args} - After Invoke Exception {name} - {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw;
      }
    }
  }
}