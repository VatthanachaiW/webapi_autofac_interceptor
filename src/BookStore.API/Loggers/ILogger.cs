using System;

namespace BookStore.API.Loggers
{
  public interface ILogger
  {
    void Info(string message);

    void Warning(string message);

    void Error(Exception e);
    void Error(Exception e, string message);
  }
}