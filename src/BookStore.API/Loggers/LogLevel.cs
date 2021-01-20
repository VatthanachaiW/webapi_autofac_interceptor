using System;
using System.Runtime.Serialization;

namespace BookStore.API.Loggers
{
  [Serializable]
  [DataContract]
  public enum LogLevel : byte
  {
    Verbose = 1,
    Debug,
    Trace,
    Information,
    Warning,
    Error,
    Critical
  }
}