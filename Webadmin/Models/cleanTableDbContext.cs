using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Webadmin.Models
{
    public partial class cleanTableDbContext : DbContext
    {
        public cleanTableDbContext()
        {
        }

        public cleanTableDbContext(DbContextOptions<cleanTableDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminLocation> AdminLocations { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingAttendee> BookingAttendees { get; set; }
        public virtual DbSet<BookingLocation> BookingLocations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<OpeningTime> OpeningTimes { get; set; }
        public virtual DbSet<StaffPosition> StaffPositions { get; set; }
        public virtual DbSet<StaffShift> StaffShifts { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cleanTableDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admins");

                entity.HasIndex(e => e.AdminUsername, "UQ__admins__0CEDD55F8F0E072A")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminLevel)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("admin_level");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("admin_password");

                entity.Property(e => e.AdminUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("admin_username");
            });

            modelBuilder.Entity<AdminLocation>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.AdminId })
                    .HasName("PK__admin_lo__86921A99E0133B36");

                entity.ToTable("admin_locations");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminLocations)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__admin_loc__admin__4222D4EF");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.AdminLocations)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__admin_loc__venue__412EB0B6");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingSize).HasColumnName("booking_size");

                entity.Property(e => e.BookingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("booking_time");
            });

            modelBuilder.Entity<BookingAttendee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("booking_attendees");

                entity.Property(e => e.BookingAttended).HasColumnName("booking_attended");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.Booking)
                    .WithMany()
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__booking_a__booki__3A81B327");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__booking_a__custo__3B75D760");
            });

            modelBuilder.Entity<BookingLocation>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.BookingId })
                    .HasName("PK__booking___877684D66B78553C");

                entity.ToTable("booking_locations");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingLocations)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__booking_l__booki__34C8D9D1");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.BookingLocations)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__booking_l__venue__33D4B598");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerUsername, "UQ__customer__64E4CB014C3FBEC7")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerContactNumber).HasColumnName("customer_contact_number");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customer_password");

                entity.Property(e => e.CustomerUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_username");
            });

            modelBuilder.Entity<Employment>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.StaffId })
                    .HasName("PK__employme__533E8354AE10DF90");

                entity.ToTable("employment");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Employments)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__employmen__staff__2E1BDC42");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Employments)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__employmen__venue__2D27B809");
            });

            modelBuilder.Entity<OpeningTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("opening_times");

                entity.Property(e => e.VenueClosingTime).HasColumnName("venue_closing_time");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueOpeningTime).HasColumnName("venue_opening_time");

                entity.Property(e => e.VenueTimeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("venue_time_id");
            });

            modelBuilder.Entity<StaffPosition>(entity =>
            {
                entity.ToTable("staff_positions");

                entity.Property(e => e.StaffPositionId).HasColumnName("staff_position_id");

                entity.Property(e => e.StaffPositionName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("staff_position_name");
            });

            modelBuilder.Entity<StaffShift>(entity =>
            {
                entity.ToTable("staff_shifts");

                entity.Property(e => e.StaffShiftId).HasColumnName("staff_shift_id");

                entity.Property(e => e.StaffEndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("staff_end_time");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StaffStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("staff_start_time");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffShifts)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__staff_shi__staff__2A4B4B5E");
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.ToTable("venues");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.AddLineOne)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("add_line_one");

                entity.Property(e => e.AddLineTwo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("add_line_two");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("county");

                entity.Property(e => e.VenueName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("venue_name");

                entity.Property(e => e.VenuePostcode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("venue_postcode");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StaffContactNum).HasColumnName("staff_contact_num");

                entity.Property(e => e.StaffName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("staff_name");

                entity.Property(e => e.StaffPositionId).HasColumnName("staff_position_id");

                entity.HasOne(d => d.StaffPosition)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.StaffPositionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__staff__staff_pos__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
