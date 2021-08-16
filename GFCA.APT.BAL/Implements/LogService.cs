using GFCA.APT.BAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
    public class LogService : ILogService
    {
        private readonly ILog _log;

        public static LogService CreateInstant()
        {
            var svc = new LogService();
            return svc;
        }

        public LogService()
        {
            _log = LogManager.GetLogger("MONITOR");
        }

        #region [ Debug ]
        public void Debug(string message)
        {
            _log.Debug(message);
        }
        public void Debug(string message, Exception exception)
        {
            _log.Debug(message, exception);
        }
        #endregion [ Debug ]
        #region [ Error ]
        public void Error(string message)
        {
            _log.Error(message);
        }
        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }
        #endregion [ Error ]
        #region [ Fatal ]
        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
        #endregion [ Fatal ]
        #region [ Error ]
        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            _log.Info(message, exception);
        }
        #endregion [ Error ]
        #region [ Warn ]
        public void Warn(string message)
        {
            _log.Warn(message);
        }
        public void Warn(string message, Exception exception)
        {
            _log.Warn(message, exception);
        }
        #endregion [ Warn ]
    }
}
