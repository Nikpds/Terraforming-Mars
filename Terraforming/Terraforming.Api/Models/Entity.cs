using System;

namespace Terraforming.Api.Models
{
    public abstract class Entity
    {       
        public string Id { get; set; }
        public DateTime Updated { get; set; }
    }
}
