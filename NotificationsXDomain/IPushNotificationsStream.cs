using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsXDomain
{
    public interface IPushNotificationsStream
    {
        void Publish(string message);
        void Subscribe(string subscriberName, Action<string> action);
    }
}
