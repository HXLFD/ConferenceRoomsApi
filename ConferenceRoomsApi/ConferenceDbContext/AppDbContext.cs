using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;
using ConferenceRoomsApi.Models.Bookings;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomsApi.Models;


namespace ConferenceRoomsApi.ConferenceDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Rooms");

            // Настройка таблицы Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
            });

            // Настройка таблицы Service
            modelBuilder.Entity<Service>()
           .HasMany(s => s.RoomServices)
           .WithOne(rs => rs.Service)
           .HasForeignKey(rs => rs.ServiceId);


            // Настройка таблицы-связки RoomService
            modelBuilder.Entity<RoomService>()
           .HasKey(rs => new { rs.RoomId, rs.ServiceId });
            //    modelBuilder.Entity<RoomService>(entity =>
            //    {
            //        entity.ToTable("RoomServices");
            //        entity.HasKey(rs => new { rs.RoomId, rs.ServiceId });

            //        entity.HasOne(rs => rs.Room)
            //              .WithMany(r => r.RoomServices)
            //              .HasForeignKey(rs => rs.RoomId)
            //              .OnDelete(DeleteBehavior.Cascade);

            //        entity.HasOne(rs => rs.Service)
            //              .WithMany(s => s.RoomServices)
            //              .HasForeignKey(rs => rs.ServiceId)
            //              .OnDelete(DeleteBehavior.Cascade);
            //    });

            //    modelBuilder.Entity<RoomService>()
            //.HasKey(rs => new { rs.RoomId, rs.ServiceId });

            //    modelBuilder.Entity<RoomService>()
            //        .HasOne(rs => rs.Room)
            //        .WithMany(r => r.RoomServices)
            //        .HasForeignKey(rs => rs.RoomId)
            //        .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            //    modelBuilder.Entity<RoomService>()
            //        .HasOne(rs => rs.Service)
            //        .WithMany(s => s.RoomServices)
            //        .HasForeignKey(rs => rs.ServiceId);
        }
    }
}
// --project C:\Users\ARW10\source\repos\ConferenceRoomsApi\ConferenceRoomsApi\ConferenceRoomsApi.csproj