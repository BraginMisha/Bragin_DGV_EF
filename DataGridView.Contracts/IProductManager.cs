using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataGridView.Contracts.Models;

namespace DataGridView.Contracts
{
    /// <summary>
    /// Интерфейс для управления продуктами, предоставляющий методы для добавления, изменения, удаления и получения статистики по продуктам.
    /// </summary>
    public interface IProductManager
    {
        /// <summary>
        /// Получение списка всех продуктов асинхронно.
        /// </summary>
        /// <returns>Коллекция объектов <see cref="Product"/>, представляющих все продукты.</returns>
        Task<IReadOnlyCollection<Product>> GetAllAsync();

        /// <summary>
        /// Добавление нового продукта в хранилище асинхронно.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, представляющий добавляемый продукт.</param>
        /// <returns>Добавленный объект <see cref="Product"/>.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Обновление информации о существующем продукте асинхронно.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, содержащий обновленные данные о продукте.</param>
        Task EditAsync(Product product);

        /// <summary>
        /// Удаление продукта из хранилища по его уникальному идентификатору асинхронно.
        /// </summary>
        /// <param name="id">Уникальный идентификатор продукта.</param>
        /// <returns>Значение <see cref="bool"/>, указывающее, было ли удаление успешным.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Получение статистики по продуктам, включая общее количество, цену с НДС и без НДС, асинхронно.
        /// </summary>
        /// <returns>Объект <see cref="IProductStats"/>, содержащий статистические данные по продуктам.</returns>
        Task<IProductStats> GetStatsAsync();
    }
}
