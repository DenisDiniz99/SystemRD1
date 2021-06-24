using System;
using System.Collections.Generic;
using System.Text;
using SystemRD1.Business.Notifications;

namespace SystemRD1.Business.Contracts.Notifiers
{
    public interface INotifier
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
