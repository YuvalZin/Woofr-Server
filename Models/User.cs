namespace woofr.Models
{
    public class User
    {
        private string id; 
        private string firstName;
        private string lastName;
        private string email;
        private string password; 
        private string gender;
        private string profilePictureUrl;
        //private string bio; 
        private DateTime birthday;
        private string token;

        public string Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ProfilePictureUrl { get => profilePictureUrl; set => profilePictureUrl = value; }
        //public string Bio { get => bio; set => bio = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Token { get => token; set => token = value; }

        public string RegisterUser()
        {
            DBservices dbs = new DBservices();
            string token = dbs.RegisterUser(this);
            if (token == null) throw new Exception("We couldn't register an account with the email and password you provided. Please check your details and try again.");
            else return token;
            
        }
        public User UploadProfileImage(string id, string imageURL)
        {

            DBservices dbs = new DBservices();
            User u = dbs.UploadImage(id,imageURL);
            if (u == null) throw new Exception("Error uploading photo");
            else return u;
        }
       public string GetFollowCount(string token)
        {
            DBservices dbs = new DBservices();
            string followData = dbs.GetFollowCount(token);
            if (followData == null) throw new Exception("Error getting user data");
            else return followData;
        }

         public List<string> SearchUsers(string keyword)
        {
            DBservices dbs = new DBservices();
            List<string> results = dbs.SearchUsers(keyword);
            if (results == null) throw new Exception("Error finding search results");
            else return results;
        }

        public User GetUser(string token)
        {
            DBservices dbs = new DBservices();
            User userData = dbs.GetUser(token);
            if (userData == null) throw new Exception("Error getting user data");
            else return userData;
        }

      
        public string LogIn(string email, string password)
        {
            DBservices dbs = new DBservices();
            string token = dbs.LogIn(email, password);
            if (token == null) throw new Exception("We couldn't find an account with the email and password you provided. Please check your details and try again.");
            else return token;
        }

        //public DateTime RegistrationDate { get; set; }
        //public DateTime LastLoginDate { get; set; }
        //public AccountStatus Status { get; set; } // Enum: Active, Suspended, Deleted
    }
}
