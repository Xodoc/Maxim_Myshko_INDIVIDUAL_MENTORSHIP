using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
