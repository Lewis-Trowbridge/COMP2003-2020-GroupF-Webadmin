using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class COMP2003_FContext : DbContext
    {
        public COMP2003_FContext()
        {
        }

        public COMP2003_FContext(DbContextOptions<COMP2003_FContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLocations> AdminLocations { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<AppBookingsView> AppBookingsView { get; set; }
        public virtual DbSet<AppVenueView> AppVenueView { get; set; }
        public virtual DbSet<BookingAttendees> BookingAttendees { get; set; }
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employment> Employment { get; set; }
        public virtual DbSet<Flags> Flags { get; set; }
        public virtual DbSet<OpeningTimes> OpeningTimes { get; set; }
        public virtual DbSet<SessionCache> SessionCache { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffShifts> StaffShifts { get; set; }
        public virtual DbSet<VenueTables> VenueTables { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }
        public virtual DbSet<WebBookingsView> WebBookingsView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=socem1.uopnet.plymouth.ac.uk;Initial Catalog=COMP2003_F;Persist Security Info=True;User ID=COMP2003_F;Password=CncJ279*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminLocations>(entity =>
            {
                entity.HasKey(e => new { e.VenueId, e.AdminId })
                    .HasName("PK__admin_lo__86921A99F10485DB");

                entity.ToTable("admin_locations");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminLocations)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__admin_loc__admin__2C3393D0");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.AdminLocations)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__admin_loc__venue__2D27B809");
            });

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__admins__43AA4141AEAB66FE");

                entity.ToTable("admins");

                entity.HasIndex(e => e.AdminUsername)
                    .HasName("UQ__admins__0CEDD55F12DAEE4A")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

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

            modelBuilder.Entity<AppBookingsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("app_bookings_view");

                entity.Property(e => e.AddLineOne)
                    .IsRequired()
                    .HasColumnName("add_line_one")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddLineTwo)
                    .HasColumnName("add_line_two")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BookingAttended).HasColumnName("booking_attended");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingSize).HasColumnName("booking_size");

                entity.Property(e => e.BookingTime)
                    .HasColumnName("booking_time")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

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

                entity.Property(e => e.VenueTableCapacity).HasColumnName("venue_table_capacity");

                entity.Property(e => e.VenueTableId).HasColumnName("venue_table_id");

                entity.Property(e => e.VenueTableNum).HasColumnName("venue_table_num");
            });

            modelBuilder.Entity<AppVenueView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("app_venue_view");

                entity.Property(e => e.AddLineOne)
                    .IsRequired()
                    .HasColumnName("add_line_one")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddLineTwo)
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

                entity.Property(e => e.TotalCapacity).HasColumnName("total_capacity");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

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

            modelBuilder.Entity<BookingAttendees>(entity =>
            {
                entity.HasKey(e => new { e.BookingId, e.CustomerId });

                entity.ToTable("booking_attendees");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.BookingAttended).HasColumnName("booking_attended");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingAttendees)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__booking_a__booki__36B12243");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingAttendees)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__booking_a__custo__37A5467C");
            });

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__bookings__5DE3A5B18784C5FA");

                entity.ToTable("bookings");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingSize).HasColumnName("booking_size");

                entity.Property(e => e.BookingTime)
                    .HasColumnName("booking_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueTableId).HasColumnName("venue_table_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__bookings__staff___6D0D32F4");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookings__venue___32E0915F");

                entity.HasOne(d => d.VenueTable)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VenueTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookings__venue___33D4B598");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB85A162A00C");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerUsername)
                    .HasName("UQ__customer__64E4CB01F95F2078")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerContactNumber)
                    .IsRequired()
                    .HasColumnName("customer_contact_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

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
                    .HasName("PK__employme__533E835494A137A7");

                entity.ToTable("employment");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Employment)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__employmen__staff__3F466844");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Employment)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__employmen__venue__403A8C7D");
            });

            modelBuilder.Entity<Flags>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flags");

                entity.Property(e => e.FlagCategory)
                    .HasColumnName("flag_category")
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.FlagDate)
                    .HasColumnName("flag_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FlagDesc)
                    .HasColumnName("flag_desc")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FlagLocationPage)
                    .HasColumnName("flag_location_page")
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.FlagPersistent).HasColumnName("flag_persistent");

                entity.Property(e => e.FlagResolved).HasColumnName("flag_resolved");

                entity.Property(e => e.FlagTitle)
                    .HasColumnName("flag_title")
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.FlagUrgency).HasColumnName("flag_urgency");

                entity.Property(e => e.FlagVenueId).HasColumnName("flag_venue_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
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

                entity.HasOne(d => d.Venue)
                    .WithMany()
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__opening_t__venue__4222D4EF");
            });

            modelBuilder.Entity<SessionCache>(entity =>
            {
                entity.ToTable("session_cache");

                entity.HasIndex(e => e.ExpiresAtTime)
                    .HasName("Index_ExpiresAtTime");

                entity.Property(e => e.Id).HasMaxLength(449);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StaffContactNum)
                    .IsRequired()
                    .HasColumnName("staff_contact_num")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnName("staff_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffPosition)
                    .IsRequired()
                    .HasColumnName("staff_position")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffShifts>(entity =>
            {
                entity.HasKey(e => e.StaffShiftId)
                    .HasName("PK__staff_sh__488620F3A187C459");

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
                    .HasConstraintName("FK__staff_shi__staff__3C69FB99");
            });

            modelBuilder.Entity<VenueTables>(entity =>
            {
                entity.HasKey(e => e.VenueTableId)
                    .HasName("PK__venue_ta__5B02BE9094DDA9C7");

                entity.ToTable("venue_tables");

                entity.Property(e => e.VenueTableId).HasColumnName("venue_table_id");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.VenueTableCapacity).HasColumnName("venue_table_capacity");

                entity.Property(e => e.VenueTableNum).HasColumnName("venue_table_num");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.VenueTables)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__venue_tab__venue__300424B4");
            });

            modelBuilder.Entity<Venues>(entity =>
            {
                entity.HasKey(e => e.VenueId)
                    .HasName("PK__venues__82A8BE8DDDC86568");

                entity.ToTable("venues");

                entity.Property(e => e.VenueId).HasColumnName("venue_id");

                entity.Property(e => e.AddLineOne)
                    .IsRequired()
                    .HasColumnName("add_line_one")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddLineTwo)
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

            modelBuilder.Entity<WebBookingsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("web_bookings_view");

                entity.Property(e => e.BookingTime)
                    .HasColumnName("booking_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerContactNumber)
                    .IsRequired()
                    .HasColumnName("customer_contact_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customer_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffContactNum)
                    .IsRequired()
                    .HasColumnName("staff_contact_num")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnName("staff_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VenueId).HasColumnName("venue_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
