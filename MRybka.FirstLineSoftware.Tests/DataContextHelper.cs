using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Tests
{
    public static class DataContextHelper
    {
        public static DataContext InMemorySqlite()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<DataContext>().UseSqlite(connection).Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            return dataContext;
        }
    }
}
