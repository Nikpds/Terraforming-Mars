using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Terraforming.Api.Models
{
    public class Entity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public DateTime Updated { get; set; }
    }
}
