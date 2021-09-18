using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Model.DataBase.DBContext;

namespace TestingMVVM.Model.Methods
{
    public class DeleteData
    {
        public void DeleteAllData()
        {
            using (CountriesContext db = new CountriesContext())
            {
                db.Database.ExecuteSqlRaw("DELETE FROM Country");
                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Country, RESEED, 0)");

                db.Database.ExecuteSqlRaw("DELETE FROM Cities");
                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Cities, RESEED, 0)");

                db.Database.ExecuteSqlRaw("DELETE FROM Regions");
                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Regions, RESEED, 0)");

                db.SaveChanges();
            }
        }
    }
}
