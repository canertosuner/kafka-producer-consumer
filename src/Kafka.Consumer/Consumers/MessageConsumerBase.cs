using System;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka.Consumer.Consumers
{
    public abstract class MessageConsumerBase<IMessage>
    {
        private readonly string _topic;

        protected MessageConsumerBase(string topic)
        {
            this._topic = topic;
        }

        public void StartConsuming()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "emailmessage-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetResetType.Earliest
            };

            using (var consumer = new Consumer<Ignore, string>(conf))
            {
                consumer.Subscribe(_topic);

                var keepConsuming = true;
                consumer.OnError += (_, e) => keepConsuming = !e.IsFatal;

                while (keepConsuming)
                {
                    try
                    {
                        var consumedTextMessage = consumer.Consume();
                        Console.WriteLine($"Consumed message '{consumedTextMessage.Value}' Topic: '{consumedTextMessage.Topic}'.");

                        var message = JsonConvert.DeserializeObject<IMessage>(consumedTextMessage.Value);

                        OnMessageDelivered(message);
                    }
                    catch (ConsumeException e)
                    {
                        OnErrorOccured(e.Error);
                    }
                }

                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                consumer.Close();
            }
        }

        public abstract void OnMessageDelivered(IMessage message);

        public abstract void OnErrorOccured(Error error);
    }
}
