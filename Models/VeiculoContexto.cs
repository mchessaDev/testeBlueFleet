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
        /* Criando uma intervace para nosso banco de dados  */
        private readonly IMongoDatabase _mongoDatabase;
        /* Criando nosso construtor */
        public VeiculoContexto(IOptions <ConfigDB> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectionString);
            /*  Verificando se nosso banco de dados esta Nulo */
            if (mongoClient != null)
            {
                /*  Pegando nossos valores dentro do nosso banco de dados */
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);

            }
        }
        /*  Retornando as informações dentro do nosso banco de dados */
        public IMongoCollection<Veiculo> Veiculos
        {
            get
            {
                return _mongoDatabase.GetCollection<Veiculo>("Veiculos");
            }
        }

    }
}
