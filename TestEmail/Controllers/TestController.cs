using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TestEmail.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult SideMenu()
        //{
        //    return View();
        //}
        //public JsonResult SaveList(string ItemList)
        //{
        //    string[] arr = ItemList.Split(',');
        //    foreach (var id in arr)
        //    {
        //        var currentId = id;
        //    }

        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult GetSuggestion(string text)
        //{
        //    List<MyShop>
        //}

        public JsonResult SendEmailToUser()
        {
            bool result = false;
            result = SendEmail("nirmalaa0606@gmail.com", "[NOTIFICATION] Leave Request From Employee", "<p>Dear Manager,<br />Please Evaluate your Employee Leave Request<br /><br />Regards <br /> Leave Request System</p>");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}