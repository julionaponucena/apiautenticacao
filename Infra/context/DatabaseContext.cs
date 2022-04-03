using Microsoft.EntityFrameworkCore;
using Model;
namespace Infra.Context;
public class DatabaseContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite(@"dataSource=app.db;cache=Shared");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<User>().HasKey(user =>user.id);
        modelBuilder.Entity<User>().Property(user => user.isAdmin).HasDefaultValue(false);
        modelBuilder.Entity<User>().HasIndex(user =>user.username).IsUnique();

    }
    public DbSet <User>Users {get;set;} = default!;
}