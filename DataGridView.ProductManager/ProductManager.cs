using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using DataGridView.ProductManager.Models;
using Microsoft.Extensions.Logging;

namespace DataGridView.ProductManager
{
    /// <inheritdoc cref="IProductManager"/>
    public class ProductsManager : IProductManager
    {
        private IProductStorage productStorage;
        private readonly ILogger logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductsManager"/> с указанным хранилищем продуктов.
        /// </summary>
        /// <param name="productStorage">Объект, реализующий интерфейс <see cref="IProductStorage"/>, используемый для управления продуктами.</param>
        public ProductsManager(IProductStorage productStorage, ILogger logger)
        {
            this.productStorage = productStorage;
            this.logger = logger;
        }

        /// <inheritdoc cref="IProductManager.AddAsync(Product)"/>
        public async Task<Product> AddAsync(Product product)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Product result;
            try
            {
                result = await productStorage.AddAsync(product);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorProduct(
                    logger,
                    nameof(IProductManager.AddAsync),
                    product.Id,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message,
                    product.Name
                    );
                return null;
            }

            stopwatch.Stop();
            LoggingHelper.LogInfoProduct(
                logger,
                nameof(IProductManager.AddAsync),
                product.Id,
                stopwatch.ElapsedMilliseconds,
                product.Name
                );
            return result;
        }

        /// <inheritdoc cref="IProductManager.DeleteAsync(Guid)"/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            bool result;

            try
            {
                result = await productStorage.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorProduct(logger, nameof(IProductManager.DeleteAsync),
                         id,
                         stopwatch.ElapsedMilliseconds,
                         ex.Message
                         );
                return false;
            }

            stopwatch.Stop();
            LoggingHelper.LogInfoProduct(logger, nameof(IProductManager.DeleteAsync),
                    id,
                    stopwatch.ElapsedMilliseconds
                );
            return result;
        }

        /// <inheritdoc cref="IProductManager.EditAsync(Product)"/>
        public async Task EditAsync(Product product)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await productStorage.EditAsync(product);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorProduct(logger, nameof(IProductManager.EditAsync),
                         product.Id,
                         stopwatch.ElapsedMilliseconds,
                         ex.Message,
                         product.Name
                         );
            }

            stopwatch.Stop();
            LoggingHelper.LogInfoProduct(logger, nameof(IProductManager.EditAsync),
                    product.Id,
                    stopwatch.ElapsedMilliseconds,
                    product.Name
                );
        }

        /// <inheritdoc cref="IProductManager.GetAllAsync"/>
        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            try
            {
                return await productStorage.GetAllAsync();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, nameof(IProductManager.GetAllAsync), ex.Message);
            }
            return null;
        }

        /// <inheritdoc cref="IProductManager.GetStatsAsync"/>
        public async Task<IProductStats> GetStatsAsync()
        {
            try
            {
                var product = await productStorage.GetAllAsync();
                return new ProductStatsModel
                {
                    TotalAmount = product.Count,
                    FullPriceNoNDS = product.Select(x => x.Quantity * x.Price).Sum(),
                    FullPriceWithNDS = product.Select(x => x.Quantity * x.Price).Sum() * 1.2m,
                };
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, nameof(IProductManager.GetStatsAsync), ex.Message);
            }
            return null;
        }
    }
}
