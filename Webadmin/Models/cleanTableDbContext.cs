using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employment> Employment { get; set; }
        public virtual DbSet<Flags> Flags { get; set; }
        public virtual DbSet<OpeningTimes> OpeningTimes { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffShifts> StaffShifts { get; set; }
        public virtual DbSet<VenueTables> VenueTables { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cleanTableDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminLocations>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.AdminId })
                    .HasName("PK__admin_lo__86921A994ABEC73A");

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
                    .HasName("PK__admins__43AA4141065ED78C");

                entity.ToTable("admins");

                entity.HasIndex(e => e.AdminUsername)
                    .HasName("UQ__admins__0CEDD55FF35CEE82")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminLevel)
                    .IsRequired()
                    .HasColumnName("admin_level")
                    .HasMaxLength(15)
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
                    .HasConstraintName("FK__booking_a__booki__4E88ABD4");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__booking_a__custo__4F7CD00D");
            });

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__bookings__5DE3A5B1987AA430");

                entity.ToTable("bookings");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingSize).HasColumnName("booking_size");

                entity.Property(e => e.BookingTime)
                    .HasColumnName("booking_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueTableId).HasColumnName("venue_table_id");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookings__venue___4AB81AF0");

                entity.HasOne(d => d.VenueTable)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VenueTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookings__venue___4BAC3F29");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB85816F9077");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerUsername)
                    .HasName("UQ__customer__64E4CB015344ACC3")
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
                    .HasName("PK__employme__533E835466CC7C92");

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

            modelBuilder.Entity<Flags>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FlagCategory)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.FlagDate).HasColumnType("datetime");

                entity.Property(e => e.FlagDesc)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FlagLocationPage)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.FlagTitle)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.FlagVenueId).HasColumnName("FlagVenueID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
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
                    .IsRequired()
                    .HasColumnName("staff_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffPosition)
                    .IsRequired()
                    .HasColumnName("staff_position")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffShifts>(entity =>
            {
                entity.HasKey(e => e.StaffShiftId)
                    .HasName("PK__staff_sh__488620F31F883AE1");

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

            modelBuilder.Entity<VenueTables>(entity =>
            {
                entity.HasKey(e => e.VenueTableId);

                entity.ToTable("venue_tables");

                entity.Property(e => e.VenueTableId).HasColumnName("venue_table_id");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueTableCapacity).HasColumnName("venue_table_capacity");

                entity.Property(e => e.VenueTableNum).HasColumnName("venue_table_num");
            });

            modelBuilder.Entity<Venues>(entity =>
            {
                entity.HasKey(e => e.VenueId)
                    .HasName("PK__venues__82A8BE8D51FF46ED");

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
    }
}
