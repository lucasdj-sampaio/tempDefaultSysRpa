#region - Imports
using COE000.Portal.NomeProjeto.Enum;
#endregion

namespace COE000.Portal.NomeProjeto.Models
{
    public class NotifyModel
    {
        public string? Message { get; set; }

        public EModalNotification NotificationType { get; }

        public NotifyIcon Icon { get; set; }

        public bool IsSucess() => NotificationType == EModalNotification.Sucess;

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public NotifyModel(EModalNotification notification = EModalNotification.Warning)
        {
            NotificationType = notification;
            Icon = new NotifyIcon(notification);
        }
    }

    public class NotifyIcon 
    {
        public string IconClass { get; set; }

        public NotifyIcon(EModalNotification notification) =>
            IconClass = GetIconClass(notification);

        private static string GetIconClass(EModalNotification notification) =>
            (notification) switch
            {
                EModalNotification.Sucess => "bx bxs-check-circle",
                EModalNotification.Error => "bx bxs-x-circle",
                _ => "bx bxs-error-circle"
            };
    }
}