//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DIExample;
//using DIExample.s;
//using Microsoft.Extensions.DependencyInjection;

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        // Create IoC container
//        var services = new ServiceCollection();

//        //Register services
//        services.AddScoped<IMessageService, EmailService>();
//        services.AddScoped<IMessageService, SmsService>(); // Use SMS instead

//        services.AddScoped<Notification>();

//        // Build provider
//        var provider = services.BuildServiceProvider();

//        // Resolve Notification
//        var notification = provider.GetRequiredService<Notification>();

//        notification.Notify("Hello Dependency Injection Example");




//    }
//}

//using constructor

//namespace iocdependenciesinjection
//{
//    internal class Program
//    {

//        public interface IMessageService
//        {
//            void Send(string message);
//        }

//        public class EmailService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("Email sent: " + message);
//            }
//        }
//        public class EmailService1 : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("Email1 sent: " + message);
//            }
//        }
//        public class SmsService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("SMS sent: " + message);
//            }
//        }

//        public class Notification
//        {
//            private readonly IMessageService _messageService;

//            // Dependency comes from outside
//            public Notification(IMessageService messageService)
//            {
//                _messageService = messageService;
//            }

//            public void Notify(string msg)
//            {
//                _messageService.Send(msg);
//            }
//        }


//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//            //IMessageService email= new EmailService();
//            //Notification notification = new Notification(  email);
//            //   or
//            Notification notification = new Notification(new EmailService1());

//            notification.Notify("hello m");
//            //Notification notification = new Notification(new SmsService());
//            // or
//            IMessageService sms = new SmsService();
//            Notification notification1 = new Notification(sms);
//            notification1.Notify("newsms");
//            //notification.Notify("hello");

//        }
//    }
//}

//using properties

//using System;

//namespace iocdependenciesinjection
//{
//    internal class Program
//    {
//        public interface IMessageService
//        {
//            void Send(string message);
//        }

//        public class EmailService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("Email sent: " + message);
//            }
//        }

//        public class SmsService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("SMS sent: " + message);
//            }
//        }

//        public class Notification
//        {
//            // Dependency is injected via property
//            public IMessageService MessageService { get; set; }

//            public void Notify(string msg)
//            {
//                if (MessageService != null)
//                    MessageService.Send(msg);
//                else
//                    Console.WriteLine("No message service provided!");
//            }
//        }

//        static void Main(string[] args)
//        {
//            Console.WriteLine("Property Injection Example:");

//            // Create Notification object
//            Notification notification = new Notification();

//            // Inject dependency through property
//            notification.MessageService = new EmailService();
//            notification.Notify("Hello via Email!");

//            // Change dependency to SMS service dynamically
//            notification.MessageService = new SmsService();
//            notification.Notify("Hello via SMS!");

//            // Try without dependency
//            Notification notification2 = new Notification();
//            notification2.Notify("This will show missing dependency message!");
//        }
//    }
//}


//Method injection

//using System;

//namespace iocdependenciesinjection
//{
//    internal class Program
//    {
//        public interface IMessageService
//        {
//            void Send(string message);
//        }

//        public class EmailService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("Email sent: " + message);
//            }
//        }

//        public class SmsService : IMessageService
//        {
//            public void Send(string message)
//            {
//                Console.WriteLine("SMS sent: " + message);
//            }
//        }

//        public class Notification
//        {
//            // Dependency is provided via method
//            public void Notify(string msg, IMessageService messageService)
//            {
//                messageService.Send(msg);
//            }
//        }

//        static void Main(string[] args)
//        {
//            Console.WriteLine("Method Injection Example:");

//            // Create services
//            // IMessageService email = new EmailService();
//            IMessageService sms = new SmsService();

//            // Create Notification object
//            Notification notification = new Notification();

//            // Inject dependencies via method call
//            // notification.Notify("Hello via Email!", email);
//            notification.Notify("helloveml", new EmailService());
//            notification.Notify("Hello via SMS!", sms);
//        }
//    }
//}

//using System;

//public sealed class Singleton
//{
//    // Step 1: Create a private static variable to hold the single instance
//    private static Singleton instance;

//    // Step 2: Private constructor so no other class can create an object
//    private Singleton()
//    {
//        Console.WriteLine("Singleton instance created");
//    }

//    // Step 3: Public static method to provide global access
//    public static Singleton GetInstance()
//    {
//        if (instance == null)
//        {
//            instance = new Singleton(); // create instance if not exists
//        }
//        return instance;
//    }

//    public void ShowMessage()
//    {
//        Console.WriteLine("Hello from Singleton!");
//    }
//}

//// Usage
//class Program
//{
//    static void Main()
//    {
//        Singleton s1 = Singleton.GetInstance();
//        s1.ShowMessage();

//        Singleton s2 = Singleton.GetInstance();

//        // Check if both references point to the same instance
//        if (s1 == s2)
//        {
//            Console.WriteLine("Both are the same instance");
//        }
//    }
//}

using System;

public interface IMessageService
{
    void Send(string message);
}

public class EmailService : IMessageService
{
    public EmailService()
    {
        Console.WriteLine("EmailService instance created");
    }

    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

class Program
{
    static void Main()
    {
        // Step 1: Manually create a new instance each time
        IMessageService s1 = CreateEmailService();
        IMessageService s2 = CreateEmailService();

        // Step 2: Compare instances
        if (ReferenceEquals(s1, s2))
        {
            Console.WriteLine("Both are same instance");
        }
        else
        {
            Console.WriteLine("Different instances (Custom Transient)");
        }

        s1.Send("Hello from Custom Transient service!");
    }

    // Step 3: Method to create a new instance of EmailService (like AddTransient)
    static IMessageService CreateEmailService()
    {
        return new EmailService();
    }
}
