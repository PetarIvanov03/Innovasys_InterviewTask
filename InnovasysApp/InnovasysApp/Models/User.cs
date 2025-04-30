using System.Net;

namespace InnovasysApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NotUsername { get; set; } // Обърни внимание: не е Username, а NotUsername
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Address Address { get; set; }
    }

}
