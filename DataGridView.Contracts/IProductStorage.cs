using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataGridView.Contracts.Models;

namespace DataGridView.Contracts
{
    /// <summary>
    /// Интерфейс для работы с хранилищем продуктов, предоставляющий методы добавления, изменения, удаления и получения данных о продуктах.
    /// </summary>
    public interface IProductStorage
    {
        /// <summary>
        /// Получение списка всех продуктов из хранилища асинхронно.
        /// </summary>
        /// <returns>Коллекция объектов <see cref="Product"/>, содержащих данные о продуктах.</returns>
        Task<IReadOnlyCollection<Product>> GetAllAsync();

        /// <summary>
        /// Добавление нового продукта в хранилище асинхронно.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, представляющий добавляемый продукт.</param>
        /// <returns>Добавленный объект <see cref="Product"/>.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Обновление информации о существующем продукте в хранилище асинхронно.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, содержащий обновленные данные о продукте.</param>
        Task EditAsync(Product product);

        ///<summary>
        /// Удаление продукта из хранилища по его уникальному идентификатору асинхронно.
        /// </summary>
        /// <param name="id">Уникальный идентификатор продукта.</param>
        /// <returns>Значение <see cref="bool"/>, указывающее, было ли удаление успешным.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
