using System;

namespace event_planner.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
