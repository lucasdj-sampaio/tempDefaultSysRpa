#region - Imports
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COE000.Portal.NomeProjeto.Enum;
using COE000.Portal.NomeProjeto.Models;
using COE000.Portal.NomeProjeto.Reposity;
using Microsoft.AspNetCore.Authorization;
using COE000.Portal.NomeProjeto.Models.Entity;
using COE000.Portal.NomeProjeto.Reposity.Entity;
#pragma warning disable CS8604, CS8602, IDE1006
#endregion

namespace COE000.Portal.NomeProjeto.Controllers
{
    [Authorize]
    public class RpaPreferenceController : Controller
    {
        private readonly ILogger<RpaPreferenceController> _logger;

        private EntityRepository _repository { get; set; }


        public RpaPreferenceController(ILogger<RpaPreferenceController> logger, DataBaseContext context)
        {
            _logger = logger;
            _repository = new(context);
        }


        public async Task<IActionResult> RpaPreference()
            => View(new {
                SelectValues = await _repository.GetSelectEnvItems(),
                UserCardGroup = await _repository.GetRpaCredentialItems(),
                NotifyModal = new NotifyModel()
            });


        public async Task<IActionResult> GetRpaUserWithFilter(SearchModel filter)
            => View("RpaPreference", new {
                SelectValues = await _repository.GetSelectEnvItems(),
                UserCardGroup = await _repository.GetRpaCredentialItems(filter.InputedCriterie),
                NotifyModal = new NotifyModel()
            });


        [HttpPost]
        public async Task<IActionResult> UpdateRpaCredential(RpaCredentialModel rpaUser)
        {
            var response = await _repository.UpdateRpaUser(rpaUser);

            if (response.IsSucess())
                await _repository.InsertHistoric(new HistoricModel()
                {
                    DateOn = DateTime.Now,
                    Observation = $"As credencias do usuário: '{rpaUser.UserName}' foram alteradas!",
                    UserId = await _repository.GetUserIdByName(HttpContext.User.Identity.Name),
                    FunctionTypeId = (int)EFunctionType.RpaUserController
                }, true);

            return View("RpaPreference", new {
                SelectValues = await _repository.GetSelectEnvItems(),
                UserCardGroup = await _repository.GetRpaCredentialItems(),
                NotifyModal = response
            });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}