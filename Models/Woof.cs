namespace woofr.Models
{
    public class Woof
    {
        private string id;
        private string content;
        private DateTime createdAt;
        private string userId;
        private string mediaUrl;

        public string Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public string UserId { get => userId; set => userId = value; }
        public string MediaUrl { get => mediaUrl; set => mediaUrl = value; }

        public bool InsertPost()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertPost(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error finding post");
        }
         static public bool Delete(string id)
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.Delete(id);
            if (rowsAff > 0) return true;
            throw new Exception("Error finding post");
        }

        static public List<Woof> GetUserPostsById(string id)
        {
            DBservices dbs = new DBservices();
            List<Woof> results = dbs.GetUserPosts(id);
            if (results == null) throw new Exception("Error finding search results");
            else return results;
        }
        
        static public bool LikePost(string post_id, string user_id)
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.LikePost(post_id,user_id);
            if (rowsAff > 0) return true;
            throw new Exception("Invalid like procedere");

        }
        
        static public List<Woof> GetHomePagePostsById(string id)
        {
            DBservices dbs = new DBservices();
            List<Woof> results = dbs.GetHomePagePosts(id);
            if (results == null) throw new Exception("Error finding search results");
            else return results;
        }
    }
}
