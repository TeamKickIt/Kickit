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
        public ViewResult Index()
        {
      
            return View();
        }

        //CONTACT VIEW
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(
            [Bind(Include = "FromName, FromEmail, ReceiverName, ReceiverEmail, DateTime1, DateTime2, DateTime3")] Invitor invitor)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();

                Invitor invite = dbContext.Invitors.Add(invitor);
                dbContext.SaveChanges();
                return View("Sent", invitor);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }


        //View after invite has been sent
        public ActionResult Sent()
        {
            return View();
        }


        // GET /RecepientForm/17
        [HttpGet]
        public ViewResult RecepientForm(int invite)
        {
           //fill in what needs to display on form
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