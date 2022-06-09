#region - Imports
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COE000.Portal.NomeProjeto.Models;
using Microsoft.AspNetCore.Authorization;
using COE000.Portal.NomeProjeto.Reposity;
using COE000.Portal.NomeProjeto.Reposity.Entity;
#pragma warning disable
#endregion

namespace COE334.Portal.FirstData.Controllers
{
    [Authorize]
    public class PreferenceController : Controller
    {
        private readonly ILogger<PreferenceController> _logger;
        private EntityRepository _repository { get; set; }

        public PreferenceController(ILogger<PreferenceController> logger
            , DataBaseContext context)
        {
            _logger = logger;
            _repository = new(context);
        }

        public async Task<IActionResult> Preference()
        {
            return View(new { 
                NotifyModal = new NotifyModel(),
                UserGroup = await _repository.GetUser(HttpContext.User.Identity.Name)
            });
        }

        public async Task<IActionResult> GetUserWithFilter(SearchModel filter)
            => View("Preference", new {
                NotifyModal = new NotifyModel(),
                UserGroup = await _repository.GetUser(HttpContext.User.Identity.Name, filter.InputedCriterie)
            }); 


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}