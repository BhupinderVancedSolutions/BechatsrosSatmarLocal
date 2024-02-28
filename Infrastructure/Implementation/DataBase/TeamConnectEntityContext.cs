using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation.DataBase
{
    public class TeamConnectEntityContext : DbContext
    {
        public TeamConnectEntityContext(DbContextOptions<TeamConnectEntityContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentProvider> PaymentProviders { get; set; }

    }
}
