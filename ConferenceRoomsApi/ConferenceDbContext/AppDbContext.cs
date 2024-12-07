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

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
            });

            modelBuilder.Entity<Service>()
           .HasMany(s => s.RoomServices)
           .WithOne(rs => rs.Service)
           .HasForeignKey(rs => rs.ServiceId);


            modelBuilder.Entity<RoomService>()
           .HasKey(rs => new { rs.RoomId, rs.ServiceId });
            
        }
    }
}
