
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Mvc;

using WebApplication4.Models;

namespace WebApplication4.Controllers
{
  
    [Produces("application/json")]
    public class HomeController : ApiController
    {
        private ApplicationDbContext dbContext { get; set; }
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
                var products = dbContext.Album.ToList();

                return Json(products);
            
        }
       
    }
}
