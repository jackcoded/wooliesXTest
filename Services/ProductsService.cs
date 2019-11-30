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
            return await GetData<List<Product>>("products");
        }

        private async Task<T> GetData<T>(string endPoint)
        {
            var token = _config["appSettings:appToken"];
            var uri = _config["appSettings:appUri"];
            var content = await _httpClient.GetStringAsync($"{uri}/resource/{endPoint}?token={token}");
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
