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
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public ActionResult NewFormInDb(Invitor invitor)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Invitor model)
        {
            if (ModelState.IsValid)
            {
                Invitor email = new Invitor();
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();

                message.To.Add(new MailAddress(model.ReceiverEmail)); //replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, "http://localhost:50941/Home/Contact");// model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                     
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
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
        public ViewResult RecepientForm(MovieTimes movietimes)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", movietimes);
            }
            else
            {
                return View("Sorry");
            }
        }
    }
}