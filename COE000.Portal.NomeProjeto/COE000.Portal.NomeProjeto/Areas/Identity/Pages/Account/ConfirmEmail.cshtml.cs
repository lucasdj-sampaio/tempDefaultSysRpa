using System.Text;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
using COE000.Portal.NomeProjeto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace COE000.Portal.NomeProjeto.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IncriseUserModel> _userManager;

        public NotifyModel? Notify { get; set; }

        public ConfirmEmailModel(UserManager<IncriseUserModel> userManager) 
            => _userManager = userManager;

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Notify = new NotifyModel(Enum.EModalNotification.Error)
                {
                    Message = $"Usuário de ID: '{userId}' não localizado."
                };

                return Page();
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            Notify = result.Succeeded 
                ? new NotifyModel(Enum.EModalNotification.Sucess) {
                    Message = "Tudo pronto, e-mail confirmado com sucesso!" 
                }
                : new NotifyModel(Enum.EModalNotification.Error)
                {
                    Message = "Erro ao confirmar e-mail"
                };

            return Page();
        }
    }
}