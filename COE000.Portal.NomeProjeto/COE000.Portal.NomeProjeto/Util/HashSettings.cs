#region - Imports
using COE000.Portal.NomeProjeto.Reposity;
#endregion

namespace COE000.Portal.NomeProjeto.Util
{
    public static class HashSettings
    {
        public static bool HashIsValid(EntityRepository repository, Guid currentHash) => 
            currentHash == (repository.GetHash(currentHash).GetAwaiter()).GetResult().Id;
    }
}