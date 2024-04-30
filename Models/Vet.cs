namespace woofr.Models
{
    public class Vet
    {
        private string id;
        private string userId;
        private string displayName;
        private string? city;
        private string address;
        private string phone;
        private string profileImage;
        private string description;
        private string? specialization;
        private int ratingScore;
        private bool? availability24_7; // Nullable<bool>
        private bool? sellsProducts; // Nullable<bool>
        private bool? vetToHome; // Nullable<bool>
        private bool activeWoofr;
        private string notes;
        private string verificationStatus;

        public string Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string ProfileImage { get => profileImage; set => profileImage = value; }
        public string Description { get => description; set => description = value; }
        public string? Specialization { get => specialization; set => specialization = value; }
        public int RatingScore { get => ratingScore; set => ratingScore = value; }
        public bool? Availability24_7 { get => availability24_7; set => availability24_7 = value; }
        public bool? SellsProducts { get => sellsProducts; set => sellsProducts = value; }
        public bool? VetToHome { get => vetToHome; set => vetToHome = value; }
        public string Notes { get => notes; set => notes = value; }
        public string VerificationStatus { get => verificationStatus; set => verificationStatus = value; }
        public bool ActiveWoofr { get => activeWoofr; set => activeWoofr = value; }
        public string? City { get => city; set => city = value; }
        public string UserId { get => userId; set => userId = value; }

        public bool RegisterVet()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.RegisterVet(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error adding vet");
        }

        public List<Vet> GetVets()
        {
            DBservices dbs = new DBservices();
            List<Vet> results = dbs.GetVerifiedVets(this);
            if (results == null) throw new Exception("Error finding vets results");
            else return results;
        }
         static public Vet GetVetById(string id)
        {
            DBservices dbs = new DBservices();
            Vet result = dbs.GetVerifiedVetById(id);
            if (result == null) throw new Exception("Error finding vet data");
            else return result;
        }


    }
}
