﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NotificationsXDomain;

namespace NotificationsX.WebClient
{
    public interface IRelay
    {
        void CreateSubscriber(IHubContext<Hubs.NotificationHub> hubContext, IPushNotificationsStream stream);
    }
}
