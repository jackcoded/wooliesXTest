using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wooliesXTest.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace wooliesXTest.Services
{
    public interface ITrolleyCalculatorServices
    {
        public Task<decimal> GetTotal(Trolley response);
    }
    public class TrolleyCalculatorServices : ITrolleyCalculatorServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public TrolleyCalculatorServices(HttpClient httpClient, [FromServices] IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<decimal> GetTotal(Trolley response)
        {
            return await PostData("trolleyCalculator", response);
        } 
        private async Task<decimal> PostData(string endPoint, Trolley request)
        {
            var token = _config["appSettings:appToken"];
            var uri = _config["appSettings:appUri"];
            var jsonRequest = JsonConvert.SerializeObject(request);
            var response = await _httpClient.PostAsync($"{uri}/resource/{endPoint}?token={token}", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            decimal result;
            if (Decimal.TryParse(contents, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }

}
