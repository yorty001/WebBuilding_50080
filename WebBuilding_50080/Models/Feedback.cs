using Microsoft.AspNetCore.Mvc;

namespace WebBuilding_50080.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int CusID { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public int Rating { get; set; }
    }
}
