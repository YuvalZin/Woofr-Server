namespace woofr.Models
{
    public class User
    {
        private int userId; 
        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private string password; 
        private string gender;
        //private string profilePictureUrl; 
        //private string bio; 
        private DateTime birthday;

        public int UserId { get => userId;}
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Gender { get => gender; set => gender = value; }
        //public string ProfilePictureUrl { get => profilePictureUrl; set => profilePictureUrl = value; }
        //public string Bio { get => bio; set => bio = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }


        public string RegisterUser()
        {
            DBservices dbs = new DBservices();
            string token = dbs.RegisterUser(this);
            if (token == null) throw new Exception("We couldn't register an account with the email and password you provided. Please check your details and try again.");
            else return token;
            
        }
        public int UploadImage(string token, byte[] image)
        {

            DBservices dbs = new DBservices();
            int rowsAff = dbs.UploadImage(token,image);
            return rowsAff;
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
