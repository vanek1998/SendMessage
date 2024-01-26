namespace SendMessage.Models
{
    public class Message
    {
        protected string _subject;
        protected string _body;
        private readonly IEnumerable<int> _recipients;

        public string Subject => _subject;
        public string Body => _body;
        public IEnumerable<int> Recipients => _recipients;

        public Message(string subject, string body, IEnumerable<int> recipients) 
        {
            _subject = subject;
            _body = body;
            _recipients = recipients;
        }
    }
}
