using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.DBContext;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.ModelOfCountry;
using TestingMVVM.Infrastructure.Mapping;
using AutoMapper;

namespace TestingMVVM.Model.Methods
{
    public class ShowData
    {
        public ObservableCollection<CountryModel> ShowAllData()
        {
            using (CountriesContext db = new CountriesContext())
            {
                var dbCollection = new ObservableCollection<Country>(db.Countries.Include(c => c.CapitalNavigation).Include(c => c.RegionNavigation).ToList());

                var mappedCountries = new List<CountryModel>();

                foreach(Country c in dbCollection)
                {
                    var mapper = Mapping.MapMethod();

                    CountryModel newCountryModel = mapper.Map<MegaCountry, CountryModel>(new MegaCountry
                    {
                        CountryName = c.CountryName,
                        CountryCode = c.CountryCode,
                        CapitalName = c.CapitalNavigation.CityName,
                        Area = c.Area,
                        CountryPopulation = c.CountryPopulation,
                        RegionName = c.RegionNavigation.RegionName
                    });

                    mappedCountries.Add(newCountryModel);
                }

                var mappedCollection = new ObservableCollection<CountryModel>(mappedCountries);

                return mappedCollection;
            }
        }
    }
}
