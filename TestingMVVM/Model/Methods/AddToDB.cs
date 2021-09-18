using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.DBContext;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.Methods.Adding;
using TestingMVVM.Model.ModelOfCountry;

namespace TestingMVVM.Model.Methods
{
    class AddToDB
    {
        public void AddToDb(IEnumerable<CountryModel> someCountries)
        {
            using (CountriesContext db = new CountriesContext())
            {
                try
                {
                    var pickedCountry = someCountries.Where(c => c.IsPicked);

                    foreach (var c in pickedCountry)
                    {
                        AddToCities addToCities = new AddToCities();
                        AddToRegions addToRegions = new AddToRegions();
                        AddToCountries addToCountries = new AddToCountries();

                        City capitalData = db.Cities.FirstOrDefault(x => x.CityName == c.capital);
                        Region regionData = db.Regions.FirstOrDefault(y => y.RegionName == c.region);
                        Country countryData = db.Countries.FirstOrDefault(z => z.CountryCode == c.numericCode);

                        if (capitalData == null)
                        {
                            addToCities.AddCity(db, c.capital);

                            capitalData = db.Cities.FirstOrDefault(x => x.CityName == c.capital);
                        }

                        if (regionData == null)
                        {
                            addToRegions.AddRegion(db, c.region);

                            regionData = db.Regions.FirstOrDefault(y => y.RegionName == c.region);
                        }

                        if (countryData == null && capitalData != null && regionData != null)
                        {
                            addToCountries.AddCountry(db, c.name, c.numericCode, c.population, (decimal?)c.area, capitalData.Id, regionData.Id);
                        }
                        else
                        {
                            db.Countries.Update(countryData);
                        }
                    }
                }
                catch (ArgumentNullException ex)
                {
                    throw new Exception($"Сначала необходимо выбрать страну!!! \n{ex.Message}");
                }
            }
        }
    }
}
