#region - Imports
using System.ComponentModel;
#endregion

namespace COE000.Portal.NomeProjeto.Enum
{
    public enum EModalNotification
    {
        [Description("Used to show icon with sucess into a generic modal")] Sucess = 1,
        Warning = 2,
        Error = 3,
    }
}