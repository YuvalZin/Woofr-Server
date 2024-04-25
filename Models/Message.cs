namespace woofr.Models
{
    public class Message
    {
        private string messageId;
        private string chatId;
        private string senderId;
        private string receiverId;
        private string messageText;
        private DateTime timestamp;

        public string MessageId { get => messageId; set => messageId = value; }
        public string ChatId { get => chatId; set => chatId = value; }
        public string SenderId { get => senderId; set => senderId = value; }
        public string ReceiverId { get => receiverId; set => receiverId = value; }
        public string MessageText { get => messageText; set => messageText = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }

        static public List<Message> GetChatMessages(string id)
        {
            DBservices dbs = new DBservices();
            List<Message> results = dbs.GetChatMessages(id);
            if (results == null) throw new Exception("Error finding chat messages");
            else return results;
        }

    }
}
