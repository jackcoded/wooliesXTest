using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace wooliesXTest.Models
{
    public class Trolley
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
        [JsonProperty("specials")]
        public List<Special> Specials { get; set; }
        [JsonProperty("quantities")]
        public List<ProductQuantity> Quantities { get; set; }
    }
}
