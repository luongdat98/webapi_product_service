using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int IsActive { get; set; }
        public int TypeUserId { get; set; }

        public TypeUser TypeUser { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
