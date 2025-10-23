namespace DIExample.s
{
    public class SmsService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine("📱 SMS sent: " + message);
        }
    }
}