using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickit.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

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
               SendSimpleMessage(invitor);
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
        //Method to call mailgun

        public static IRestResponse SendSimpleMessage(Invitor invitor)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-8b65bd539d243a3e8c4a03a5c16e6f4b");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "sandbox90b6af4c9c9f40eaa4ba0073541a6975.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox90b6af4c9c9f40eaa4ba0073541a6975.mailgun.org>");
            request.AddParameter("to", "Teamkickitapp <teamkickitapp@gmail.com>");// This is receiver mail.can change this mail id after add and activate in mailgun account Receipient mail list 
            request.AddParameter("subject", "Hello Teamkickitapp");
            request.AddParameter("text", $"Hi {invitor.ReceiverName} you are invited by {invitor.FromName} .Click this link  to respond  http://kickitapp.azurewebsites.net/Home/RecepientForm/?id=");//This is message sent to receiver
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}