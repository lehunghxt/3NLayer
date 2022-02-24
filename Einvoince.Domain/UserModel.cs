using System;

namespace Shop.Domain
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Nullable<int> Age { get; set; }
    }
}
