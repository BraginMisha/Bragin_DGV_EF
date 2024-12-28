using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DataGridView.Contracts.Models;

namespace DataGridView.Forms
{
    /// <summary>
    /// Форма добавления и редактирования товаров
    /// </summary>
    public partial class AddAndEditProductForm : Form
    {
        private Validate product;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="oldProduct">Продукт</param>
        public AddAndEditProductForm(Validate oldProduct = null)
        {
            InitializeComponent();

            product = oldProduct ?? DataGenerator.CreateDefaultProduct();

            textBoxName.AddBinding(control => control.Text, product, product => product.Name, errorProvider1);
            numericUpDownSize.AddBinding(control => control.Value, product, product => product.Size, errorProvider1);
            comboBoxMaterials.AddBinding(control => control.SelectedItem, product, product => product.Material);
            numericUpDownQuantity.AddBinding(control => control.Value, product, product => product.Quantity, errorProvider1);
            numericUpDownMinimumQuantity.AddBinding(control => control.Value, product, product => product.MinimumQuantity, errorProvider1);
            numericUpDownPrice.AddBinding(control => control.Value, product, product => product.Price, errorProvider1);

            foreach (var item in Enum.GetValues(typeof(Materials)))
            {
                comboBoxMaterials.Items.Add(GetDisplayValue((Materials)item));
            }
            if (comboBoxMaterials.Items.Count > 0)
            {
                comboBoxMaterials.SelectedIndex = 0;
            }

        }

        public Validate Product => product;

        private void comboBoxMaterials_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1)
            {
                var value = (sender as ComboBox).Items[e.Index];
                e.Graphics.DrawString((string)value,
                    e.Font,
                    new SolidBrush(e.ForeColor),
                    e.Bounds.X,
                    e.Bounds.Y);
            }
        }

        private string GetDisplayValue(Materials value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes<DescriptionAttribute>(false);
            return attributes.FirstOrDefault()?.Description ?? "Неизвестно";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (Product.IsValid())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
