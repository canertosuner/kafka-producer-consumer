using System;
using Confluent.Kafka;
using Kafka.Message;
using Newtonsoft.Json;

namespace Kafka.Producer
{
    public class MessageProducer : IMessageProducer
    {
        public void Produce(string topic, IMessageBase message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var producer = new Producer<Null, string>(config))
            {
                var textMessage = JsonConvert.SerializeObject(message);

                producer.BeginProduce(topic, new Message<Null, string> { Value = textMessage }, OnDelivery);
            }
        }

        private void OnDelivery(DeliveryReportResult<Null, string> r)
        {
            Console.WriteLine(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery Error: {r.Error.Reason}");
        }
    }
}
