using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions Options) : base(Options)
    {

    }
    public DbSet<AppUser> Users { get; set; }
     public DbSet<Photo> Photos { get; set; }
}