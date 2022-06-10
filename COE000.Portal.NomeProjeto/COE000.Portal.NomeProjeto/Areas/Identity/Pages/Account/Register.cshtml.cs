// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using COE000.Portal.NomeProjeto.Util;
using COE000.Portal.NomeProjeto.Models;
using COE000.Portal.NomeProjeto.Enum;
using COE000.Portal.NomeProjeto.Models.Entity;
using COE000.Portal.NomeProjeto.Reposity;
using COE000.Portal.NomeProjeto.Reposity.Entity;

namespace COE000.Portal.NomeProjeto.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IncriseUserModel> _signInManager;
        private readonly UserManager<IncriseUserModel> _userManager;
        private readonly IUserStore<IncriseUserModel> _userStore;
        private readonly IUserEmailStore<IncriseUserModel> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly EmailService _emailSender;
        private readonly EntityRepository _repository;

        public RegisterModel(
            UserManager<IncriseUserModel> userManager,
            IUserStore<IncriseUserModel> userStore,
            SignInManager<IncriseUserModel> signInManager,
            ILogger<RegisterModel> logger,
            EmailService emailSender,
            DataBaseContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _repository = new(context);
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirme sua senha")]
            [Compare("Password", ErrorMessage = "As senhas não coincidem")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Como você quer ser chamado?")]
            public string Nick { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Identidade sexual")]
            public char Gender { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string token = null, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (String.IsNullOrEmpty(token) || !HashSettings.HashIsValid(_repository, Guid.Parse(token)))
               return RedirectToPage("./Login");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.Nick = Input.Nick;
                user.UserGender = Input.Gender;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { 
                            area = "Identity", 
                            userId, 
                            code, 
                            returnUrl 
                        },
                        protocol: Request.Scheme);

                    await _emailSender.SendAsync(Input.Email, "Confirmação de e-mail",
                        @$"<p style='font-family:Calibri; font-size:16px; color:#1F1589;'>Oi, {Input.Nick}!<br>
                        Sua conta está quase pronta. Para ativá-la, por favor confirme o seu<br>
                        endereço de email clicando no link abaixo.
                        <br><br>
                        <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clique aqui!</a>
                        <br><br>
                        Sua conta não será ativada até que seu email seja confirmado.<br>
                        Se você não se cadastrou no serviço recentemente, por favor ignore este email.<br>
                        Att,
                        </p>");

                    return RedirectToPage("./RegisterConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IncriseUserModel CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IncriseUserModel>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IncriseUserModel)}'. " +
                    $"Ensure that '{nameof(IncriseUserModel)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IncriseUserModel> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IncriseUserModel>)_userStore;
        }
    }
}