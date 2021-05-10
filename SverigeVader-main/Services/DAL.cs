using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SverigeVader.Services
{
    public class DAL : IDAL
    {

        private readonly IConfiguration _configuration;
        private string _myMongoDbSettings;


        public DAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _myMongoDbSettings = _configuration["MyMongoDBSettings"];
        }

        private MongoClient GetClient()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(_configuration[$"{_myMongoDbSettings}:Host"], 10255);
            settings.UseTls = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            settings.RetryWrites = false;
            

            MongoIdentity identity = new MongoInternalIdentity(_configuration[$"{_myMongoDbSettings}:DbNaMe"], _configuration[$"{_myMongoDbSettings}:UserName"]);
            MongoIdentityEvidence evidence = new PasswordEvidence(_configuration[$"{_myMongoDbSettings}:Password"]);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);

            return client;
        }


        public IEnumerable<Models.Measurement> GetWeatherData()
        {
            var client = GetClient();
            var database = client.GetDatabase(_configuration[$"{_myMongoDbSettings}:DbNaMe"]);
            var measurementCollection = database.GetCollection<Models.Measurement>(_configuration[$"{_myMongoDbSettings}:CollectionName"]);
            return measurementCollection.Find(new BsonDocument()).ToList();
        }


    }
}
