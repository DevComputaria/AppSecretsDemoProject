using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public ActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    break;
                default:
                    return new HttpStatusCodeResult(500);
            }

            return View("Error", modelErro);
        }
    }
}
