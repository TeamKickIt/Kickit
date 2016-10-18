using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Kickit.API
{
    public class EventBrite
    {
        public List<EventBriteEvent> Search(string zipCode)
        {
            string html = string.Empty;
            string url =
                @"https://www.eventbriteapi.com/v3/events/search/?location.address=" + zipCode +
                "&token=CV6HX5TNIPF76F3VLGFC";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            JObject eventbritejson = JObject.Parse(html);
            JArray eventsArray = (JArray) eventbritejson["events"];
            List<EventBriteEvent> eventList = new List<EventBriteEvent>();
            foreach (JToken detail in eventsArray)
            {
                string name = (string) detail["name"]["text"];
                string description = (string) detail["description"]["text"];
                DateTime startTime = (DateTime) detail["start"]["local"];
                DateTime endtime = (DateTime) detail["end"]["local"];
                string eventUrl = (string) detail["url"];
                eventList.Add(new EventBriteEvent(name, description, startTime, endtime, eventUrl));
            }


            return eventList;
        }

    }
    }