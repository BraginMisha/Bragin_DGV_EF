using System;
using DataGridView.Contracts.Models;

namespace DataGridView
{
    internal class DataGenerator
    {
        /// <summary>
        /// Создание нового экземпляр <see cref="Product"/>
        /// </summary>
        public static Validate CreateDefaultProduct(Action<Validate> addInfo = null)
        {
            var newNail = new Validate
            {
                Id = Guid.NewGuid(),
                Name = "",
                Size = 1.0M,
                Material = Materials.Copper.ToString(),
                Quantity = 1,
                MinimumQuantity = 1,
                Price = 0
            };

            addInfo?.Invoke(newNail);

            return newNail;
        }
    }
}
