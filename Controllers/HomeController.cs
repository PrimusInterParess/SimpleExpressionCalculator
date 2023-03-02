using MathExpressionSolver.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalculate _calculate;

        public HomeController(
            ILogger<HomeController> logger,
            ICalculate calculate
            )
        {
            _logger = logger;
            _calculate = calculate;
        }

        public IActionResult Index()
        {
            var resultModel = new DataModel { Data = "No result yet" };
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult Index(DataModel inputModel)
        {
            var resultModel = new DataModel();

            try
            {
                resultModel.Input = inputModel.Input;
                resultModel.Data = this._calculate.ProcessData(inputModel.Input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(String.Empty, e.Message);
                if (this.ModelState.IsValid==false)
                {
                    return this.View(resultModel);
                }
            }
            return View(resultModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}