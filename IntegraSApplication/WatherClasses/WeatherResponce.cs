using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraSApplication.WatherClasses
{
    class WeatherResponce
    {
        public TemperatureInfo Main { get; set; } // => Main -> temp т.е где то нужно описать main а потом уже в
                                                  // классе который нужно распарсить пишем
                                                  // название 
        public string Name { get; set; }
    }
}
