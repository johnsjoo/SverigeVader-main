using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Models
{
    public class WeatherData
    {
        public Datum[] data { get; set; }
        public double count { get; set; }
    }

    public class Datum
    {
        public double rh { get; set; }
        public float wind_spd { get; set; }
        public Weather weather { get; set; }
        public string datetime { get; set; }
        public float temp { get; set; }
        public double clouds { get; set; }
        public float uv { get; set; }
        public double wind_dir { get; set; }
        public double app_temp { get; set; }

    }

    public class Weather
    {
        public string icon { get; set; }
        public double code { get; set; }
        public string description { get; set; }
    }












}
