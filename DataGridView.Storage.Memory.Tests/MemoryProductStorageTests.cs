//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DataGridView.Contracts;
//using DataGridView.Contracts.Models;
//using FluentAssertions;
//using Xunit;

//namespace DataGridView.Storage.Memory.Tests
//{
//    public class MemoryProductStorageTests
//    {
//        private readonly IProductStorage productStorage;

//        public MemoryProductStorageTests()
//        {
//            productStorage = new MemoryProductStorage();
//        }


//        /// <summary>
//        /// Добавление товара происходит корректно
//        /// </summary>
//        [Fact]
//        public async Task AddAsyncShouldReturnValue()
//        {
//            // Arrange
//            var model = new Product()
//            {
//                Id = Guid.NewGuid(),
//                Name = "qwe",
//                Size = 2,
//                Material = Materials.Copper.ToString(),
//                Quantity = 11,
//                MinimumQuantity = 1,
//                Price = 10,
//            };

//            // Assert
//            var result = await productStorage.AddAsync(model);

//            // Assert
//            result.Should()
//                .NotBeNull()
//                .And.BeEquivalentTo(new
//                {
//                    model.Id,
//                    model.Name,
//                    model.Size,
//                    model.Material,
//                    model.Quantity,
//                    model.MinimumQuantity,
//                    model.Price,
//                });
//        }

//        /// <summary>
//        /// Удаление из хранилища происходит корректно
//        /// </summary>
//        [Fact]
//        public async Task DeleteAsyncShouldReturnTrue()
//        {
//            // Arrange
//            var productId = Guid.NewGuid();

//            // Act
//            await productStorage.AddAsync(new Product()
//            {
//                Id = productId,
//                Name = "qwe",
//                Size = 2,
//                Material = Materials.Copper.ToString(),
//                Quantity = 11,
//                MinimumQuantity = 1,
//                Price = 10,
//            });
//            var result = await productStorage.DeleteAsync(productId);
//            var list = await productStorage.GetAllAsync();

//            // Assert
//            result.Should().BeTrue();
//            list.Should().NotContain(p => p.Id == productId);
//        }

//        /// <summary>
//        /// Возвращает false, если объект для удаления не найден
//        /// </summary>
//        [Fact]
//        public async Task DeleteAsyncShouldReturnFalse()
//        {
//            // Arrange
//            var productId = Guid.NewGuid();

//            // Act
//            Func<Task> act = productStorage.Awaiting(x => x.DeleteAsync(productId));
//            var result = await productStorage.DeleteAsync(productId);

//            // Assert
//            result.Should().BeFalse();
//            await act.Should().NotThrowAsync();
//        }

//        /// <summary>
//        /// Изменение происходят корректно
//        /// </summary>
//        [Fact]
//        public async Task EditAsyncShouldUpdateStorageData()
//        {
//            // Arrange
//            var productId = Guid.NewGuid();
//            var originalProduct = new Product
//            {
//                Id = productId,
//                Name = "Оригинальный",
//                Size = 1,
//                Material = Materials.Chrome.ToString(),
//                Quantity = 10,
//                MinimumQuantity = 1,
//                Price = 100
//            };

//            var updatedProduct = new Product
//            {
//                Id = productId,
//                Name = "Измененный",
//                Size = 2,
//                Material = Materials.Chrome.ToString(),
//                Quantity = 20,
//                MinimumQuantity = 5,
//                Price = 150
//            };

//            // Act
//            await productStorage.AddAsync(originalProduct);
//            await productStorage.EditAsync(updatedProduct);
//            var result = await productStorage.GetAllAsync();
//            var product = result.FirstOrDefault(x => x.Id == productId);

//            // Assert
//            product.Should().BeEquivalentTo(new
//            {
//                updatedProduct.Name,
//                updatedProduct.Size,
//                updatedProduct.Material,
//                updatedProduct.Quantity,
//                updatedProduct.MinimumQuantity,
//                updatedProduct.Price,
//            });
//        }

//        /// <summary>
//        /// При пустом списке нет ошибок
//        /// </summary>
//        [Fact]
//        public async Task GetAllAsyncShouldNotThrow()
//        {
//            // Act
//            Func<Task> act = productStorage.Awaiting(x => x.GetAllAsync());

//            // Assert
//            await act.Should().NotThrowAsync();
//        }

//        /// <summary>
//        /// Получение пустого списка
//        /// </summary>
//        [Fact]
//        public async Task GetAllAsyncShouldReturnEmpty()
//        {
//            // Assert
//            var result = await productStorage.GetAllAsync();

//            // Assert
//            result.Should()
//                .NotBeNull()
//                .And.BeEmpty();
//        }
//    }
//}
