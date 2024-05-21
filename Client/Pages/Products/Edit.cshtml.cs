using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using WebAPI.Dto;

namespace Client.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public EditModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public ProductDto Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            string url = $"https://localhost:7188/ass1/api/product/detail/{id}";
            HttpResponseMessage rs = await _httpClient.GetAsync(url);
            if (rs.IsSuccessStatusCode) {
                string strData = await rs.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true,
                };
                Product = JsonSerializer.Deserialize<ProductDto>(strData, option);

                url = $"https://localhost:7188/ass1/api/category/all";
                rs = await _httpClient.GetAsync(url);
                strData = await rs.Content.ReadAsStringAsync();
                List<CategoryDto> categoryDtos = JsonSerializer.Deserialize<List< CategoryDto>>(strData, option);
                ViewData["CategoryId"] = new SelectList(categoryDtos, "CategoryId", "CategoryName");
            } else {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //string url = $"https://localhost:7188/ass1/api/product/update";

            //HttpContent content = new StringContent(JsonSerializer.Serialize(Product));
            //HttpResponseMessage rs = await _httpClient.PutAsJsonAsync(url, content);

            return RedirectToPage("./Index");
        }
    }
}
