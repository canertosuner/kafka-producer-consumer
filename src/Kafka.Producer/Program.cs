using System;
using Kafka.Message;

namespace Kafka.Producer
{
    class Program
    {
        public static void Main(string[] args)
        {
            IMessageProducer messageProducer = new MessageProducer();

            //produce email message
            var emailMessage = new EmailMessage
            {
                Content = "Contoso Retail Daily News Email Content",
                Subject = "Contoso Retail Daily News",
                To = "all@contosoretail.com.tr"
            };
            messageProducer.Produce("emailmessage-topic", emailMessage);
            
            Console.ReadLine();
        }
    }
}
