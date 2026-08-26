using Microsoft.EntityFrameworkCore;
using Sistema_de_cuenta_de_ahorros.Infrastructure.Modelo;
namespace Sistema_de_cuenta_de_ahorros.Infrastructure.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transacciones => Set<Transaction>();
        public DbSet<Balance> Balance => Set<Balance>();
    }
}
