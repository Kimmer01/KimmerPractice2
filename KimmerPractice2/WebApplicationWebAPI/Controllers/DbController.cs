using EFCorePractice;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationWebAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class DbController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DbController(MyDbContext dbCtx)
        {
            _context = dbCtx;
        }

        [HttpGet]
        public string GetConnectionStr() {
            int c = _context.Books.Count();
            return $"Books count: {c}";
        }
    }
}
