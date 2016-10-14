using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickit.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Kickit.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind (Include = "FromName, FromEmail, ReceiverName, ReceiverEmail, DateTime1, DateTime2, DateTime3")]Invitor invitor)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();

                dbContext.Invitors.Add(invitor);
                dbContext.SaveChanges();

                Invitor email = new Invitor();
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();

                message.To.Add(new MailAddress(invitor.ReceiverEmail)); //replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, invitor.FromName, invitor.FromEmail, "http://localhost:50941/Home/Contact");// model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                     
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(invitor);
        }



        public ActionResult Sent()
        {
            return View();
        }


        [HttpGet]
        public ViewResult RecepientForm()
        {
           
            return View();
        }

        [HttpPost]
        public ViewResult RecepientForm(InvitorRecepientModel responsemodel)
        {
            bool Option1 = responsemodel.recepientform.DateTime1;
            bool Option2 = responsemodel.recepientform.DateTime2;
            bool Option3 = responsemodel.recepientform.DateTime3;


            if (ModelState.IsValid)
            {
                return View("Thanks", responsemodel);
            }
            else
            {
                return View("Sorry");
            }
        }
    }
}