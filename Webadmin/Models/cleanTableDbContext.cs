using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Webadmin.Models;
using Microsoft.Data.SqlClient;

namespace Webadmin.Models
{
    public partial class cleanTableDbContext : DbContext
    {
        public SessionContextInterceptor Interceptor { get; set; }


        public cleanTableDbContext()
        {
        }

        public cleanTableDbContext(DbContextOptions<cleanTableDbContext> options)
            : base(options)
        {
            SessionContextInterceptor interceptor = new SessionContextInterceptor();
            this.Interceptor = interceptor;
        }

        public virtual DbSet<AdminLocations> AdminLocations { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<BookingAttendees> BookingAttendees { get; set; }
        public virtual DbSet<BookingLocations> BookingLocations { get; set; }
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employment> Employment { get; set; }
        public virtual DbSet<OpeningTimes> OpeningTimes { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffPositions> StaffPositions { get; set; }
        public virtual DbSet<StaffShifts> StaffShifts { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }
        public virtual DbSet<Flags> Flags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(Interceptor);
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cleanTableDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { //------------------------ Flags

            modelBuilder.Entity<Flags>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PK__FlagID");
                entity.ToTable("Flags");

                entity.Property(e => e.ID).HasColumnName("id");
                entity.Property(e => e.FlagTitle).HasColumnName("flag_title");
                entity.Property(e => e.FlagLocationPage).HasColumnName("flag_location_page");
                entity.Property(e => e.FlagCategory).HasColumnName("flag_category");
                entity.Property(e => e.FlagPersistent).HasColumnName("flag_persistent");
                entity.Property(e => e.FlagUrgency).HasColumnName("flag_urgency");
                entity.Property(e => e.FlagDesc).HasColumnName("flag_desc");
                entity.Property(e => e.FlagVenueID).HasColumnName("flag_venue_id");
                entity.Property(e => e.FlagDate).HasColumnName("flag_date");
                entity.Property(e => e.FlagResolved).HasColumnName("flag_resolved");


            });

            //------------------------


            modelBuilder.Entity<AdminLocations>(entity =>
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

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__admins__43AA4141D6F72DBB");

                entity.ToTable("admins");

                entity.HasIndex(e => e.AdminUsername)
                    .HasName("UQ__admins__0CEDD55F8F0E072A")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminLevel)
                    .IsRequired()
                    .HasColumnName("admin_level")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AdminSalt)
                    .IsRequired()
                    .HasColumnName("admin_salt")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("admin_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AdminUsername)
                    .IsRequired()
                    .HasColumnName("admin_username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookingAttendees>(entity =>
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

            modelBuilder.Entity<BookingLocations>(entity =>
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

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__bookings__5DE3A5B10A626279");

                entity.ToTable("bookings");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingSize).HasColumnName("booking_size");

                entity.Property(e => e.BookingTime)
                    .HasColumnName("booking_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB8511777C82");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerUsername)
                    .HasName("UQ__customer__64E4CB014C3FBEC7")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerContactNumber).HasColumnName("customer_contact_number");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customer_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasColumnName("customer_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerUsername)
                    .IsRequired()
                    .HasColumnName("customer_username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employment>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.StaffId })
                    .HasName("PK__employme__533E8354AE10DF90");

                entity.ToTable("employment");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Employment)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__employmen__staff__2E1BDC42");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Employment)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__employmen__venue__2D27B809");
            });

            modelBuilder.Entity<OpeningTimes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("opening_times");

                entity.Property(e => e.VenueClosingTime).HasColumnName("venue_closing_time");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueOpeningTime).HasColumnName("venue_opening_time");

                entity.Property(e => e.VenueTimeId)
                    .HasColumnName("venue_time_id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StaffContactNum).HasColumnName("staff_contact_num");

                entity.Property(e => e.StaffName)
                    .HasColumnName("staff_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffPositionId).HasColumnName("staff_position_id");

                entity.HasOne(d => d.StaffPosition)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StaffPositionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__staff__staff_pos__276EDEB3");
            });

            modelBuilder.Entity<StaffPositions>(entity =>
            {
                entity.HasKey(e => e.StaffPositionId)
                    .HasName("PK__staff_po__6E04F9C6D291DDF7");

                entity.ToTable("staff_positions");

                entity.Property(e => e.StaffPositionId).HasColumnName("staff_position_id");

                entity.Property(e => e.StaffPositionName)
                    .HasColumnName("staff_position_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffShifts>(entity =>
            {
                entity.HasKey(e => e.StaffShiftId)
                    .HasName("PK__staff_sh__488620F3C5DEA45E");

                entity.ToTable("staff_shifts");

                entity.Property(e => e.StaffShiftId).HasColumnName("staff_shift_id");

                entity.Property(e => e.StaffEndTime)
                    .HasColumnName("staff_end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StaffStartTime)
                    .HasColumnName("staff_start_time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffShifts)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__staff_shi__staff__2A4B4B5E");
            });

            modelBuilder.Entity<Venues>(entity =>
            {
                entity.HasKey(e => e.VenueId)
                    .HasName("PK__venues__82A8BE8D9F10979F");

                entity.ToTable("venues");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.AddLineOne)
                    .IsRequired()
                    .HasColumnName("add_line_one")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddLineTwo)
                    .IsRequired()
                    .HasColumnName("add_line_two")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasColumnName("county")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VenueName)
                    .IsRequired()
                    .HasColumnName("venue_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VenuePostcode)
                    .IsRequired()
                    .HasColumnName("venue_postcode")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Webadmin.Models.VenueTables> VenueTables { get; set; }
    }
}
