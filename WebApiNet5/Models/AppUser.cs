using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNet5.Models
{
    public class AppUser : IdentityUser
    {
        //Custom propertys
        //if change - create new migration
        //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
        //add-migration -context AppUserDbContext MirgatinName
        public string Nickname { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
