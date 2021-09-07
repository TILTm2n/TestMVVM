using System;
using System.Collections.Generic;

#nullable disable

namespace TestingMVVM.Model.DataBase.Entities
{
    public partial class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int? Capital { get; set; }
        public decimal? Area { get; set; }
        public int? CountryPopulation { get; set; }
        public int? Region { get; set; }

        public virtual City CapitalNavigation { get; set; }
        public virtual Region RegionNavigation { get; set; }
    }
}
