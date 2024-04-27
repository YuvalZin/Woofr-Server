namespace woofr.Models
{
    public class Chat
    {
        private string chatID;
        private string participant1ID;
        private string participant2ID;
        private int participant1UnreadCount;
        private int participant2UnreadCount;
        private string lastMessage;
        private DateTime timestamp;

        public string ChatID { get => chatID; set => chatID = value; }
        public string Participant1ID { get => participant1ID; set => participant1ID = value; }
        public string Participant2ID { get => participant2ID; set => participant2ID = value; }
        public int Participant1UnreadCount { get => participant1UnreadCount; set => participant1UnreadCount = value; }
        public int Participant2UnreadCount { get => participant2UnreadCount; set => participant2UnreadCount = value; }
        public string LastMessage { get => lastMessage; set => lastMessage = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }

        static public List<Chat> GetUsersChat(string id)
        {
            DBservices dbs = new DBservices();
            List<Chat> results = dbs.GetUsersChat(id);
            if (results == null) throw new Exception("Error finding chats");
            else return results;
        }

        public Chat StartChat()
        {
            DBservices dbs = new DBservices();
            Chat chatId = dbs.StartChat(this);
            return chatId;
            throw new Exception("Error starting new chat");
        }
    }
}
