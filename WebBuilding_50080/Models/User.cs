namespace WebBuilding_50080.Models
{
    public class User
    {

            public int userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string pass {  get; set; }



    }

    public class Customer : User
    {
        public string cardName { get; set; }
        public int cardNum { get; set; }
        public DateOnly cardDate { get; set; }
    }

    public class Manager : User
    {
    }

}

