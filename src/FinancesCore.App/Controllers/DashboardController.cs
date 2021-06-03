﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinancesCore.App.ViewModels;

namespace FinancesCore.App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500) {
                modelErro.Message = "An error has occurred! Please try again later or contact our support.";
                modelErro.Title = "An error has occurred!";
                modelErro.ErroCode = id;
            }
            else if (id == 404) {
                modelErro.Message = "The page you are looking for does not exist! <br />If you have any questions, please contact our support";
                modelErro.Title = "Oops! Page not found.";
                modelErro.ErroCode = id;
            }
            else if (id == 403) {
                modelErro.Message = "You are not allowed to do this.";
                modelErro.Title = "Access denied";
                modelErro.ErroCode = id;
            }
            else {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
