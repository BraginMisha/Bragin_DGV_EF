using System;
using System.Windows.Forms;
using DataGridView.ProductManager;
using DataGridView.Storage.Memory;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;
using DataGridView.Storage.DB;


namespace DataGridView
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:1111", apiKey: "Q3IpLn3WLvf7FzZSKfFo")
                .CreateLogger();

            var logger = new SerilogLoggerFactory(serilogLogger)
                .CreateLogger("Bragin_DataGridView");
            
            var storage = new DataGridProductStorage();
            var manager = new ProductsManager(storage, logger);
            Application.Run(new MainForm(manager));
        }
    }
}
