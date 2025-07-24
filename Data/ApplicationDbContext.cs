using Microsoft.EntityFrameworkCore;
using SampleWebApi.Entities;

namespace SampleWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== 複合ユニーク制約 =====

            // User: Department + EmployeeCode
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Department, u.EmployeeCode })
                .IsUnique()
                .HasDatabaseName("IX_User_Department_EmployeeCode");

            // User: Email (単一ユニーク)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_User_Email");

            // Order: OrderNumber + OrderType
            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.OrderNumber, o.OrderType })
                .IsUnique()
                .HasDatabaseName("IX_Order_OrderNumber_OrderType");

            // Category: CategoryCode + CategoryType
            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.CategoryCode, c.CategoryType })
                .IsUnique()
                .HasDatabaseName("IX_Category_Code_Type");

            // Product: ProductCode + ProductVersion
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.ProductCode, p.ProductVersion })
                .IsUnique()
                .HasDatabaseName("IX_Product_Code_Version");

            // ===== 複合主キー =====

            // OrderItem: OrderId + ProductId
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            // UserRole: UserId + RoleId
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // ===== 外部キー制約 =====

            // Order -> User (CASCADE)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> Category (SET NULL)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Product -> Category (RESTRICT)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category -> ParentCategory (RESTRICT)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem -> Order (CASCADE)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Product (RESTRICT)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserRole -> User (CASCADE)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserRole -> Role (RESTRICT)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== パフォーマンス向上のためのインデックス =====

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderDate)
                .HasDatabaseName("IX_Order_OrderDate");

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId)
                .HasDatabaseName("IX_Order_UserId");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_Product_CategoryId");
        }
    }
}
