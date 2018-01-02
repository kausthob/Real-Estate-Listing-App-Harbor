using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Real_Estate_Listing.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Real_Estate_Listing.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Real_Estate_Listing.Models.RealEstateProperty> RealEstateProperties { get; set; }
    }
}