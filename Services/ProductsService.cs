using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wooliesXTest.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace wooliesXTest.Services
{
    public interface IProductsService
    {
        Task<List<Product>> SortProducts(string sortOptions);
    }
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ProductsService(HttpClient httpClient, [FromServices] IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
       public async Task<List<Product>> SortProducts(string sortOptions)
        {
            List<Product> productList = await GetData<List<Product>>("products");
            List<Order> orderList = await GetData<List<Order>>("shopperHistory");

            if (String.Equals(sortOptions, "low", StringComparison.InvariantCultureIgnoreCase)) {
                productList = productList.OrderBy(p => p.Price).ToList();
            }
            else if (String.Equals(sortOptions, "high", StringComparison.InvariantCultureIgnoreCase)) {
                productList = productList.OrderByDescending(p => p.Price).ToList();
            }
            else if (String.Equals(sortOptions, "ascending", StringComparison.InvariantCultureIgnoreCase)) {
                productList = productList.OrderBy(p => p.Name).ToList();
            }
            else if (String.Equals(sortOptions, "descending", StringComparison.InvariantCultureIgnoreCase)) {
                productList = productList.OrderByDescending(p => p.Name).ToList();
            }
            else if (String.Equals(sortOptions, "recommended", StringComparison.InvariantCultureIgnoreCase)) {
                productList = orderList.Select(o => o.Products)
                                       .SelectMany(p => p).ToList()
                                       .Concat(productList.Select(p => { p.Quantity = 0; return p; }).ToList())
                                       .GroupBy(p => p.Name)
                                       .Select(g =>
                                               new Product
                                               {
                                                   Name = g.First().Name,
                                                   Price = g.First().Price,
                                                   Quantity = g.Sum(p => p.Quantity)
                                               })
                                       .OrderByDescending(p => p.Quantity)
                                       // filter list from productList
                                       .Where(product => productList.Any(p => product.Name == p.Name))
                                       // changes all quantity back to 0
                                       .Select(p => { p.Quantity = 0; return p; })
                                       .ToList(); 
            }
            return productList;
        }

        // Generic helper for Getting data and converting json
        private async Task<T> GetData<T>(string endPoint)
        {
            var token = _config["appSettings:appToken"];
            var uri = _config["appSettings:appUri"];
            var content = await _httpClient.GetStringAsync($"{uri}/resource/{endPoint}?token={token}");
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
