using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : DynamicController<Product>
    {
        public ProductsController(AppDbContext context) : base(context)
        {
        }
    }
}
