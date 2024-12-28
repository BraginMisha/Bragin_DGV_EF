using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGridView.Contracts.Models;

namespace DataGridView
{
    /// <summary>
    /// Предоставляет методы для преобразования между объектами <see cref="Validate"/> и <see cref="Product"/>.
    /// </summary>
    public static class Converts
    {
        /// <summary>
        /// Преобразует объект <see cref="Validate"/> в объект <see cref="Product"/>.
        /// </summary>
        /// <param name="valid">Объект <see cref="Validate"/>, который необходимо преобразовать.</param>
        /// <returns>Новый объект <see cref="Product"/> с данными из <paramref name="valid"/>.</returns>
        public static Product ToProduct(Validate valid)
        {
            return new Product()
            {
                Id = valid.Id,
                Name = valid.Name,
                Size = valid.Size,
                Material = valid.Material,
                Quantity = valid.Quantity,
                MinimumQuantity = valid.MinimumQuantity,
                Price = valid.Price,
            };
        }

        /// <summary>
        /// Преобразует объект <see cref="Product"/> в объект <see cref="Validate"/>.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, который необходимо преобразовать.</param>
        /// <returns>Новый объект <see cref="Validate"/> с данными из <paramref name="product"/>.</returns>
        public static Validate ToValidatableProduct(Product product)
        {
            return new Validate()
            {
                Id = product.Id,
                Name = product.Name,
                Size = product.Size,
                Material = product.Material,
                Quantity = product.Quantity,
                MinimumQuantity = product.MinimumQuantity,
                Price = product.Price,
            };
        }
    }
}
