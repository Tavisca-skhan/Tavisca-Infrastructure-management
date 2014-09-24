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
    public class ExceptionLogger
    {
        static string logFilePath = ConfigurationManager.AppSettings["elogFilePath"];
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

