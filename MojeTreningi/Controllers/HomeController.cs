using MojeTreningi.DAL;
using MojeTreningi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojeTreningi.Controllers
{
    public class HomeController : Controller
    {
        private MojeTreningiContext db = new MojeTreningiContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kalendarz()
        {
            ViewBag.Data = DateTime.Now;
            return View();
        }

    }
}