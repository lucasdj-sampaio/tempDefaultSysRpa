namespace COE000.Portal.NomeProjeto.Models.Entity
{
    public class FunctionTypeModel
    {
        public int Id { get; set; }

        public string? FunctionName { get; set; }

        public string? Description { get; set; }


        public ICollection<HistoricModel>? HistoricGroup { get; set; }
    }
}