// Notification.cs
using DIExample.s;

namespace DIExample
{
    public class Notification
    {
        private readonly IMessageService _messageService;

        // Constructor Injection (automatic in ASP.NET Core)
        public Notification(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Notify(string message)
        {
            _messageService.Send(message);
        }
    }
}
