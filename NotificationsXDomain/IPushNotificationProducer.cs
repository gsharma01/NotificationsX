using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsXDomain
{
    public interface IPushNotificationProducer
    {
        void Produce(string message);
    }
}
