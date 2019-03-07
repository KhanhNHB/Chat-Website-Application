using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat
{
    public class UserDTO
    {
        public string ConnectionID { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public bool Role { get; set; }
        public string LoginTime { get; set; }
    }
}
