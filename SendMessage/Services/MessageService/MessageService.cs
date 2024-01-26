using SendMessage.Models;

namespace SendMessage.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private Queue<Message> _messages;

        public MessageService()
        {
            _messages = new Queue<Message>();
        }

        public IEnumerable<Message> GetMessages(int recipient)
            => _messages.Where(message => message.Recipients.Contains(recipient));

        public void AddRange(IEnumerable<Message> messages)
            => messages.Foreach(Add);

        public void Add(Message message)
            => _messages.Enqueue(message);
    }
}
