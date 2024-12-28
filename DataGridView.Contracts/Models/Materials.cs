using System.ComponentModel;

namespace DataGridView.Contracts.Models
{
    public enum Materials
    {
        /// <summary>
        /// Медь
        /// </summary>
        [Description("Медь")]
        Copper = 1,

        /// <summary>
        /// Сталь
        /// </summary>
        [Description("Сталь")]
        Steel = 2,

        /// <summary>
        /// Железо
        /// </summary>
        [Description("Железо")]
        Iron = 3,

        /// <summary>
        /// Хром
        /// </summary>
        [Description("Хром")]
        Chrome = 4,
    }
}
