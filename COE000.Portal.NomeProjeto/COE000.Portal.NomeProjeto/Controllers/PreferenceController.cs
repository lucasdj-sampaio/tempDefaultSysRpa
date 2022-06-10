#region - Imports
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using COE000.Portal.NomeProjeto.Enum;
using COE000.Portal.NomeProjeto.Util;
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
        private readonly EmailService _emailSender;
        private EntityRepository _repository { get; set; }

        public PreferenceController(ILogger<PreferenceController> logger,
            DataBaseContext context,
            EmailService emailSender)
        {
            _logger = logger;
            _repository = new(context);
            _emailSender = emailSender;
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

        public async Task<IActionResult> RemoveUser(Guid id) 
        {
            var response = await _repository.DeleteUser(id, true);

            return View("Preference", new {
                NotifyModal = response,
                UserGroup = await _repository.GetUser(HttpContext.User.Identity.Name)
            });
        }

        public async Task<IActionResult> InviteUser(string userMail)
        {
            try 
            {
                await _repository.CreateHashCode(true);

                var callbackUrl = Url.Page($"/Account/Register",
                pageHandler: null,
                values: new { 
                    token = await _repository.GetLastHashCreated()
                    },
                protocol: Request.Scheme);
            
                await _emailSender.SendAsync(userMail,
                    "Parece que você recebeu um convite!",
                    @$"<p style='font-family:Calibri; font-size:16px; color:#1F1589;'>Oi, {userMail}!<br>
                    Alguém te convidou para fazer parte do nosso sistema
                    <br><br>
                    <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clique aqui para realizar seu cadastro!</a>
                    <br><br>
                    Att,
                    </p>");

                return View("Preference", new {
                    NotifyModal = new NotifyModel(EModalNotification.Sucess) {
                            Message = "Usuário convidado com exito" 
                        },
                    UserGroup = await _repository.GetUser(HttpContext.User.Identity.Name)
                });
            }
            catch(Exception ex) 
            {
                return View("Preference", new {
                    NotifyModal = new NotifyModel(EModalNotification.Error) { 
                            Message=ex.Message 
                        },
                    UserGroup = await _repository.GetUser(HttpContext.User.Identity.Name)
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}