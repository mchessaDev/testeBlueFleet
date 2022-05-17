using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testeBlueFleet.Models
{
    public class Veiculo
    {
        [BsonElement("_id")]
        public Guid VeiculoId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Montadora { get; set; }

    }    
}
