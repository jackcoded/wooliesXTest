using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wooliesXTest.Models;
namespace wooliesXTest.Models
{
    public class Order
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
