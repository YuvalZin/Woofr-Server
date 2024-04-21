namespace woofr.Models
{
    public class Woof
    {
        private string id;
        private string content;
        private DateTime createdAt;
        private string userId;
        private string mediaId;

        public string Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public string UserId { get => userId; set => userId = value; }
        public string MediaId { get => mediaId; set => mediaId = value; }

        static public List<Woof> GetUserPostsById(string id)
        {
            DBservices dbs = new DBservices();
            List<Woof> results = dbs.GetUserPosts(id);
            if (results == null) throw new Exception("Error finding search results");
            else return results;
        }
    }
}
