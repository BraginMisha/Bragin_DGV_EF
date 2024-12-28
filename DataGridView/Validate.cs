using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataGridView
{
    public class Validate
    {
        /// <summary>
        /// Индивидуальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [DisplayName("Имя")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Размер товара
        /// </summary>
        [DisplayName("Размер")]
        [Range(0d, double.MaxValue)]
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
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Минимальный предел количества
        /// </summary>
        [DisplayName("Минимальное количество")]
        [Range(1, int.MaxValue)]
        public int MinimumQuantity { get; set; }

        /// <summary>
        /// Цена (без НДС)
        /// </summary>
        [DisplayName("Цена")]
        [Range(0d, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Проверка валидности данных
        /// </summary>
        public bool IsValid()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(this, context, results, validateAllProperties: true);
        }
    }

}
