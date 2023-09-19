using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.DataAccess
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
