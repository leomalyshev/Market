using Market.DB;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(string name, string description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }

                    ctx.Products.Add(new Product { Name = name, Description = description });
                    ctx.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.Products.Select(x => new Product
                        { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deleteproducts")]
        public ActionResult<int> DeleteProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (!ctx.Products.Any())
                    {
                        return NotFound();
                    }

                    var list = ctx.Products.ExecuteDelete();
                    ctx.SaveChanges();
                    return Ok(list);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "updateproduct")]
        public ActionResult UpdateProducts(string name, string newName, string newDescription)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var product = ctx.Products.FirstOrDefault(x => x.Name == name);
                    if (product != null)
                    {
                        product.Name = newName;
                        product.Description = newDescription;
                    }
                    else
                    {
                        ctx.Products.Add(new Product { Name = name, Description = newDescription });
                    }

                    ctx.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}