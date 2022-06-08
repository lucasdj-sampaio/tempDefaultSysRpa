namespace COE000.Portal.NomeProjeto.Models.Entity
{
    public class RpaCredentialModel
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Url { get; set; }


        public int EnvironmentId { get; set; }
        public EnvironmentModel? Environment { get; set; }
    }
}