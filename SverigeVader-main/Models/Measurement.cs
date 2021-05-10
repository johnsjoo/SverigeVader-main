using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public Values Values { get; set; }
    }

    public class Values
    {
        public double Relativehumidity { get; set; }
        public float Temp { get; set; }
        public double WindSpeed { get; set; }
        public double Cloud { get; set; }
        public float Uv { get; set; }
        public double Wind_Dir { get; set; }
        public double App_Temp { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
    }
}
