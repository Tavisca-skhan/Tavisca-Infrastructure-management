using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemObjects;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace EmailSender
{
    public class EmailSend
    {
        static void Main(string[] args)
        {
           // BusinessLogicLayer.Helper ce = new BusinessLogicLayer.Helper();
            //ce.CheckExpiry();
            EmailSend p = new EmailSend();
           p.SendMail("skhan@tavisca.com", true);

            p.SendMailExpiry();
            Console.ReadKey();
            
        }
        public void SendMailExpiry()
        {
            

                XElement xmlDoc = XElement.Load(ConfigurationManager.AppSettings["productFilePath"]);
                var items = from item in xmlDoc.Descendants("Item")
                            where item.Element("IsAssigned").Value == "True" && IsExpire(Convert.ToDateTime(item.Element("DateOfExpiryOfAssignment").Value))
                            select item.Element("AssignedTo").Value;

            // && i.Element("Brand").Value == itemBrand && i.Element("UniqueID").Value == itemId
            //if (items != null)
           // {
                foreach(var item in items)
                {
                    string email = GetEmail(item);
                   // Console.WriteLine(email);
                   // Console.ReadKey();
                   
                    SendMail(email, true);
                }
            //}
            //else
            //{
            // }


        }
        public bool IsExpire(DateTime date)
        {

            DateTime expiryDate = date;
            DateTime newDate = DateTime.Now;

            // Difference in days, hours, and minutes.
            TimeSpan ts = expiryDate - newDate;
            // Difference in days.
            int differenceInDays = ts.Days;
            if (differenceInDays == 1)
                return true;
            else
                return false;
        }
        public string GetEmail(string empid)
        {

            string Email = "";
            XElement xmlDoc = XElement.Load(ConfigurationManager.AppSettings["employeeFilePath"]);
            var items = from i in xmlDoc.Descendants("Employee")
                        where i.Element("EmployeeId").Value == empid
                        select i.Element("EMailAddr").Value;
            foreach (var item in items)
            {
                Email = item;
            }

            return Email;
        }

        public void SendMail(string Emailid,bool isexpiry)
        {
           
           string sender = ConfigurationManager.AppSettings["sender"];
             string password=  ConfigurationManager.AppSettings["password"];
            
            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["client"],587);
                Console.WriteLine(ConfigurationManager.AppSettings["client"]);
                //Console.ReadKey();
                client.EnableSsl = true;
                client.Timeout = 20000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(sender,password);
                MailMessage msg = new MailMessage();
               
                msg.To.Add(Emailid);
                msg.From = new MailAddress(ConfigurationManager.AppSettings["sender"]);

              
                msg.Subject = "Email from Tavisca IT Infrastuctue....";
                EmailBody eb = new EmailBody();
                msg.IsBodyHtml = true;
                if (isexpiry)
                {
                    msg.Body = @eb.GetExpireBody();
                   
                }
                else
                    msg.Body = @eb.GetAssignBody();

                
                client.Send(msg);
             

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
