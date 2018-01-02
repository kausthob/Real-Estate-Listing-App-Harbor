using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Real_Estate_Listing.Models
{
    public class RealEstateProperty
    {
        public int id { get; set; }
        public String Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public decimal? OriginalSaleAmount { get; set; }
        public decimal? SoldAmount { get; set; }
        public DateTime? SaleDate { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}