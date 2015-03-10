using compareIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompareIT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            CompareITContext dbContext = new CompareITContext();

            dbContext.Computers.Add(new compareIT.Data.Model.Computer() { Name = "Toshiba" });

            dbContext.SaveChanges();

            var computer = dbContext.Computers.ToList();

            return View();
        }

        
    }
}
