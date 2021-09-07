using System;
using System.Collections.Generic;

#nullable disable

namespace TestingMVVM.Model.DataBase.Entities
{
    public partial class Region
    {
        public Region()
        {
            Countries = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
