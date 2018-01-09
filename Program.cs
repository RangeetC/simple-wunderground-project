using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wunderground_API_Project
{

    class Program
    {
        static void Main(string[] args)
        {
            int ErrorCode = 0;
            WeatherClass weatherObject;
            // The following block of code is used to request the .json file and subsequently parse and store it.
            try
            {
                WebRequest request = WebRequest.Create(@"http://api.wunderground.com/api/XXXXXXXXX/conditions/q/IN/Kolkata.json"); // An API key has been censored here.
                request.ContentType = "application/json; charset=utf-8";
                string responseString;

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            responseString = streamReader.ReadToEnd();
                        }
                    }
                }
                JObject jObject = JObject.Parse(responseString);
                weatherObject = new WeatherClass(jObject);
            }

            //A WebException occurs if there is a problem with the API; for example, a corrupt json file is returned.
            catch (WebException webException)
            {
                weatherObject = null;
                ErrorCode = 1;
            }
            //An Exception implies a more general problem; for example, there is no internet access.
            catch (Exception exception)
            {
                weatherObject = null;
                ErrorCode = 2;
            }

            IOHelpers.WriteWeatherObject(weatherObject, ErrorCode);

        }

    }

}

        
        

       

    

            
