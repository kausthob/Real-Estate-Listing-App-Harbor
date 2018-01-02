using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Real_Estate_Listing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public String name { get; set; }
        public virtual List<RealEstateProperty> RealEstateProperties { get; set; }
    }
}