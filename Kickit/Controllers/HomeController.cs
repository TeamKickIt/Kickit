using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickit.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Kickit.API;
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
        public ViewResult RecepientForm(int id)


        {


            ApplicationDbContext dbContext = new ApplicationDbContext();

            var invitordetail = dbContext.Invitors.SingleOrDefault(i => i.Id == id);

            ViewBag.fromName = invitordetail.FromName; //Datas are passed to responseform
            ViewBag.receiverName = invitordetail.ReceiverName;
            ViewBag.date1 = invitordetail.DateTime1;
            ViewBag.date2 = invitordetail.DateTime2;
            ViewBag.date3 = invitordetail.DateTime3;

            //InvitorRecepientModel invitorecipientmodel = new InvitorRecepientModel();



            //fill in what needs to display on form
            return View();
        }

        [HttpPost]
        public ViewResult RecepientForm(InvitorRecepientModel responsemodel)
        {
          //  if (responsemodel.recepientform.DateTime1==null|| responsemodel.recepientform.DateTime1 == null|| responsemodel.recepientform.DateTime1 == null)
         bool Option1 = responsemodel.recepientform.DateTime1;
            bool Option2 = responsemodel.recepientform.DateTime2;
           bool Option3 = responsemodel.recepientform.DateTime3;
           // ViewBag.receiverName = responsemodel.invitor.ReceiverName;

            if (Option1 || Option2 || Option3 == true)
            //if (ModelState.IsValid)
            {
                var api = new EventBrite();
                List<EventBriteEvent> eventlist = api.Search("48026");

                return View("EventBriteAPI", eventlist);
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
            request.AddParameter("to", "Gaby <teamkickitapp@gmail.com>");// This is receiver mail.can change this mail id after add and activate in mailgun account Receipient mail list 
            request.AddParameter("subject", "Hello Teamkickitapp");
            request.AddParameter("text", $"Hi {invitor.ReceiverName} you are invited by {invitor.FromName} .Click this link  to :http://kickitapp.azurewebsites.net/Home/RecepientForm/?id={invitor.Id}");//This is message sent to receiver
            request.Method = Method.POST;
            return client.Execute(request);
        }

    }
}
