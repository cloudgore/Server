using ApiUseExample.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiUseExample
{
    class Program
    {
        static async void MainA(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            Task<Stream> jsonStr = httpClient.GetStreamAsync("http://localhost:5000/WeatherForecast");
            DataContractJsonSerializer dataContractJson = new DataContractJsonSerializer(typeof(List<WeatherForecast>));
            List<WeatherForecast> weatherForecasts = (List<WeatherForecast>)dataContractJson.ReadObject(await jsonStr);

            foreach (WeatherForecast weather in weatherForecasts)
            {
                Console.WriteLine(weather.summary);
            }
        }
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                MainA(args);
            });
            Console.ReadKey();
        }
    }
}
