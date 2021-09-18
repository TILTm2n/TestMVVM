using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.DataBase.DBContext;

namespace TestingMVVM.Model.Methods.Adding
{
    class AddToCities
    {
        public void AddCity(CountriesContext Context, string newCapital)
        {
            City capital = new City()
            {
                CityName = newCapital
            };

            Context.Cities.Add(capital);
            Context.SaveChanges();
        }
    }
}
