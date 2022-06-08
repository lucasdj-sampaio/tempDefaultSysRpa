#region - Imports
using Microsoft.AspNetCore.Identity;
using COE000.Portal.NomeProjeto.Models.Entity;
#endregion

namespace COE000.Portal.NomeProjeto.Areas.Identity.Data;

// Add profile data for application users by adding properties to the IncriseUserModal class
public class IncriseUserModel : IdentityUser
{
    public char UserGender { get; set; }

    public string? Nick { get; set; }

    public ICollection<HistoricModel>? HistoricCollection { get; set; }
}