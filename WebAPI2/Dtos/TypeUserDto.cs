using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos
{
    public class TypeUserDto
    {
        public int TypeUserId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
    }
}
