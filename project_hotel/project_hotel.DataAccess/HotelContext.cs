using Microsoft.EntityFrameworkCore;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions options = null) : base(options)
        {

        }

        public IApplicationUser User { get; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=hotel_asp;Integrated Security=True");

        //    base.OnConfiguring(optionBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User?.Username;
                            break;
                        case EntityState.Deleted:
                            e.DeletedAt = DateTime.UtcNow;
                            e.DeletedBy = User?.Username;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ApartmentEquipment> ApartmentEquipments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<ApartmentRoom> ApartmentRooms { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ApartmentImage> ApartmentImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<UserUseCase> UserUceCases { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
