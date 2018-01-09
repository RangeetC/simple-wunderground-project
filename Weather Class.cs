using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wunderground_API_Project
{
    // This class is used to store all the information from the json object. 

    public class WeatherClass
    {      
     
        public int Temperature { get; set; }
        public string RelativeHumidity { get; set; }
        public int FeelsLike { get; set; }
        public string Weather { get; set; }
        public string ObservationTime { get; set; }


        public WeatherClass(JObject jObject)
        {
            Temperature = (int)jObject["current_observation"]["temp_c"];
            RelativeHumidity = (string)jObject["current_observation"]["relative_humidity"];
            FeelsLike = (int)jObject["current_observation"]["feelslike_c"];
            Weather = (string)jObject["current_observation"]["weather"];
            ObservationTime = (string)jObject["current_observation"]["observation_time"];
        }

        // This function is called to actually output the relevant data.
        public string Serialize()
        {
            return String.Format($"{Temperature}, {RelativeHumidity}, {FeelsLike},{Weather},{ObservationTime}");
        }
    }
}
