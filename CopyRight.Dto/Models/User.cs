using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dto.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public bool IsActive { get; set; }


        public string Email { get; set; } = null!;

        public RoleCode? Role { get; set; }

        public string Password { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }
    }
}
