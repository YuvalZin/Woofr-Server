namespace woofr.Models
{
    public class Professional
    {
        private string id;
        private string userId;
        private string displayName;
        private string? city;
        private string address;
        private string phone;
        private string profileImage;
        private string description;
        private string? type;
        private int ratingScore;
        private bool? availability24_7; // Nullable<bool>
        private bool? sellsProducts; // Nullable<bool>
        private bool? toHome; // Nullable<bool>
        private bool activeWoofr;
        private string notes;
        private string verificationStatus;

        public string Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string ProfileImage { get => profileImage; set => profileImage = value; }
        public string Description { get => description; set => description = value; }
        public string? Type { get => type; set => type = value; }
        public int RatingScore { get => ratingScore; set => ratingScore = value; }
        public bool? Availability24_7 { get => availability24_7; set => availability24_7 = value; }
        public bool? SellsProducts { get => sellsProducts; set => sellsProducts = value; }
        public bool? ToHome { get => toHome; set => toHome = value; }
        public string Notes { get => notes; set => notes = value; }
        public string VerificationStatus { get => verificationStatus; set => verificationStatus = value; }
        public bool ActiveWoofr { get => activeWoofr; set => activeWoofr = value; }
        public string? City { get => city; set => city = value; }
        public string UserId { get => userId; set => userId = value; }

        public bool RegisterProfessional()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.RegisterProfessional(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error adding professional");
        }

        public bool UpdateProfessional()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.UpdateProffesional(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error updating professional");
        }

        public List<Professional> GetProfessionals()
        {
            DBservices dbs = new DBservices();
            List<Professional> results = dbs.GetVerifiedProfessionals(this);
            if (results == null) throw new Exception("Error finding professionals results");
            else return results;
        }
         static public Professional GetProfessionalById(string id)
        {
            DBservices dbs = new DBservices();
            Professional result = dbs.GetVerifiedProfessionalById(id);
            if (result == null) return null;
            else return result;
        }
        static public List<Professional> GetProfessionalsHomePage()
        {
            DBservices dbs = new DBservices();
            List<Professional> result = dbs.GetProffesionalsForHomePage();
            if (result == null) throw new Exception("Error get professional for home page data");
            else return result;
        }
        static public bool Delete(string id)
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.DeleteProfessional(id);
            if (rowsAff > 0) return true;
            throw new Exception("Error delete post");
        }


    }
}
