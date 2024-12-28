using DataGridView.Contracts.Models;

namespace DataGridView.ProductManager.Models
{
    /// <inheritdoc cref="IProductStats"/>
    public class ProductStatsModel : IProductStats
    {
        /// <inheritdoc cref="IProductStats.TotalAmount"/>
        public decimal TotalAmount { get; set; }

        /// <inheritdoc cref="IProductStats.FullPriceNoNDS"/>
        public decimal FullPriceNoNDS { get; set; }

        /// <inheritdoc cref="IProductStats.FullPriceWithNDS"/>
        public decimal FullPriceWithNDS { get; set; }
    }
}
