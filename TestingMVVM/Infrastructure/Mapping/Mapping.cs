using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.ModelOfCountry;

namespace TestingMVVM.Infrastructure.Mapping
{
    public static class Mapping
    {
        public static Mapper MapMethod()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<MegaCountry, CountryModel>()
                                    .ForMember(CM => CM.name, MC => MC.MapFrom(m => m.CountryName))
                                    .ForMember(CM => CM.numericCode, MC => MC.MapFrom(m => m.CountryCode))
                                    .ForMember(CM => CM.capital, MC => MC.MapFrom(m => m.CapitalName))
                                    .ForMember(CM => CM.area, MC => MC.MapFrom(m => m.Area))
                                    .ForMember(CM => CM.population, MC => MC.MapFrom(m => m.CountryPopulation))
                                    .ForMember(CM => CM.region, MC => MC.MapFrom(m => m.RegionName)));

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
