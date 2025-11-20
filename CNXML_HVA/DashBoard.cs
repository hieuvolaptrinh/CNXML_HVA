using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace CNXML_HVA
{
    public partial class DashBoard : Form
    {
        private Timer animationTimer;
        private Timer counterAnimationTimer;
        private int animationProgress;
        private Dictionary<Label, int> targetValues = new Dictionary<Label, int>();
        private Dictionary<Label, int> currentValues = new Dictionary<Label, int>();
        private Dictionary<Label, decimal> targetDecimalValues = new Dictionary<Label, decimal>();
        private Dictionary<Label, decimal> currentDecimalValues = new Dictionary<Label, decimal>();

        public DashBoard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            timerClock.Start();
            UpdateClock();
            ApplyModernStyles();
            PrepareAnimations();
            LoadCounters();
            LoadRevenueStats();
            UpdateChart();
            LoadRecentActivities();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = "🕐 " + DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm:ss");
        }

        private void LoadCounters()
        {
            int customers = CountRootChildren("Customers.xml");
            int fields = CountRootChildren("Fields.xml");
            int equipments = CountRootChildren("Equipments.xml");
            int bookings = CountRootChildren("Bookings.xml");
            int branches = CountRootChildren("Branches.xml");
            int orders = CountRootChildren("Orders.xml");

            // Thiết lập giá trị đích cho animation
            targetValues[lblCustomersCount] = customers;
            targetValues[lblFieldsCount] = fields;
            targetValues[lblEquipmentsCount] = equipments;
            targetValues[lblBookingsCount] = bookings;
            targetValues[lblBranchesCount] = branches;
            targetValues[lblOrdersCount] = orders;

            // Khởi tạo giá trị hiện tại
            currentValues[lblCustomersCount] = 0;
            currentValues[lblFieldsCount] = 0;
            currentValues[lblEquipmentsCount] = 0;
            currentValues[lblBookingsCount] = 0;
            currentValues[lblBranchesCount] = 0;
            currentValues[lblOrdersCount] = 0;

            // Bắt đầu animation đếm số
            StartCounterAnimation();
        }

        private void StartCounterAnimation()
        {
            counterAnimationTimer = new Timer();
            counterAnimationTimer.Interval = 30;
            int step = 0;

            counterAnimationTimer.Tick += (s, e) =>
            {
                step++;
                bool allComplete = true;

                foreach (var kvp in targetValues)
                {
                    Label label = kvp.Key;
                    int target = kvp.Value;
                    int current = currentValues[label];

                    if (current < target)
                    {
                        // Tăng dần với tốc độ khác nhau
                        int increment = Math.Max(1, (target - current) / 10);
                        current = Math.Min(current + increment, target);
                        currentValues[label] = current;
                        label.Text = current.ToString();
                        allComplete = false;
                    }
                }

                if (allComplete || step > 60)
                {
                    // Đảm bảo giá trị cuối cùng chính xác
                    foreach (var kvp in targetValues)
                    {
                        kvp.Key.Text = kvp.Value.ToString();
                    }
                    counterAnimationTimer.Stop();
                    counterAnimationTimer.Dispose();
                }
            };

            counterAnimationTimer.Start();
        }

        private void LoadRevenueStats()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Orders.xml");
                if (!File.Exists(path))
                {
                    lblTotalRevenue.Text = "0 VNĐ";
                    lblPaidRevenue.Text = "0 VNĐ";
                    lblPendingRevenue.Text = "0 VNĐ";
                    return;
                }

                var doc = XDocument.Load(path);
                var orders = doc.Root.Elements("order");

                decimal totalRevenue = 0;
                decimal paidRevenue = 0;
                decimal pendingRevenue = 0;

                foreach (var order in orders)
                {
                    decimal amount = decimal.Parse(order.Element("total_amount")?.Value ?? "0");
                    string paymentStatus = order.Element("payment_status")?.Value ?? "";

                    totalRevenue += amount;

                    if (paymentStatus == "Paid")
                        paidRevenue += amount;
                    else if (paymentStatus == "Pending")
                        pendingRevenue += amount;
                }

                // Thiết lập giá trị đích cho animation
                targetDecimalValues[lblTotalRevenue] = totalRevenue;
                targetDecimalValues[lblPaidRevenue] = paidRevenue;
                targetDecimalValues[lblPendingRevenue] = pendingRevenue;

                currentDecimalValues[lblTotalRevenue] = 0;
                currentDecimalValues[lblPaidRevenue] = 0;
                currentDecimalValues[lblPendingRevenue] = 0;

                StartRevenueAnimation();
            }
            catch
            {
                lblTotalRevenue.Text = "0 VNĐ";
                lblPaidRevenue.Text = "0 VNĐ";
                lblPendingRevenue.Text = "0 VNĐ";
            }
        }

        private void StartRevenueAnimation()
        {
            Timer revenueTimer = new Timer();
            revenueTimer.Interval = 30;
            int step = 0;

            revenueTimer.Tick += (s, e) =>
            {
                step++;
                bool allComplete = true;

                foreach (var kvp in targetDecimalValues)
                {
                    Label label = kvp.Key;
                    decimal target = kvp.Value;
                    decimal current = currentDecimalValues[label];

                    if (current < target)
                    {
                        decimal increment = Math.Max(10000, (target - current) / 10);
                        current = Math.Min(current + increment, target);
                        currentDecimalValues[label] = current;
                        label.Text = FormatCurrency(current);
                        allComplete = false;
                    }
                }

                if (allComplete || step > 60)
                {
                    foreach (var kvp in targetDecimalValues)
                    {
                        kvp.Key.Text = FormatCurrency(kvp.Value);
                    }
                    revenueTimer.Stop();
                    revenueTimer.Dispose();
                }
            };

            revenueTimer.Start();
        }

        private string FormatCurrency(decimal amount)
        {
            return amount.ToString("#,##0") + " VNĐ";
        }

        private void LoadRecentActivities()
        {
            try
            {
                listBoxActivities.Items.Clear();

                string bookingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bookings.xml");
                if (File.Exists(bookingsPath))
                {
                    var bookingsDoc = XDocument.Load(bookingsPath);
                    var bookings = bookingsDoc.Root.Elements("Booking")
                        .OrderByDescending(b => b.Attribute("id")?.Value)
                        .Take(5);

                    foreach (var booking in bookings)
                    {
                        string customer = booking.Element("customer")?.Value ?? "N/A";
                        string field = booking.Element("field")?.Value ?? "N/A";
                        string date = booking.Element("date")?.Value ?? "N/A";
                        string time = booking.Element("time")?.Value ?? "N/A";

                        listBoxActivities.Items.Add($"🏟️  {customer} đặt {field} - {date} {time}h");
                    }
                }

                string ordersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Orders.xml");
                if (File.Exists(ordersPath) && listBoxActivities.Items.Count < 10)
                {
                    var ordersDoc = XDocument.Load(ordersPath);
                    var orders = ordersDoc.Root.Elements("order")
                        .OrderByDescending(o => o.Element("order_date")?.Value)
                        .Take(10 - listBoxActivities.Items.Count);

                    foreach (var order in orders)
                    {
                        string orderId = order.Attribute("id")?.Value ?? "N/A";
                        string amount = order.Element("total_amount")?.Value ?? "0";
                        string status = order.Element("payment_status")?.Value ?? "N/A";
                        string statusIcon = status == "Paid" ? "✅" : status == "Pending" ? "⏳" : "❌";

                        decimal amt = decimal.Parse(amount);
                        listBoxActivities.Items.Add($"{statusIcon}  Đơn {orderId} - {FormatCurrency(amt)} - {status}");
                    }
                }

                if (listBoxActivities.Items.Count == 0)
                {
                    listBoxActivities.Items.Add("Chưa có hoạt động nào");
                }
            }
            catch
            {
                listBoxActivities.Items.Clear();
                listBoxActivities.Items.Add("Không thể tải dữ liệu");
            }
        }

        private void UpdateChart()
        {
            chartOverview.Series.Clear();
            chartOverview.ChartAreas.Clear();
            chartOverview.Legends.Clear();

            // Tạo ChartArea với style hiện đại
            var area = new ChartArea("MainArea");
            area.BackColor = Color.White;
            area.BorderWidth = 0;

            // Cấu hình trục X
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LineColor = Color.FromArgb(220, 220, 220);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10F);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);
            area.AxisX.MajorTickMark.Enabled = false;

            // Cấu hình trục Y
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.LineColor = Color.FromArgb(220, 220, 220);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10F);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);
            area.AxisY.MajorTickMark.Enabled = false;

            chartOverview.ChartAreas.Add(area);

            // Tạo Series với green theme colors
            var series = new Series("Thống kê")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BorderWidth = 0
            };

            // Thêm dữ liệu với màu sắc green theme
            var colors = new Color[]
            {
                Color.FromArgb(46, 125, 50),    // Dark Green
                Color.FromArgb(67, 160, 71),    // Medium Green
                Color.FromArgb(102, 187, 106),  // Light Green
                Color.FromArgb(129, 199, 132),  // Lighter Green
                Color.FromArgb(165, 214, 167),  // Pale Green
                Color.FromArgb(56, 142, 60)     // Forest Green
            };

            series.Points.AddXY("Khách hàng", SafeParse(lblCustomersCount.Text));
            series.Points.AddXY("Sân", SafeParse(lblFieldsCount.Text));
            series.Points.AddXY("Dụng cụ", SafeParse(lblEquipmentsCount.Text));
            series.Points.AddXY("Đặt lịch", SafeParse(lblBookingsCount.Text));
            series.Points.AddXY("Chi nhánh", SafeParse(lblBranchesCount.Text));
            series.Points.AddXY("Đơn hàng", SafeParse(lblOrdersCount.Text));

            for (int i = 0; i < series.Points.Count; i++)
            {
                series.Points[i].Color = colors[i % colors.Length];
                series.Points[i].LabelForeColor = colors[i % colors.Length];
                series.Points[i].BorderWidth = 0;
            }

            series["PixelPointWidth"] = "50";
            chartOverview.Series.Add(series);

            // Loại bỏ legend
            chartOverview.Legends.Clear();
        }

        private int SafeParse(string text)
        {
            int n;
            return int.TryParse(text, out n) ? n : 0;
        }

        private void ApplyModernStyles()
        {
            // Form background - Light green tint
            this.BackColor = Color.FromArgb(245, 251, 246);

            // Panel shadows - sử dụng Paint event
            AddShadowEffect(panelTile1);
            AddShadowEffect(panelTile2);
            AddShadowEffect(panelTile3);
            AddShadowEffect(panelTile4);
            AddShadowEffect(panelTile5);
            AddShadowEffect(panelTile6);
            AddShadowEffect(panelChartContainer);
            AddShadowEffect(panelRevenueContainer);
            AddShadowEffect(panelActivitiesContainer);

            // Rounded corners cho tiles
            SetRoundedCorners(panelTile1, 12);
            SetRoundedCorners(panelTile2, 12);
            SetRoundedCorners(panelTile3, 12);
            SetRoundedCorners(panelTile4, 12);
            SetRoundedCorners(panelTile5, 12);
            SetRoundedCorners(panelTile6, 12);
            SetRoundedCorners(panelChartContainer, 12);
            SetRoundedCorners(panelRevenueContainer, 12);
            SetRoundedCorners(panelActivitiesContainer, 12);
            SetRoundedCorners(panelLogo, 0);

            // Icon panels rounded
            SetRoundedCorners(panelTile1Icon, 8);
            SetRoundedCorners(panelTile2Icon, 8);
            SetRoundedCorners(panelTile3Icon, 8);
            SetRoundedCorners(panelTile4Icon, 8);
            SetRoundedCorners(panelTile5Icon, 8);
            SetRoundedCorners(panelTile6Icon, 8);

            // Style cho menu buttons
            StyleMenuButton(btnChiNhanh);
            StyleMenuButton(btnSan);
            StyleMenuButton(btnLoaiSan);
            StyleMenuButton(btnDungCu);
            StyleMenuButton(btnKhachHang);
            StyleMenuButton(btnDatLich);

            // Logout button hover effect
            btnDangXuat.MouseEnter += (s, e) => btnDangXuat.BackColor = Color.FromArgb(211, 47, 47);
            btnDangXuat.MouseLeave += (s, e) => btnDangXuat.BackColor = Color.FromArgb(229, 57, 53);

            // Tile hover effects
            AddTileHoverEffect(panelTile1);
            AddTileHoverEffect(panelTile2);
            AddTileHoverEffect(panelTile3);
            AddTileHoverEffect(panelTile4);
            AddTileHoverEffect(panelTile5);
            AddTileHoverEffect(panelTile6);
        }

        private void StyleMenuButton(Button btn)
        {
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 125, 50);
            btn.Cursor = Cursors.Hand;

            // Hiệu ứng hover mượt mà
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(46, 125, 50);
                btn.ForeColor = Color.White;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.White;
            };
        }

        private void AddTileHoverEffect(Panel panel)
        {
            panel.Cursor = Cursors.Hand;
            Color originalColor = panel.BackColor;

            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(248, 250, 252);
                AnimateScale(panel, 1.02f, 200);
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = originalColor;
                AnimateScale(panel, 1.0f, 200);
            };
        }

        private void AnimateScale(Control control, float scale, int duration)
        {
            // Tạo hiệu ứng scale nhẹ (giới hạn do WinForms)
            int originalWidth = control.Width;
            int originalHeight = control.Height;
            Point originalLocation = control.Location;

            int targetWidth = (int)(originalWidth * scale);
            int targetHeight = (int)(originalHeight * scale);
            int offsetX = (targetWidth - originalWidth) / 2;
            int offsetY = (targetHeight - originalHeight) / 2;

            Timer scaleTimer = new Timer();
            scaleTimer.Interval = 10;
            int steps = duration / 10;
            int currentStep = 0;

            scaleTimer.Tick += (s, e) =>
            {
                currentStep++;
                float progress = (float)currentStep / steps;

                int newWidth = originalWidth + (int)((targetWidth - originalWidth) * progress);
                int newHeight = originalHeight + (int)((targetHeight - originalHeight) * progress);

                control.Size = new Size(newWidth, newHeight);
                control.Location = new Point(
                    originalLocation.X - (int)(offsetX * progress),
                    originalLocation.Y - (int)(offsetY * progress)
                );

                if (currentStep >= steps)
                {
                    scaleTimer.Stop();
                    scaleTimer.Dispose();
                }
            };

            scaleTimer.Start();
        }

        private void SetRoundedCorners(Control control, int radius)
        {
            if (radius == 0)
            {
                control.Region = null;
                return;
            }

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius * 2, radius * 2), 180, 90);
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(new Rectangle(control.Width - radius * 2, 0, radius * 2, radius * 2), 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(new Rectangle(control.Width - radius * 2, control.Height - radius * 2, radius * 2, radius * 2), 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(new Rectangle(0, control.Height - radius * 2, radius * 2, radius * 2), 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }

        private void AddShadowEffect(Control control)
        {
            // Tạo border để giả lập shadow
            control.Paint += (s, e) =>
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
                    // Không vẽ border để giữ clean look
                }
            };
        }

        private void PrepareAnimations()
        {
            animationProgress = 0;

            // Thiết lập vị trí ban đầu cho animation
            int offset = -50;
            panelTile1.Top += offset;
            panelTile2.Top += offset;
            panelTile3.Top += offset;
            panelTile4.Top += offset;
            panelTile5.Top += offset;
            panelTile6.Top += offset;
            panelChartContainer.Top += offset;
            panelRevenueContainer.Top += offset;
            panelActivitiesContainer.Top += offset;

            // Thiết lập opacity ban đầu (giới hạn trong WinForms)
            panelTile1.Visible = false;
            panelTile2.Visible = false;
            panelTile3.Visible = false;
            panelTile4.Visible = false;
            panelTile5.Visible = false;
            panelTile6.Visible = false;
            panelChartContainer.Visible = false;
            panelRevenueContainer.Visible = false;
            panelActivitiesContainer.Visible = false;

            // Animation timer với easing
            animationTimer = new Timer();
            animationTimer.Interval = 20;
            int delayCounter = 0;

            animationTimer.Tick += (s, e) =>
            {
                delayCounter++;
                animationProgress++;

                // Hiện từng panel với delay
                if (delayCounter == 3) panelTile1.Visible = true;
                if (delayCounter == 6) panelTile2.Visible = true;
                if (delayCounter == 9) panelTile3.Visible = true;
                if (delayCounter == 12) panelTile4.Visible = true;
                if (delayCounter == 15) panelTile5.Visible = true;
                if (delayCounter == 18) panelTile6.Visible = true;
                if (delayCounter == 21) panelRevenueContainer.Visible = true;
                if (delayCounter == 24) panelChartContainer.Visible = true;
                if (delayCounter == 27) panelActivitiesContainer.Visible = true;

                // Ease-out animation
                float easeProgress = 1 - (float)Math.Pow(1 - animationProgress / 35.0, 3);
                int dy = (int)(offset * (1 - easeProgress));

                if (panelTile1.Visible && panelTile1.Top < 0)
                    panelTile1.Top = dy;
                if (panelTile2.Visible && panelTile2.Top < 0)
                    panelTile2.Top = dy;
                if (panelTile3.Visible && panelTile3.Top < 0)
                    panelTile3.Top = dy;
                if (panelTile4.Visible && panelTile4.Top < 0)
                    panelTile4.Top = dy;
                if (panelTile5.Visible && panelTile5.Top < 0)
                    panelTile5.Top = dy;
                if (panelTile6.Visible && panelTile6.Top < 0)
                    panelTile6.Top = dy;
                if (panelRevenueContainer.Visible && panelRevenueContainer.Top < 150)
                    panelRevenueContainer.Top = 150 + dy;
                if (panelChartContainer.Visible && panelChartContainer.Top < 270)
                    panelChartContainer.Top = 270 + dy;
                if (panelActivitiesContainer.Visible && panelActivitiesContainer.Top < 270)
                    panelActivitiesContainer.Top = 270 + dy;

                if (animationProgress >= 40)
                {
                    // Snap to final positions
                    panelTile1.Top = 0;
                    panelTile2.Top = 0;
                    panelTile3.Top = 0;
                    panelTile4.Top = 0;
                    panelTile5.Top = 0;
                    panelTile6.Top = 0;
                    panelRevenueContainer.Top = 150;
                    panelChartContainer.Top = 270;
                    panelActivitiesContainer.Top = 270;
                    animationTimer.Stop();
                    animationTimer.Dispose();
                }
            };

            animationTimer.Start();
        }

        private int CountRootChildren(string fileName)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                if (!File.Exists(path)) return 0;
                var doc = XDocument.Load(path);
                return doc.Root == null ? 0 : doc.Root.Elements().Count();
            }
            catch
            {
                return 0;
            }
        }

        private void btnChiNhanh_Click(object sender, EventArgs e)
        {
            using (var f = new ChiNhanh())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnSan_Click(object sender, EventArgs e)
        {
            using (var f = new San())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnLoaiSan_Click(object sender, EventArgs e)
        {
            using (var f = new LoaiSan())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnDungCu_Click(object sender, EventArgs e)
        {
            using (var f = new DungCu())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            using (var f = new KhachHang())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnDatLich_Click(object sender, EventArgs e)
        {
            using (var f = new DatLich())
            {
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
            LoadRevenueStats();
            LoadRecentActivities();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            try
            {
                var login = new Login();
                login.Show();
            }
            catch
            {
                // Nếu không có form Login, chỉ cần đóng Dashboard
            }
            this.Hide();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
            // Event handler giữ nguyên từ code gốc
        }
    }
}