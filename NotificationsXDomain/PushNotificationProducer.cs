using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace NotificationsXDomain
{
    public class PushNotificationProducer : IPushNotificationProducer
    {
        public void Produce(string message)
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", "localhost:9092"},
                { "acks","all" }
        };
            using (var producer = new Producer <Null, string> (config, null, new StringSerializer(Encoding.UTF8)))
        {
                producer.ProduceAsync("notification_pushnotification", null, message);//.GetAwaiter().GetResult();

                producer.Flush(100);
            }
        }
    }
}
