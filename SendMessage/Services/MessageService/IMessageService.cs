using SendMessage.Models;

namespace SendMessage.Services.MessageService
{
    public interface IMessageService
    {
        IEnumerable<Message> GetMessages(int resipient);
        void Add(Message message);
        void AddRange(IEnumerable<Message> messages);
    }
}
