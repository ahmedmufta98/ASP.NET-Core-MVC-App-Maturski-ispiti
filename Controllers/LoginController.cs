using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;


namespace RS1_Ispit_asp.net_core.Controllers
{
    public class LoginController : Controller
    {
        private MojContext _db;

        public LoginController(MojContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

      
    }
}