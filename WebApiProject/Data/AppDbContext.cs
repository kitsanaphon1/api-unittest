using Microsoft.EntityFrameworkCore;
using WebApiProject.Models; // ✅ เพิ่ม namespace นี้

namespace WebApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } // ✅ ใช้ Product จาก WebApiProject.Models
    }
}
