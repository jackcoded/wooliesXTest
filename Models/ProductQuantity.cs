using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace wooliesXTest.Models
{
    public class ProductQuantity
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
