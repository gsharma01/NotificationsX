using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NotificationsXDomain;

namespace NotificationsX.WebClient
{
    public class NotificationRelay //: IRelay
    {
        public NotificationRelay(IHubContext<Hubs.NotificationHub> hubContext, IPushNotificationsStream stream)
        {            
            stream.Subscribe("PushNotification_Relay", (m) => hubContext.Clients.All.SendAsync("NotificationMessage", m));
        }

    }
}
