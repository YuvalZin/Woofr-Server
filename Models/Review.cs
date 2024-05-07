namespace woofr.Models
{
    public class Review
    {
        private string id;
        private string proUserId;
        private string userId;
        private int rating;
        private string reviewText;
        private DateTime datePosted;

        public string Id { get => id; set => id = value; }
        public string ProUserId { get => proUserId; set => proUserId = value; }
        public string UserId { get => userId; set => userId = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public DateTime DatePosted { get => datePosted; set => datePosted = value; }

        static public List<Review> GetReviews(string id)
        {
            DBservices dbs = new DBservices();
            List<Review> results = dbs.GetReviewsByProUserId(id);
            if (results == null) throw new Exception("Error geting user reviews");
            else return results;
        }

        public bool InsertReview()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertReview(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error inserting review");
        }
        static public bool Delete(string id)
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.DeleteReview(id);
            if (rowsAff > 0) return true;
            throw new Exception("Error delete post");
        }

    }
}
