using DataGridView.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView.Storage.DB
{
    public class DataGridContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        public DataGridContext() : base("DataGridProducts")
        {
        }

        /// <summary>
        /// Таблица <see cref="Contracts.Models.Product"/> в базе данных
        /// </summary>
        public DbSet<Product> Product { get; set; }
    }
}
