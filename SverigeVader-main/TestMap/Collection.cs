using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SverigeVader.TestMap
{
    public class Collection
    {
        private string host = "weatherdatacosmosmongo.mongo.cosmos.azure.com";
        private string userName = "weatherdatacosmosmongo";
        private string password = "qjrig9CTjfJeCFL1wx1JOsHy7XMsymWfvC4YbF6EVZsA5VI78yjomnnGZOCDSBJtijd3EpxmaWffjZi2ahPRzg==";

        private string dbName = "WDCosmosMongoDB";
        private string collectionName = "CosmosMongoCollection";


        private MongoClient GetClient()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseTls = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            settings.RetryWrites = false;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);

            return client;
        }


        public IMongoCollection<Models.Measurement> MeasurementCollection()
        {
            var client = GetClient();
            var database = client.GetDatabase(dbName);
            var measurementCollection = database.GetCollection<Models.Measurement>(collectionName);
            return measurementCollection;
        }


    }
}
