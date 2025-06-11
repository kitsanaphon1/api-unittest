using Xunit;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Controllers;
using WebApiProject.Data;
using WebApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApiProject.Tests
{
    public class ProductsControllerTests
    {
        private DbContextOptions<AppDbContext> GetInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // 🔁 ใช้ชื่อสุ่ม ป้องกัน test ทับกัน
                .Options;
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenFound()
        {
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "Test Product", Price = 9.99m });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new ProductsController(context);
                var result = await controller.GetProduct(1);
                var okResult = Assert.IsType<ActionResult<Product>>(result);
                var product = Assert.IsType<Product>(okResult.Value);
                Assert.Equal("Test Product", product.Name);
            }
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenMissing()
        {
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                var controller = new ProductsController(context);
                var result = await controller.GetProduct(999);
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async Task AddProduct_ReturnsCreatedProduct()
        {
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                var controller = new ProductsController(context);
                var newProduct = new Product { Id = 10, Name = "New Product", Price = 19.99m };

                var result = await controller.AddProduct(newProduct);

                var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var createdProduct = Assert.IsType<Product>(createdResult.Value);
                Assert.Equal("New Product", createdProduct.Name);
            }
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenSuccessful()
        {
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.Add(new Product { Id = 20, Name = "Old Name", Price = 5.0m });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new ProductsController(context);
                var updatedProduct = new Product { Id = 20, Name = "Updated Name", Price = 7.5m };

                var result = await controller.UpdateProduct(20, updatedProduct);

                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenSuccessful()
        {
            var options = GetInMemoryDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Products.Add(new Product { Id = 30, Name = "To Delete", Price = 12.0m });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new ProductsController(context);

                var result = await controller.DeleteProduct(30);

                Assert.IsType<NoContentResult>(result);
            }
        }
    }
}
