using System;
using Kafka.Consumer.Consumers;

namespace Kafka.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer Started !");

            var emailMessageConsumer = new EmailMessageConsumer();
            emailMessageConsumer.StartConsuming();
            
            Console.ReadLine();
        }
    }
}
