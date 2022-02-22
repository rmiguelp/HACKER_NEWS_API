using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace HACKER__NEWS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("best20")]
        public string app_endpoint_best20()
        {
            try
            {
                List<string> jSONsourceBestStories = new List<string>();
                string sResult = "";
                IEnumerable<string> listDataItemsBestStories;
                int numberOfTopResults = 0;
                DataItems clsDataItems = new DataItems();               

                var url = "https://hacker-news.firebaseio.com/v0/beststories.json";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Accept = "application/json";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    sResult = streamReader.ReadToEnd();
                    jSONsourceBestStories = JsonConvert.DeserializeObject<List<string>>(sResult);
                    numberOfTopResults = jSONsourceBestStories.Count;

                    listDataItemsBestStories = (from item in jSONsourceBestStories orderby item descending select item).Take(numberOfTopResults);
                }
                
                if (httpResponse.StatusDescription != "OK")
                { throw new Exception("HttpResponse.StatusDescription returned a NOK"); }

                foreach (string s in listDataItemsBestStories)
                {
                    url = String.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty", s);
                    httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Accept = "application/json";
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        sResult = streamReader.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<DataItem>(sResult);
                        clsDataItems._DataItems.Add(result);
                    }                    

                    if (httpResponse.StatusDescription != "OK")
                    { throw new Exception("HttpResponse.StatusDescription returned a NOK"); }
                }

                var topScoredList = (from item in clsDataItems._DataItems orderby item.score descending select item).Take(20);                
                return JsonConvert.SerializeObject(topScoredList, Formatting.Indented);
            }
            catch (Exception ex)
            {                
                return string.Format("* An error has occurred *\n\nMessage:\n{0}\n\nStackTrace:\n{1}", ex.Message, ex.StackTrace);
            }            
        }        
    }
}
