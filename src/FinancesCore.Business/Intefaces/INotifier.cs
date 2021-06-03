using System.Collections.Generic;
using FinancesCore.Business.Notifications;

namespace FinancesCore.Business.Intefaces
{
    public interface INotifier
    {
        bool IsThereAnyNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
