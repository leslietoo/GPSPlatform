using Microsoft.Extensions.Logging;
using SuperSocket.SocketBase.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class SuperSocketNLogExtensions : ILog
    {
        private ILogger logger;

        public SuperSocketNLogExtensions(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException("logger");
        }

        public bool IsDebugEnabled => logger.IsEnabled(LogLevel.Debug);

        public bool IsErrorEnabled => logger.IsEnabled(LogLevel.Error);
        [Obsolete("未实现")]
        public bool IsFatalEnabled => logger.IsEnabled(LogLevel.Critical);

        public bool IsInfoEnabled => logger.IsEnabled(LogLevel.Information);

        public bool IsWarnEnabled => logger.IsEnabled(LogLevel.Warning);

        public void Debug(object message)
        {
            logger.LogDebug(message.ToString());
        }

        public void Debug(object message, Exception exception)
        {
            logger.LogDebug(exception, message.ToString());
        }

        public void DebugFormat(string format, object arg0)
        {
            logger.LogDebug(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            logger.LogDebug(format, args);
        }

        [Obsolete("未实现")]
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        [Obsolete("未实现")]
        public void DebugFormat(string format, object arg0, object arg1)
        {
            logger.LogDebug(format, arg0,arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.LogDebug(format, arg0, arg1, arg2);
        }

        public void Error(object message)
        {
            logger.LogError(message.ToString());
        }

        public void Error(object message, Exception exception)
        {
            logger.LogError(exception,message.ToString());
        }

        public void ErrorFormat(string format, object arg0)
        {
            logger.LogError(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            logger.LogError(format, args);
        }

        [Obsolete("未实现")]
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            logger.LogError(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.LogError(format, arg0, arg1, arg2);
        }

        [Obsolete("未实现")]
        public void Fatal(object message)
        {
            throw new NotImplementedException();
        }

        [Obsolete("未实现")]
        public void Fatal(object message, Exception exception)
        {
            throw new NotImplementedException();
        }
        [Obsolete("未实现")]
        public void FatalFormat(string format, object arg0)
        {
            throw new NotImplementedException();
        }
        [Obsolete("未实现")]
        public void FatalFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }
        [Obsolete("未实现")]
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }
        [Obsolete("未实现")]
        public void FatalFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }
        [Obsolete("未实现")]
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            logger.LogInformation(message.ToString());
        }

        public void Info(object message, Exception exception)
        {
            logger.LogInformation(exception,message.ToString());
        }

        public void InfoFormat(string format, object arg0)
        {
            logger.LogInformation(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            logger.LogInformation(format, args);
        }

        [Obsolete("未实现")]
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            logger.LogInformation(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.LogInformation(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            logger.LogWarning(message.ToString());
        }

        public void Warn(object message, Exception exception)
        {
            logger.LogWarning(exception,message.ToString());
        }

        public void WarnFormat(string format, object arg0)
        {
            logger.LogWarning(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            logger.LogWarning(format, args);
        }

        [Obsolete("未实现")]
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            logger.LogWarning(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.LogWarning(format, arg0, arg1, arg2);
        }
    }
}
