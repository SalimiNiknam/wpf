using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient; // اضافه شد
using System.Data;           // اضافه شد

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // این تابع وقتی اجرا میشه که روی دکمه جستجو کلیک کنید
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // 1. رشته اتصال به دیتابیس (این مقدار رو باید از سیستم خودتون کپی کنید)
            string connectionString = "اینجا رشته اتصال کپی شده رو بگذارید";

            // 2. گرفتن شماره قطعه از تکست‌باکس
            string searchNumber = txtPartNumber.Text.Trim();

            if (string.IsNullOrEmpty(searchNumber))
            {
                txtResult.Text = "لطفاً شماره قطعه را وارد کنید";
                txtResult.Foreground = Brushes.Red;
                return;
            }

            // 3. نوشتن دستور SQL (نام جدول و ستون‌ها را با دیتابیس خودتان جایگزین کنید)
            // مثلا: SELECT PartName FROM Parts WHERE PartNumber = @PartNumber
            string query = "SELECT PName FROM Part WHERE PID = @PartNumber";

            // 4. اتصال به دیتابیس و اجرای جستجو
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Sina-Salimi-niknam;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PartNumber", searchNumber);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            txtResult.Text = result.ToString();
                            txtResult.Foreground = Brushes.Blue;
                        }
                        else
                        {
                            txtResult.Text = "قطعه‌ای با این شماره یافت نشد";
                            txtResult.Foreground = Brushes.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtResult.Text = "خطا در اتصال: " + ex.Message;
                    txtResult.Foreground = Brushes.Red;
                }
            }
        }
    }
}
