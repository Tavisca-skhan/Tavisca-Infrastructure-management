using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
namespace EmailSender
{
   public class EmailBody
    {
        public string GetAssignBody()
        {
            string AssignHtmlFilePath = ConfigurationManager.AppSettings["AssignHtmlFilePath"];
            string assign = File.ReadAllText(AssignHtmlFilePath);
            return assign;
        }
        public string GetExpireBody()
        {
            string ExpireHtmlFilePath = ConfigurationManager.AppSettings["ExpireHtmlFilePath"];
            string expire = File.ReadAllText(ExpireHtmlFilePath);
            return expire;
        }
        public string GetUnAssignBody()
        {
            string UnAssignHtmlFilePath = ConfigurationManager.AppSettings["UNAssignHtmlFilePath"];
            string unassign = File.ReadAllText(UnAssignHtmlFilePath);
            return unassign;
        }

    }
}
