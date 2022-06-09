#region - Imports
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COE000.Portal.NomeProjeto.Models;
using COE000.Portal.NomeProjeto.Reposity;
using Microsoft.AspNetCore.Authorization;
using COE000.Portal.NomeProjeto.Reposity.Entity;
#pragma warning disable CS8604, CS8602, IDE1006
#endregion

namespace COE000.Portal.NomeProjeto.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private EntityRepository _repository { get; set; }


        public HomeController(ILogger<HomeController> logger, DataBaseContext context)
        {
            _logger = logger;
            _repository = new(context);
        }

        public async Task<IActionResult> Index() 
            => View(new { NotifyModal = new NotifyModel() });

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}