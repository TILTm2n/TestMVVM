using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.DataBase.DBContext;

namespace TestingMVVM.Model.Methods.Adding
{
    class AddToCountries
    {
        public void AddCountry(CountriesContext Context, string name, string numCode, int population, decimal? area, int? capitalId, int? regionId)
        {
            Country country = new Country()
            {
                CountryName = name,
                CountryCode = numCode,
                CountryPopulation = population,
                Area = area,
                Capital = capitalId,
                Region = regionId
            };

            Context.Countries.Add(country);
            Context.SaveChanges();
        }
    }
}
