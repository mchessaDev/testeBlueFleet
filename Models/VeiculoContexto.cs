using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeBlueFleet.Config;

namespace testeBlueFleet.Models
{
    public class VeiculoContexto
    {

        private readonly IMongoDatabase _mongoDatabase;

        public VeiculoContexto(IOptions <ConfigDB> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectionString);

            if(mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);

            }
        }

        public  IMongoCollection<Veiculo> Veiculos
        {
            get
            {
                return _mongoDatabase.GetCollection<Veiculo>("Veiculos");
            }
        }

    }
}
