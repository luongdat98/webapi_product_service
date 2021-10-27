using System.Collections.Generic;

namespace WebAPI2.Models
{
    public class TypeUser
    {
        public int TypeUserId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public  ICollection<User> Users { get; set; }
    }
}