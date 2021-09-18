using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace TestingMVVM.Model.DataBase.ConnStr
{
    public static class ConnectionStringCreated
    {
        public static string CreateConnectionString(string DataSource, string Catalog)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                        .Build();

            var codeFirstConnectionString = config.GetSection("ConnectionStrings")["CountriesContext"];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(codeFirstConnectionString);

            builder.DataSource = DataSource;
            builder.InitialCatalog = Catalog;

            return builder.ConnectionString;
        }

        

    }
}
