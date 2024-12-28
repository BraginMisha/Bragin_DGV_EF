using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView.Storage.DB
{
    public class DataGridProductStorage : IProductStorage
    {

        /// <summary>
        /// Добавляет новоый продукт в базу данных.
        /// </summary>
        public async Task<Product> AddAsync(Product product)
        {
            using (var context = new DataGridContext())
            {
                var person = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Material = product.Material,
                    Price = product.Price,
                    MinimumQuantity = product.MinimumQuantity,
                    Quantity = product.Quantity,
                    Size = product.Size
                };
                context.Product.Add(product);
                await context.SaveChangesAsync();
            }

            return product;
        }

        /// <summary>
        /// Удаляет заявителя из базы данных по идентификатору.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = new DataGridContext())
            {
                var item = await context.Product.FirstOrDefaultAsync(x => x.Id == id);
                if (item != null)
                {
                    context.Product.Remove(item);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Обновляет существующий продукт в базе данных.
        /// </summary>
        public async Task EditAsync(Product product)
        {
            using (var context = new DataGridContext())
            {
                var target = await context.Product.FirstOrDefaultAsync(x => x.Id == product.Id);
                if (target != null)
                {
                    target.Name = product.Name;
                    target.Material = product.Material;
                    target.Price = product.Price;
                    target.MinimumQuantity = product.MinimumQuantity;
                    target.Quantity = product.Quantity;
                    target.Size = product.Size;
                }
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Получает все продукты из базы данных.
        /// </summary>
        async Task<IReadOnlyCollection<Product>> IProductStorage.GetAllAsync() 
        {
            using (var context = new DataGridContext())
            {
                var items = await context.Product.ToListAsync();
                return items;
            }
        }
    }

}
