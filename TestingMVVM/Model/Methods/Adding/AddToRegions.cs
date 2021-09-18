using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.Entities;
using TestingMVVM.Model.DataBase.DBContext;

namespace TestingMVVM.Model.Methods.Adding
{
    class AddToRegions
    {
        public void AddRegion(CountriesContext Context, string newRegion)
        {
            Region region = new Region()
            {
                RegionName = newRegion
            };

            Context.Regions.Add(region);
            Context.SaveChanges();
        }
    }
}
