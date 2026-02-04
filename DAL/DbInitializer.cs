using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DAL
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure database is up-to-date
            await context.Database.MigrateAsync();

            // Seed categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { CategoryName = "Electronics", IsActive = true },
                    new Category { CategoryName = "Books", IsActive = true },
                    new Category { CategoryName = "Home", IsActive = true }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Seed users (admin + sample customer)
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    new User { Name = "Administrator", Email = "admin@ecommerce.local", PasswordHash = "__PLACEHOLDER_HASH__", Role = "Admin", CreatedDate = DateTime.UtcNow },
                    new User { Name = "Jane Customer", Email = "jane@customer.local", PasswordHash = "__PLACEHOLDER_HASH__", Role = "Customer", CreatedDate = DateTime.UtcNow }
                };

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            // Seed products and associate with categories
            if (!await context.Products.AnyAsync())
            {
                var electronics = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == "Electronics");
                var books = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == "Books");

                var products = new List<Product>
                {
                    new Product { ProductName = "Wireless Headphones", Description = "Noise-cancelling over-ear headphones.", Price = 199.99m, StockQuantity = 50, CategoryId = electronics?.CategoryId },
                    new Product { ProductName = "Smartphone", Description = "Latest model smartphone with OLED display.", Price = 799.99m, StockQuantity = 30, CategoryId = electronics?.CategoryId },
                    new Product { ProductName = "C# Programming", Description = "Comprehensive guide to C# and .NET.", Price = 39.99m, StockQuantity = 200, CategoryId = books?.CategoryId }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            // Seed a sample order for the sample customer
            if (!await context.Orders.AnyAsync())
            {
                var customer = await context.Users.FirstOrDefaultAsync(u => u.Role == "Customer");
                var firstProduct = await context.Products.FirstOrDefaultAsync();

                if (customer != null && firstProduct != null)
                {
                    var order = new Order
                    {
                        UserId = customer.UserId,
                        OrderDate = DateTime.UtcNow,
                        OrderStatus = "Completed",
                        TotalAmount = 0m
                    };

                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ProductId = firstProduct.ProductId,
                        Quantity = 1,
                        Price = firstProduct.Price
                    };

                    context.OrderItems.Add(orderItem);
                    await context.SaveChangesAsync();

                    // Update order total
                    order.TotalAmount = orderItem.Price * orderItem.Quantity;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
