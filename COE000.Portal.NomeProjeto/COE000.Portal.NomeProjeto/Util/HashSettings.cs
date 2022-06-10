#region - Imports
using COE000.Portal.NomeProjeto.Reposity;
#endregion

namespace COE000.Portal.NomeProjeto.Util
{
    public static class HashSettings
    {
        public static bool HashIsValid(EntityRepository repository, Guid currentHash)
            => (repository.GetHash(currentHash).GetAwaiter()).GetResult() is not null;
    }
}