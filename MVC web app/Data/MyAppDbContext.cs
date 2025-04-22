using Microsoft.EntityFrameworkCore;
using MVC_web_app.Models;

namespace MVC_web_app.Data
{
    public class MyAppDbContext:DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemClient>()
                .HasKey(tu => new { tu.ItemId, tu.ClientId });

            modelBuilder.Entity<ItemClient>()
                .HasOne(tc => tc.Item)
                .WithMany(t => t.ItemClients)
                .HasForeignKey(tc => tc.ItemId);

            modelBuilder.Entity<ItemClient>()
                .HasOne(tc => tc.Client)
                .WithMany(t => t.ItemClients)
                .HasForeignKey(tc => tc.ClientId);

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 5, Name = "Pen", Price = 150.0, SerialNumberId = 1, CategoryId=1 }
                );

            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 1, Name = "MIC010", ItemId = 5 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="library" },
                new Category { Id=2, Name="electronics" }
                );


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
       

}
