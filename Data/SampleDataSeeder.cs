using SampleWebApi.Entities;

namespace SampleWebApi.Data
{
    public static class SampleDataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.Users.Any()) return;

            // Roles
            var roles = new List<Role>
            {
                new Role { Name = "Admin", Description = "管理者", CreatedAt = DateTime.UtcNow },
                new Role { Name = "User", Description = "一般ユーザー", CreatedAt = DateTime.UtcNow },
                new Role { Name = "Manager", Description = "マネージャー", CreatedAt = DateTime.UtcNow }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Categories
            var categories = new List<Category>
            {
                new Category { CategoryCode = "ELEC", CategoryType = "MAIN", Name = "電子機器", Description = "電子機器カテゴリ", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Category { CategoryCode = "BOOK", CategoryType = "MAIN", Name = "書籍", Description = "書籍カテゴリ", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Category { CategoryCode = "CLTH", CategoryType = "MAIN", Name = "衣類", Description = "衣類カテゴリ", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Users
            var users = new List<User>
            {
                new User { Name = "田中太郎", Email = "tanaka@example.com", Department = "営業部", EmployeeCode = "EMP001", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "佐藤花子", Email = "sato@example.com", Department = "営業部", EmployeeCode = "EMP002", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "鈴木次郎", Email = "suzuki@example.com", Department = "開発部", EmployeeCode = "EMP001", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Products
            var products = new List<Product>
            {
                new Product { ProductCode = "PC001", ProductVersion = "V1", Name = "ノートパソコン", Price = 80000m, CategoryId = categories[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Product { ProductCode = "BK001", ProductVersion = "V1", Name = "プログラミング入門書", Price = 3000m, CategoryId = categories[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Product { ProductCode = "SH001", ProductVersion = "V1", Name = "ビジネスシャツ", Price = 5000m, CategoryId = categories[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Orders
            var orders = new List<Order>
            {
                new Order { OrderNumber = "ORD001", OrderType = "NORMAL", OrderDate = DateTime.Parse("2024-01-15"), Amount = 83000m, UserId = users[0].Id, CategoryId = categories[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Order { OrderNumber = "ORD002", OrderType = "NORMAL", OrderDate = DateTime.Parse("2024-01-16"), Amount = 3000m, UserId = users[1].Id, CategoryId = categories[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Order { OrderNumber = "ORD001", OrderType = "URGENT", OrderDate = DateTime.Parse("2024-01-17"), Amount = 5000m, UserId = users[2].Id, CategoryId = categories[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // OrderItems（TotalPrice計算を削除）
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = orders[0].Id, ProductId = products[0].Id, Quantity = 1, UnitPrice = 80000m, CreatedAt = DateTime.UtcNow },
                new OrderItem { OrderId = orders[0].Id, ProductId = products[1].Id, Quantity = 1, UnitPrice = 3000m, CreatedAt = DateTime.UtcNow },
                new OrderItem { OrderId = orders[1].Id, ProductId = products[1].Id, Quantity = 1, UnitPrice = 3000m, CreatedAt = DateTime.UtcNow },
                new OrderItem { OrderId = orders[2].Id, ProductId = products[2].Id, Quantity = 1, UnitPrice = 5000m, CreatedAt = DateTime.UtcNow }
            };
            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();

            // UserRoles
            var userRoles = new List<UserRole>
            {
                new UserRole { UserId = users[0].Id, RoleId = roles[1].Id, AssignedAt = DateTime.UtcNow },
                new UserRole { UserId = users[1].Id, RoleId = roles[2].Id, AssignedAt = DateTime.UtcNow },
                new UserRole { UserId = users[2].Id, RoleId = roles[0].Id, AssignedAt = DateTime.UtcNow }
            };
            context.UserRoles.AddRange(userRoles);
            context.SaveChanges();
        }
    }
}