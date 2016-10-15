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
        public ActionResult Contact([Bind(Include = "FromName, FromEmail, ReceiverName, ReceiverEmail, DateTime1, DateTime2, DateTime3")]Invitor invitor)
        {

           // SendSimpleMessage(invitor);
            if (ModelState.IsValid)
            {
                SendSimpleMessage(invitor);
              }
        
            return View("Sent",invitor);
        }

        public static IRestResponse SendSimpleMessage(Invitor invitor)
        {
           

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-f8c4d4300117a2840bbfac000f5bed93");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "postmaster@sandbox525b6e75dca34f5cb690b927e0bdf28b.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            //request.AddParameter("from", "Mailgun Sandbox <http://kickitapp.azurewebsites.net>");
             request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox525b6e75dca34f5cb690b927e0bdf28b.mailgun.org>");
            request.AddParameter("to", "Kickit <athikumar72@gmail.com>");            
            request.AddParameter("subject", $"Hello {invitor.FromName}");
             request.AddParameter("subject", "Hello Athikumar");
            request.AddParameter("text", $"Congratulations {invitor.FromName}  invited you .Click on link to reply : http://localhost:50941/Home/RecepeientForm/?{invitor.Id} ");
            request.Method = Method.POST;
            return client.Execute(request);
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