using System;
using NotificationsXDomain;

namespace Notifications.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            var stream = new PushNotificationsStream();
           
            PushNotificationConsumer consumer = new PushNotificationConsumer(stream, Console.WriteLine);

            stream.Subscribe("Subscriber1", (m) => Console.WriteLine($"Subscriber1 Message : {m}"));
            //stream.Subscribe("Subscriber2", (m) => Console.WriteLine($"Subscriber2 Message : {m}"));

            consumer.Listen();
        }
    }
}
