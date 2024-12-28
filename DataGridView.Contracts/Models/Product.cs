using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataGridView.Contracts.Models
{
    public class Product
    {
        /// <summary>
        /// Индивидуальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [DisplayName("Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Размер товара
        /// </summary>
        [DisplayName("Размер")]
        public decimal Size { get; set; }

        /// <summary>
        /// Материал товара
        /// </summary>
        [DisplayName("Материал")]
        public string Material { get; set; }

        /// <summary>
        /// Количество товаров на складе
        /// </summary>
        [DisplayName("Количество")]
        public int Quantity { get; set; }

        /// <summary>
        /// Минимальный предел количества
        /// </summary>
        [DisplayName("Минимальное количество")]
        public int MinimumQuantity { get; set; }

        /// <summary>
        /// Цена (без НДС)
        /// </summary>
        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
