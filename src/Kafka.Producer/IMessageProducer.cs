using Kafka.Message;

namespace Kafka.Producer
{
    public interface IMessageProducer
    {
        void Produce(string topic, IMessageBase message);
    }
}
