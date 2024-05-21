using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using WebAPI.Dto;

namespace Client.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.TDbContext _context;

        public DetailsModel(BusinessObject.TDbContext context)
        {
            _context = context;
        }

        public ProductDto Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    Product = product;
            //}
            return Page();
        }
    }
}
