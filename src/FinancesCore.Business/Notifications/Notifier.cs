using System.Collections.Generic;
using System.Linq;
using FinancesCore.Business.Intefaces;

namespace FinancesCore.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }
        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
           return _notifications;
        }

        public bool IsThereAnyNotification()
        {
            return _notifications.Any();
        }
    }
}
