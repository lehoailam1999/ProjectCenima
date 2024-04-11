using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BillStatus> BillStatuses { get; set; }
        public DbSet<BillTicket> BillTickets { get; set; }
        public DbSet<Cinema> Cenimas { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTpye> MovieTypes { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<RankCustomer> RankCustomers { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<BillFood> BillFoods { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatStatus> SeatStatuses { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserStatus> UserStatuss { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-5K7OTPS\\SQLEXPRESS;Initial Catalog=Project_Cinema;Integrated Security=True;Encrypt=true;Trustservercertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
              .HasOne(b => b.seat)
              .WithMany(u => u.ticket)
              .HasForeignKey(b => b.SeatId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.user)
                .WithMany(u => u.bill)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>()
              .HasOne(s => s.room)
               .WithMany(r => r.schedule)
             .HasForeignKey(s => s.RoomId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
              .HasOne(s => s.movie)
               .WithMany(r => r.schedule)
             .HasForeignKey(s => s.MovieId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Room>()
                .HasOne(r => r.cinema)
                .WithMany(c => c.room)
                .HasForeignKey(r => r.CinemaId).OnDelete(DeleteBehavior.Restrict);


        }
        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

    }
}
/*
 * Phân tích chức năng:
 * Đăng ký, Gửi email xác nhận,Đăng nhập,Quên mất khẩu,Đổi mật khẩu,
 * CRUD mỗi bảng
 */
