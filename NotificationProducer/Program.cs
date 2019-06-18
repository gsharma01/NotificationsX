using System;
using NotificationsXDomain;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotificationProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select your choice");
            Console.WriteLine("------------------");
            Console.WriteLine("1. Add numeric number to simulate notifications");
            Console.WriteLine("2. Add Custom notification messages");


            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CaseFirst();
                    break;
                case "2":
                    CaseSecond();
                    break;

            }
        }

        private static void CaseFirst()
        {
            Console.WriteLine("Enter number to simulate");
            int number;
            int.TryParse(Console.ReadLine(), out number);

            if (number > 0)
            {
                for(int i=1;i<=number;i++)
                {
                    string message = "Notification message: " + i;
                    var producer = new PushNotificationProducer();
                    producer.Produce(message);
                }
            }
        }

        private static void CaseSecond()
        {
            Console.WriteLine("Enter your message. Enter q for quitting");
            var message = default(string);
            while ((message = Console.ReadLine()) != "q")
            {

                var producer = new PushNotificationProducer();
                producer.Produce(message);
            }
        }
    }
}
