using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomsApi.Models;


namespace ConferenceRoomsApi.ConferenceDbContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            // Таблица Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(r => r.Id);
            });

            // Таблица Service
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(s => s.Id);
            });

            // Таблица RoomService
            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.ToTable("RoomService");
                entity.HasKey(rs => new { rs.RoomId, rs.ServiceId });

                entity.HasOne(rs => rs.Room)
                    .WithMany(r => r.RoomServices)
                    .HasForeignKey(rs => rs.RoomId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rs => rs.Service)
                    .WithMany(s => s.RoomServices)
                    .HasForeignKey(rs => rs.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
