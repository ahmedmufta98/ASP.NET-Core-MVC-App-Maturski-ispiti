using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;

namespace RS1_Ispit_asp.net_core.Controllers
{
    
    public class HomeController : Controller
    {
        private MojContext _context;

        public HomeController(MojContext context/*,SignInManager<IdentityUser> user*/)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
            //if (user.IsSignedIn)
            //{
            //    // radi listu necegga n
            //}
        }

        public IActionResult TestDB()
        {
            MojDBInitializer.Izvrsi(_context);
            return View(_context); 
        }

     
    }
}