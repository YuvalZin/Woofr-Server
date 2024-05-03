namespace woofr.Models
{
    public class Pet
    {
        private string id;
        private string userId;
        private string name;
        //private string type;
        private int birthYear;
        private string breed;
        private string bio;
        private string imageUrl;

        public string Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public string Name { get => name; set => name = value; }
        //public string Type { get => type; set => type = value; }
        public int BirthYear { get => birthYear; set => birthYear = value; }
        public string Breed { get => breed; set => breed = value; }
        public string Bio { get => bio; set => bio = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }

        public bool InsertPet()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertPet(this);
            if (rowsAff > 0) return true;
            throw new Exception("Error inserting pet");
        }
        static public bool Delete(string id)
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.DeletePet(id);
            if (rowsAff > 0) return true;
            throw new Exception("Error deleting pet");
        }

        static public List<Pet> GetUserPetsById(string id)
        {
            DBservices dbs = new DBservices();
            List<Pet> results = dbs.GetUserPets(id);
            if (results == null) throw new Exception("Error finding pets");
            else return results;
        }
    }

 }
