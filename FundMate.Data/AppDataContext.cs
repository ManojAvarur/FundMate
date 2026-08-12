using FundMate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FundMate.Data;

public class AppDataContext : DbContext
{
    public DbSet<Users> Users { get; set; }

    public DbSet<Relations> Relations { get; set; }

    public DbSet<Groups> Groups { get; set; }

    public DbSet<GroupUsers> GroupUsers { get; set; }

    public DbSet<GroupTransactions> GroupTransactions { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options): base(options) { }
}
