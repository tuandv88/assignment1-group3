using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPI.Dto;

namespace Client.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<ProductDto> Product { get;set; } = default!;

        public async Task OnGetAsync(string searchString, decimal? startUnitPrice, decimal? endUnitPrice)
        {
            string url = "";
            if (searchString==null && startUnitPrice ==null && endUnitPrice == null) {
                url = "https://localhost:7188/ass1/api/product/all";
            } else {
                url = $"https://localhost:7188/ass1/api/product/search?searchString={searchString}&startUnitPrice={startUnitPrice}&endUnitPrice={endUnitPrice}";         
            }
            HttpResponseMessage rs = await _httpClient.GetAsync(url);
            string strData = await rs.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
            };
            Product = JsonSerializer.Deserialize<List<ProductDto>>(strData, option);
        }
    }
}
