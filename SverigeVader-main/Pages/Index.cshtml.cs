using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SverigeVader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SverigeVader.Pages
{


    public class IndexModel : PageModel
    
    {
        
        [BindProperty(SupportsGet = true)]
        public string City { get; set; }
        //public IEnumerable<Models.Measurement> Measurements { get; set; }

        //public IEnumerable<Models.Measurement> AllMeasurements { get; set; }

        //public List<Models.Measurement> AvgTempPerCity { get; set; }
        //public IEnumerable<IGrouping<string, Models.Measurement>> Test { get; set; }
        //public IEnumerable<Models.Measurement> MetrologicalFall { get; set; }




        //[BindProperty(SupportsGet =true)]
        //public string  ShowMenu { get; set; }

        //public string[] CitiesArray { get; set; }

        //private readonly ILogger<ErrorModel> _logger;




        public async Task OnGetAsync()
        {


            var client = new HttpClient();
            string[] cities = { "Stockholm","Kiruna","Sundsvall","Göteborg","Malmö","Nacka"};
            List<Models.Measurement> measurements = new List<Models.Measurement>();
            var collection = new TestMap.Collection();

            foreach (string city in cities)
            {

                Task<string> getWeatherStringTask = client.GetStringAsync($"https://api.weatherbit.io/v2.0/current?key=c40b31cc9aa44a588ed3dbbef62bff3e&lang=sv&city=" + city);
                string weatherString = await getWeatherStringTask;
                var weatherData = JsonSerializer.Deserialize<Models.WeatherData>(weatherString);

                var measurement = new Measurement
                {
                    Id = Guid.NewGuid(),
                    City = city,
                    Date = DateTime.Now,
                    Values = new Models.Values
                    {
                        Relativehumidity = weatherData.data[0].rh,
                        Temp = weatherData.data[0].temp,
                        WindSpeed = Math.Round(weatherData.data[0].wind_spd),
                        Description = weatherData.data[0].weather.description,
                        Cloud = weatherData.data[0].clouds,
                        Uv = weatherData.data[0].uv,
                        Wind_Dir = weatherData.data[0].wind_dir,
                        App_Temp = weatherData.data[0].app_temp,
                        



                    }
                };

                measurements.Add(measurement);
                await collection.MeasurementCollection().InsertOneAsync(measurement);
            }

            //Measurements = measurements;

            //AllMeasurements = collection.MeasurementCollection().Find(new BsonDocument())
            //    .ToList()
            //    .Where(m => m.Values.Temp > 5);

            //AvgTempPerCity = collection.MeasurementCollection().Find(new BsonDocument())
            //    .ToList();


            //Test = AvgTempPerCity.GroupBy(x => x.City);

        }
        public async Task<IActionResult> OnPostAsync()
        {

            return Page();
        }


    }
}
