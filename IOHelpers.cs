using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wunderground_API_Project
{
    public static class IOHelpers
    {
        public static void WriteWeatherObject(WeatherClass weatherObject, int ErrorCode = 0)
        {
            //The following block of code is used to make directories and/or the actual file as required. The paths are, of course, complicated.
            const string BasePath = "D:\\Runka's Files\\WeatherData\\";
            DateTime CurrentTimestamp = DateTime.Now;
            string DataToWrite = null;
            if (!Directory.Exists(String.Format($"{BasePath}{CurrentTimestamp.Year}")))
            {
                Directory.CreateDirectory(String.Format($"{BasePath}{CurrentTimestamp.Year}"));
            }

            if (!Directory.Exists(String.Format($"{BasePath}{CurrentTimestamp.Year}\\{CurrentTimestamp.ToString("MMMM")}")))
            {
                Directory.CreateDirectory(String.Format($"{BasePath}{CurrentTimestamp.Year}\\{CurrentTimestamp.ToString("MMMM")}"));
            }

            string CurrentFilePath = String.Format($"{BasePath}{CurrentTimestamp.Year}\\{CurrentTimestamp.ToString("MMMM")}\\{CurrentTimestamp.Day}.csv");


            if (!File.Exists(CurrentFilePath))
            {
                //If the file does not exist, create it and write "headers".
                using (StreamWriter streamWriter = new StreamWriter(CurrentFilePath))
                {
                    streamWriter.WriteLine("Day,Time,Temperature,Relative Humidity,Feels Like,Weather,Observation Day,Observation Time");
                }
            }

            if(ErrorCode == 0)
            {
                DataToWrite = weatherObject.Serialize();
            }
            else if(ErrorCode == 1)
            {
                DataToWrite = "Error code 1: Possible API problem";
            }
            else if(ErrorCode == 2)
            {
                DataToWrite = "Error code 2: Possible network problem";
            }

            //Finally, we can write the data to the file.

            DataToWrite = String.Format($"{CurrentTimestamp.Day},{CurrentTimestamp.TimeOfDay}, {DataToWrite}");
            using (StreamWriter streamWriter = new StreamWriter(CurrentFilePath, true))
            {
                streamWriter.WriteLine(DataToWrite);
            }
        }

    }
}
