namespace Kafka.Message
{
    public class EmailMessage:IMessageBase
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
