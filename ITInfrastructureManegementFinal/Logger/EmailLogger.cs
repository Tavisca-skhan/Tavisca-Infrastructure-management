using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using ItemObjects;

namespace Logger
{
   public class EmailLogger
    {

       static string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
        public static void LogFile(string sendBy, string sendTo, string messageType, string status)
        {
            StreamWriter log;
            if (!File.Exists(logFilePath))
            {
                log = new StreamWriter(logFilePath,true);

            }
            else
            {
                log = File.AppendText(logFilePath);
            }
            log.WriteLine(string.Format(ConstantMessages.emailSendLogFileMessage, DateTime.Now, sendBy, sendTo, messageType, status));
            log.Close();
        }
        public static void LogFile(string message)
        {
            StreamWriter log;
            if (!File.Exists(logFilePath))
            {
                log = new StreamWriter(logFilePath);

            }
            else
            {
                log = File.AppendText(logFilePath);
            }
            log.WriteLine(string.Format(ConstantMessages.exceptionLogFileMessage, DateTime.Now, message));
            log.Close();
        }
    }
}
