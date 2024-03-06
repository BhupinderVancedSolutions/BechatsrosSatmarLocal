using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation.DataBase
{
    public class BechatsrosSatmarEntityContext : DbContext
    {
        public BechatsrosSatmarEntityContext(DbContextOptions<BechatsrosSatmarEntityContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

    }
}
