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
            UpdateChart();
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

            // Thiết lập giá trị đích cho animation
            targetValues[lblCustomersCount] = customers;
            targetValues[lblFieldsCount] = fields;
            targetValues[lblEquipmentsCount] = equipments;
            targetValues[lblBookingsCount] = bookings;

            // Khởi tạo giá trị hiện tại
            currentValues[lblCustomersCount] = 0;
            currentValues[lblFieldsCount] = 0;
            currentValues[lblEquipmentsCount] = 0;
            currentValues[lblBookingsCount] = 0;

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

            // Tạo Series với gradient colors
            var series = new Series("Thống kê")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BorderWidth = 0
            };

            // Thêm dữ liệu với màu sắc gradient riêng
            var colors = new Color[]
            {
                Color.FromArgb(33, 150, 243),   // Blue
                Color.FromArgb(56, 142, 60),    // Green
                Color.FromArgb(255, 152, 0),    // Orange
                Color.FromArgb(156, 39, 176)    // Purple
            };

            series.Points.AddXY("Khách hàng", SafeParse(lblCustomersCount.Text));
            series.Points.AddXY("Sân", SafeParse(lblFieldsCount.Text));
            series.Points.AddXY("Dụng cụ", SafeParse(lblEquipmentsCount.Text));
            series.Points.AddXY("Đặt lịch", SafeParse(lblBookingsCount.Text));

            for (int i = 0; i < series.Points.Count; i++)
            {
                series.Points[i].Color = colors[i];
                series.Points[i].LabelForeColor = colors[i];
                series.Points[i].BorderWidth = 0;
            }

            series["PixelPointWidth"] = "60";
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
            // Form background
            this.BackColor = Color.FromArgb(245, 248, 250);

            // Panel shadows - sử dụng Paint event
            AddShadowEffect(panelTile1);
            AddShadowEffect(panelTile2);
            AddShadowEffect(panelTile3);
            AddShadowEffect(panelTile4);
            AddShadowEffect(panelChartContainer);

            // Rounded corners cho tiles
            SetRoundedCorners(panelTile1, 12);
            SetRoundedCorners(panelTile2, 12);
            SetRoundedCorners(panelTile3, 12);
            SetRoundedCorners(panelTile4, 12);
            SetRoundedCorners(panelChartContainer, 12);
            SetRoundedCorners(panelLogo, 0);

            // Icon panels rounded
            SetRoundedCorners(panelTile1Icon, 8);
            SetRoundedCorners(panelTile2Icon, 8);
            SetRoundedCorners(panelTile3Icon, 8);
            SetRoundedCorners(panelTile4Icon, 8);

            // Style cho menu buttons
            StyleMenuButton(btnChiNhanh);
            StyleMenuButton(btnSan);
            StyleMenuButton(btnLoaiSan);
            StyleMenuButton(btnDungCu);
            StyleMenuButton(btnKhachHang);
            StyleMenuButton(btnDatLich);

            // Logout button hover effect
            btnDangXuat.MouseEnter += (s, e) => btnDangXuat.BackColor = Color.FromArgb(198, 40, 40);
            btnDangXuat.MouseLeave += (s, e) => btnDangXuat.BackColor = Color.FromArgb(211, 47, 47);

            // Tile hover effects
            AddTileHoverEffect(panelTile1);
            AddTileHoverEffect(panelTile2);
            AddTileHoverEffect(panelTile3);
            AddTileHoverEffect(panelTile4);
        }

        private void StyleMenuButton(Button btn)
        {
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 81, 181);
            btn.Cursor = Cursors.Hand;

            // Hiệu ứng hover mượt mà
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(63, 81, 181);
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
            panelChartContainer.Top += offset;

            // Thiết lập opacity ban đầu (giới hạn trong WinForms)
            panelTile1.Visible = false;
            panelTile2.Visible = false;
            panelTile3.Visible = false;
            panelTile4.Visible = false;
            panelChartContainer.Visible = false;

            // Animation timer với easing
            animationTimer = new Timer();
            animationTimer.Interval = 20;
            int delayCounter = 0;

            animationTimer.Tick += (s, e) =>
            {
                delayCounter++;
                animationProgress++;

                // Hiện từng panel với delay
                if (delayCounter == 5) panelTile1.Visible = true;
                if (delayCounter == 10) panelTile2.Visible = true;
                if (delayCounter == 15) panelTile3.Visible = true;
                if (delayCounter == 20) panelTile4.Visible = true;
                if (delayCounter == 25) panelChartContainer.Visible = true;

                // Ease-out animation
                float easeProgress = 1 - (float)Math.Pow(1 - animationProgress / 25.0, 3);
                int dy = (int)(offset * (1 - easeProgress));

                if (panelTile1.Visible && panelTile1.Top < 0)
                    panelTile1.Top = dy;
                if (panelTile2.Visible && panelTile2.Top < 0)
                    panelTile2.Top = dy;
                if (panelTile3.Visible && panelTile3.Top < 0)
                    panelTile3.Top = dy;
                if (panelTile4.Visible && panelTile4.Top < 0)
                    panelTile4.Top = dy;
                if (panelChartContainer.Visible && panelChartContainer.Top < 160)
                    panelChartContainer.Top = 160 + dy;

                if (animationProgress >= 30)
                {
                    // Snap to final positions
                    panelTile1.Top = 0;
                    panelTile2.Top = 0;
                    panelTile3.Top = 0;
                    panelTile4.Top = 0;
                    panelChartContainer.Top = 160;
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
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnSan_Click(object sender, EventArgs e)
        {
            using (var f = new San())
            {
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnLoaiSan_Click(object sender, EventArgs e)
        {
            using (var f = new LoaiSan())
            {
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnDungCu_Click(object sender, EventArgs e)
        {
            using (var f = new DungCu())
            {
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            using (var f = new KhachHang())
            {
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void btnDatLich_Click(object sender, EventArgs e)
        {
            using (var f = new DatLich())
            {
                AddBackButton(f);
                f.ShowDialog();
            }
            LoadCounters();
            UpdateChart();
        }

        private void AddBackButton(Form form)
        {
            var back = new Button();
            back.Text = "← Quay về Dashboard";
            back.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            back.BackColor = Color.FromArgb(240, 242, 245);
            back.ForeColor = Color.FromArgb(66, 66, 66);
            back.Dock = DockStyle.Top;
            back.Height = 45;
            back.FlatStyle = FlatStyle.Flat;
            back.FlatAppearance.BorderSize = 0;
            back.Cursor = Cursors.Hand;
            back.Click += (s, e) => form.Close();

            back.MouseEnter += (s, e) => back.BackColor = Color.FromArgb(230, 232, 235);
            back.MouseLeave += (s, e) => back.BackColor = Color.FromArgb(240, 242, 245);

            form.Controls.Add(back);
            form.Controls.SetChildIndex(back, 0);
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