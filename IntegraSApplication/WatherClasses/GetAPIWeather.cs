using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegraSApplication.WatherClasses
{
    public   class GetAPIWeather 
    {
        public static float GetWeather()
        {

            string url = "https://api.openweathermap.org/data/2.5/weather?q=Samara&appid=bd25c75c9e2aa6b015dc9507bfd77f15&units=metric";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responce;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                responce = streamReader.ReadToEnd();
            }
            WeatherResponce  weatherResponce = JsonConvert.DeserializeObject<WeatherResponce>(responce); //responce = JsonFile
            return weatherResponce.Main.temp;
        }
    }
}
