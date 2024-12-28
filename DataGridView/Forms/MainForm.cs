using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using DataGridView.Forms;

namespace DataGridView
{
    /// <summary>
    /// Главная форма приложения для управления товарами.
    /// </summary>
    public partial class MainForm : Form
    {
        private IProductManager productManager;
        private readonly BindingSource bindingSource;
        /// <summary>
        /// Инициализирует новый экземпляр MainForm с заданным менеджером продуктов
        /// </summary>
        public MainForm(IProductManager manager)
        {
            InitializeComponent();

            productManager = manager;
            bindingSource = new BindingSource();

            dataGridView1.DataSource = bindingSource;
        }

        private async void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var editForm = new AddAndEditProductForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                await productManager.AddAsync(Converts.ToProduct(editForm.Product));
                bindingSource.ResetBindings(false);
                await UpdateStatusStrip();
            }
        }

        private async void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var oldProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;

                if (MessageBox.Show(
                    $"Вы точно хотите удалить товар \"{oldProduct.Name}\"?",
                    "Внимание",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                    ) == DialogResult.OK)
                {
                    await productManager.DeleteAsync(oldProduct.Id);
                    bindingSource.ResetBindings(false);
                    await UpdateStatusStrip();
                }
            }
        }

        private async void ToolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var oldProduct = (Product)dataGridView1.CurrentRow.DataBoundItem;

                var editForm = new AddAndEditProductForm(Converts.ToValidatableProduct(oldProduct));

                if (MessageBox.Show(
                    $"Изменить товар \"{oldProduct.Name}\"?",
                    "Уведомление",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                    ) == DialogResult.OK)
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        await productManager.EditAsync(Converts.ToProduct(editForm.Product));
                        bindingSource.ResetBindings(false);
                        await UpdateStatusStrip();
                    }
                }
            }
        }

        private async Task UpdateStatusStrip()
        {
            var result = await productManager.GetStatsAsync();
            toolStripStatusLabelAmountProducts.Text = $"Количество строк: {result.TotalAmount}";
            toolStripStatusLabelPriceNDS.Text = $"Цена с НДС: {result.FullPriceWithNDS}";
            toolStripStatusLabelPriceNoNDS.Text = $"Цена без НДС: {result.FullPriceNoNDS}";
            bindingSource.DataSource = await productManager.GetAllAsync();
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalPrice")
            {
                var row = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = row.Quantity * row.Price;
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await productManager.GetAllAsync();
            await UpdateStatusStrip();
            dataGridView1.Columns.Add("TotalPrice", "Сумма");
            dataGridView1.Columns[nameof(Product.Id)].Visible = false;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowHelp(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "Приложение для просмотра, добавление, изменение и удаление товаров на складе",
                    "Справка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
        }
    }
}
