namespace COE000.Portal.NomeProjeto.Models.Entity
{
    public class EnvironmentModel
    {
        public int Id { get; set; }

        public string? EnvironmentName { get; set; }


        public ICollection<RpaCredentialModel>? RpaCredentiaGroup { get; set; }
    }
}