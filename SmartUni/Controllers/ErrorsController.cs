using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartUni.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }
    }
}