﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace wooliesXTest.Models
{
    public class Special
    {
        [JsonProperty("quantities")]
        public List<ProductQuantity> Quantities { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
