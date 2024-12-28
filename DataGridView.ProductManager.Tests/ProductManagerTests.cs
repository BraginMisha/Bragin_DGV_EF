using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DataGridView.ProductManager.Tests
{
    public class ProductManagerTests
    {
        private readonly Mock<IProductStorage> productStorageMock;
        private readonly Mock<ILogger<ProductsManager>> loggerMock;
        private readonly ProductsManager productManager;

        public ProductManagerTests()
        {
            productStorageMock = new Mock<IProductStorage>();
            loggerMock = new Mock<ILogger<ProductsManager>>();
            loggerMock.Setup(x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ));

            productManager = new ProductsManager(productStorageMock.Object, loggerMock.Object);
        }

        /// <summary>
        /// Добавляет продукт в хранилище
        /// и записывает ожидаемое количество информационных логов.
        /// </summary>
        [Fact]
        public async Task AddAsyncShouldWork()
        {
            // Arrange
            var model = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "qwe",
                Size = 2,
                Material = Materials.Copper.ToString(),
                Quantity = 11,
                MinimumQuantity = 1,
                Price = 10,
            };

            productStorageMock.Setup(x => x.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(model);

            // Act
            var result = await productManager.AddAsync(model);

            // Assert
            result.Should().NotBeNull().And.Be(model);
            productStorageMock.Verify(x => x.AddAsync(It.Is<Product>(y => y.Id == model.Id)), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(1));
        }

        /// <summary>
        /// Должен возвращать true если операция успешна
        /// </summary>
        [Fact]
        public async Task DeleteAsyncShouldReturnTrueWhenProductIsDeleted()
        {
            // Arrange
            var productId = Guid.NewGuid();

            productStorageMock.Setup(x => x.DeleteAsync(productId))
                .ReturnsAsync(true);

            // Act
            var result = await productManager.DeleteAsync(productId);

            // Assert
            result.Should().BeTrue();
            productStorageMock.Verify(x => x.DeleteAsync(productId), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(1));
        }

        /// <summary>
        /// Должен возвращать false если операция безуспешна
        /// </summary>
        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWhenProductDeletionFails()
        {
            // Arrange
            var productId = Guid.NewGuid();

            productStorageMock.Setup(x => x.DeleteAsync(productId))
                .ReturnsAsync(false);

            // Act
            var result = await productManager.DeleteAsync(productId);

            // Assert
            result.Should().BeFalse();

            productStorageMock.Verify(x => x.DeleteAsync(productId), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(1));
        }

        /// <summary>
        /// Вносятся изменения и возвращается завершенная задача
        /// </summary>
        [Fact]
        public async Task EditAsyncShouldWork()
        {
            // Arrange
            var model = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Оригинальный",
                Size = 1,
                Material = Materials.Chrome.ToString(),
                Quantity = 10,
                MinimumQuantity = 1,
                Price = 100
            };

            productStorageMock.Setup(x => x.EditAsync(It.IsAny<Product>()));

            // Act
            await productManager.EditAsync(model);

            // Assert
            productStorageMock.Verify(x => x.EditAsync(It.IsAny<Product>()), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        /// <summary>
        /// Должна возвращаться коллекция продуктов
        /// </summary>
        [Fact]
        public async Task GetAllAsyncShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product() { Id = Guid.NewGuid(), Name = "Product 1", Price = 10 },
                new Product() { Id = Guid.NewGuid(), Name = "Product 2", Price = 20 }
            }.AsReadOnly();

            productStorageMock.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await productManager.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(products);

            productStorageMock.Verify(x => x.GetAllAsync(), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(0));
        }

        /// <summary>
        /// Должна возвращаться коллекция продуктов
        /// с верно подсчитанной статистикой
        /// </summary>
        [Fact]
        public async Task GetStatsAsyncShouldReturnCorrectStats()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product() { Id = Guid.NewGuid(), Quantity = 10, Price = 100 },
                new Product() { Id = Guid.NewGuid(), Quantity = 5, Price = 200 }
            }.AsReadOnly();

            productStorageMock.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await productManager.GetStatsAsync();

            // Assert
            result.TotalAmount.Should().Be(products.Count);
            result.FullPriceNoNDS.Should().Be(products.Select(p => p.Quantity * p.Price).Sum());
            result.FullPriceWithNDS.Should().Be(products.Select(p => p.Quantity * p.Price).Sum() * 1.2m);

            productStorageMock.Verify(x => x.GetAllAsync(), Times.Once);
            productStorageMock.VerifyNoOtherCalls();
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(0));
        }
    }
}
