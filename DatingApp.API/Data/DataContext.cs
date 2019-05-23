using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }    //Pass Value (Entity) to the DB. Values is the table Name in SQL

        public DbSet<User> Users { get; set; }

    }
}