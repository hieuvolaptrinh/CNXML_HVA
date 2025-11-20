using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNXML_HVA
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            // Khởi tạo tất cả file XML vào AppData khi lần đầu chạy
            try
            {
                DataPaths.InitializeAllXmlFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khởi tạo dữ liệu:\n{ex.Message}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            
            Application.Run(new DashBoard());

        }
    }
}
