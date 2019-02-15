using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Log
{
    public class Logger4net : ILog
    {
        public log4net.ILog logger;

        public Logger4net(log4net.ILog logger)
        {
            this.logger = logger;
        }

        public bool IsDebugEnabled
        {
            get
            {
                return logger.IsDebugEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return logger.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return logger.IsWarnEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return logger.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return logger.IsFatalEnabled;
            }
        }

        public bool IsAllowOutput
        {
            get
            {
                return this.IsInfoEnabled;
            }
        }

        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Debug(object message)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        public void Debug(string msgFormat, params object[] args)
        {
            if (IsDebugEnabled)
            {
                logger.DebugFormat(msgFormat, args);
            }
        }

        public void Error(object message)
        {
            if (IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        public void Error(string msgFormat, params object[] args)
        {
            if (IsErrorEnabled)
            {
                logger.ErrorFormat(msgFormat, args);
            }
        }

        public void Fatal(object message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string msgFormat, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            if (IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        public void Info(string msgFormat, params object[] args)
        {
            if (IsInfoEnabled)
            {
                logger.InfoFormat(msgFormat, args);
            }
        }

        public void Warn(object message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string msgFormat, params object[] args)
        {
            throw new NotImplementedException();
        }

        private string tryFormart(string s, params object[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    return s;
                return string.Format(s, args);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
