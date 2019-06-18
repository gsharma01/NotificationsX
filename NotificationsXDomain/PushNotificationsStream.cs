using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Subjects;


namespace NotificationsXDomain
{
    public class PushNotificationsStream : IPushNotificationsStream,IDisposable
    {

        private Subject<string> notificationSubject;
        private IDictionary<string, IDisposable> subscribers;

        public PushNotificationsStream()
        {
            notificationSubject = new Subject<string>();
            subscribers = new Dictionary<string, IDisposable>();
        }
        public void Publish(string message)
        {
            notificationSubject.OnNext(message);
        }

        public void Subscribe(string subscriberName, Action<string> action)
        {
            if (!subscribers.ContainsKey(subscriberName))
            {
                subscribers.Add(subscriberName, notificationSubject.Subscribe(action));
            }
        }
        public void Dispose()
        {
            if (notificationSubject != null)
            {
                notificationSubject.Dispose();
            }

            foreach (var subscriber in subscribers)
            {
                subscriber.Value.Dispose();
            }
        }
    }
}
