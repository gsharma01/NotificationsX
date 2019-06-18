using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace NotificationsXDomain
{
    public class PushNotificationConsumer : IPushNotificationConsumer
    {
        private readonly IPushNotificationsStream _NotificationStream;
        private readonly Action<string> _Logger;
        public PushNotificationConsumer(IPushNotificationsStream NotificationStream, Action<string>  Logger)
        {
            _NotificationStream = NotificationStream;
            _Logger = Logger;
        }
    
        public void Listen()
        {
            var config = new Dictionary<string, object>
        {
            {"group.id","pushnotification_consumer" },
            {"bootstrap.servers", "localhost:9092" },
            { "enable.auto.commit", "false" }
        };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe("notification_pushnotification");
                _Logger("Subscribed");

                consumer.OnMessage += (_, msg) =>
                {
                    _NotificationStream.Publish(msg.Value);
                };
                _Logger("Message Attached");
                while (true)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}
