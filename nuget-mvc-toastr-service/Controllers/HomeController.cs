using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nuget_mvc_toastr_service.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ToastrService.AddToUserQueue(message: "Hello world.", title: "Hello W!", type: ToastrType.Warning);

            return View();
        }
    }
}