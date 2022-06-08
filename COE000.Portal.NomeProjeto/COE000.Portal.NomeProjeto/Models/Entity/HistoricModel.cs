#region - Imports
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
#endregion

namespace COE000.Portal.NomeProjeto.Models.Entity
{
    public class HistoricModel
    {
        public Guid Id { get; set; } 

        public DateTime DateOn { get; set; }

        public string? Observation { get; set;}


        public string? UserId { get; set; }
        public IncriseUserModel? User { get; set; }

        public int FunctionTypeId { get; set; }
        public FunctionTypeModel? FunctionType { get; set; }
    }
}