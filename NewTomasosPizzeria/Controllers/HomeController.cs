using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewTomasosPizzeria.Models;

namespace NewTomasosPizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly NyTomasosContext _context;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Matratt.ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

