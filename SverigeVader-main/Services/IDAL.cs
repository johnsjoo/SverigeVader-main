using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigeVader.Services
{
    interface IDAL
    {
        IEnumerable<Models.Measurement> GetWeatherData();
        
    }
}
