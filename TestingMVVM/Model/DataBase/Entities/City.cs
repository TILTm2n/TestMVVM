using System;
using System.Collections.Generic;
using System.Windows.Input;

#nullable disable

namespace TestingMVVM.Model.DataBase.Entities
{
    public partial class City
    {
        public City()
        {
            Countries = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
