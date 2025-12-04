using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

namespace CNXML_HVA
{
    public static class HtmlGenerator
    {
        // ==========================================
        // PHẦN 1: CÁC HÀM XỬ LÝ LOGIC (GENERATE)
        // ==========================================

        public static void GenerateBranchesHtml()
        {
            try
            {
                string xmlPath = DataPaths.GetXmlFilePath("Branches.xml");
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "branches.html");

                if (!File.Exists(xmlPath)) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                StringBuilder branchesHtml = new StringBuilder();
                XmlNodeList branches = xmlDoc.SelectNodes("//branch");

                int totalFields = 0;
                int totalStaff = 0;
                long totalRevenue = 0;
                int activeBranches = 0;

                foreach (XmlNode branch in branches)
                {
                    string id = GetAttributeValue(branch, "id");
                    string name = GetNodeValue(branch, "name");
                    string code = GetNodeValue(branch, "code");
                    string city = GetNodeValue(branch, "address/city");
                    string district = GetNodeValue(branch, "address/district");
                    string street = GetNodeValue(branch, "address/street");
                    string phone = GetNodeValue(branch, "contact/phone");
                    string email = GetNodeValue(branch, "contact/email");
                    string managerName = GetNodeValue(branch, "manager_name");
                    string branchFields = GetNodeValue(branch, "total_fields");
                    string staffCount = GetNodeValue(branch, "staff_count");
                    string monthlyRevenue = GetNodeValue(branch, "monthly_revenue");
                    string description = GetNodeValue(branch, "description");
                    string status = GetNodeValue(branch, "status");
                    string imageUrl = GetNodeValue(branch, "image_url");

                    if (string.IsNullOrEmpty(imageUrl)) imageUrl = "https://via.placeholder.com/400x220?text=No+Image";

                    string address = $"{street}, {district}, {city}";
                    string statusClass = status == "Active" ? "status-active" : "status-inactive";
                    string statusText = status == "Active" ? "Hoạt động" : "Tạm đóng";

                    int fields = int.TryParse(branchFields, out int f) ? f : 0;
                    int staff = int.TryParse(staffCount, out int s) ? s : 0;
                    long revenue = long.TryParse(monthlyRevenue, out long r) ? r : 0;

                    totalFields += fields;
                    totalStaff += staff;
                    totalRevenue += revenue;
                    if (status == "Active") activeBranches++;

                    string revenueFormatted = revenue.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));

                    branchesHtml.AppendLine($@"
                        <div class=""branch-card"">
                            <span class=""status-badge {statusClass}"">{statusText}</span>
                            <img src=""{imageUrl}"" alt=""{name}"" class=""branch-image"" onerror=""this.src='https://via.placeholder.com/400x220?text=No+Image'"">
                            <div class=""branch-content"">
                                <div class=""branch-header"">
                                    <div>
                                        <div class=""branch-name"">{name}</div>
                                        <span class=""branch-code"">{code}</span>
                                    </div>
                                </div>
                                
                                <div class=""branch-info"">
                                    <div class=""info-row""><span class=""info-icon"">📍</span><span>{address}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">📞</span><span>{phone}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">✉️</span><span>{email}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">👤</span><span>Quản lý: {managerName}</span></div>
                                </div>
                                
                                {(string.IsNullOrEmpty(description) ? "" : $@"<div class=""branch-description"">{description}</div>")}
                                
                                <div class=""branch-stats"">
                                    <div class=""stat-item""><div class=""stat-value"">{fields}</div><div class=""stat-label"">Sân</div></div>
                                    <div class=""stat-item""><div class=""stat-value"">{staff}</div><div class=""stat-label"">Nhân viên</div></div>
                                    <div class=""stat-item""><div class=""stat-value"">{revenueFormatted}</div><div class=""stat-label"">Doanh thu</div></div>
                                </div>
                            </div>
                        </div>");
                }

                string totalRevenueFormatted = totalRevenue.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
                string html = GetBranchesHtmlTemplate(branches.Count, activeBranches, totalStaff, totalRevenueFormatted, branchesHtml.ToString());

                Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                File.WriteAllText(htmlPath, html, Encoding.UTF8);
            }
            catch (Exception ex) { /* Log error */ }
        }

        public static void GenerateEquipmentsHtml()
        {
            try
            {
                string xmlPath = DataPaths.GetXmlFilePath("Equipments.xml");
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "equipments.html");

                if (!File.Exists(xmlPath)) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                StringBuilder equipmentsHtml = new StringBuilder();
                XmlNodeList equipments = xmlDoc.SelectNodes("//equipment");

                int totalQuantity = 0;
                int totalAvailable = 0;
                long totalValue = 0;

                foreach (XmlNode equipment in equipments)
                {
                    string id = GetAttributeValue(equipment, "id");
                    string name = GetNodeValue(equipment, "name");
                    string category = GetNodeValue(equipment, "category");
                    string brand = GetNodeValue(equipment, "brand");
                    string model = GetNodeValue(equipment, "model");
                    string quantityTotal = GetNodeValue(equipment, "quantity_total");
                    string quantityAvailable = GetNodeValue(equipment, "quantity_available");
                    string rentalPrice = GetNodeValue(equipment, "rental_price");
                    string purchasePrice = GetNodeValue(equipment, "purchase_price");
                    string description = GetNodeValue(equipment, "description");
                    string status = GetNodeValue(equipment, "status");
                    string imageUrl = GetNodeValue(equipment, "image_url");
                    string branchId = GetNodeValue(equipment, "branch_id");

                    if (string.IsNullOrEmpty(imageUrl)) imageUrl = "https://via.placeholder.com/400x220?text=No+Image";

                    int qtyTotal = int.TryParse(quantityTotal, out int qt) ? qt : 0;
                    int qtyAvailable = int.TryParse(quantityAvailable, out int qa) ? qa : 0;
                    long price = long.TryParse(purchasePrice, out long p) ? p : 0;
                    long rent = long.TryParse(rentalPrice, out long r) ? r : 0;

                    totalQuantity += qtyTotal;
                    totalAvailable += qtyAvailable;
                    totalValue += price * qtyTotal;

                    string rentalPriceFormatted = rent.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
                    string purchasePriceFormatted = price.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));

                    string statusClass = status == "Active" ? "status-active" : "status-inactive";
                    string statusText = status == "Active" ? "Có sẵn" : "Hết hàng";

                    equipmentsHtml.AppendLine($@"
                        <div class=""equipment-card"">
                            <span class=""status-badge {statusClass}"">{statusText}</span>
                            <img src=""{imageUrl}"" alt=""{name}"" class=""equipment-image"" onerror=""this.src='https://via.placeholder.com/400x220?text=No+Image'"">
                            <div class=""equipment-content"">
                                <div class=""equipment-header"">
                                    <div class=""equipment-name"">{name}</div>
                                    <span class=""equipment-code"">{id}</span>
                                </div>
                                
                                <div class=""equipment-info"">
                                    <div class=""info-row""><span class=""info-icon"">📦</span><span>DM: {category}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">🏷️</span><span>Hãng: {brand} - {model}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">🏢</span><span>CN: {branchId}</span></div>
                                </div>
                                
                                {(string.IsNullOrEmpty(description) ? "" : $@"<div class=""equipment-description"">{description}</div>")}
                                
                                <div class=""equipment-stats"">
                                    <div class=""stat-item""><div class=""stat-value"">{qtyTotal}</div><div class=""stat-label"">Tổng SL</div></div>
                                    <div class=""stat-item""><div class=""stat-value"">{qtyAvailable}</div><div class=""stat-label"">Có sẵn</div></div>
                                    <div class=""stat-item""><div class=""stat-value"">{rentalPriceFormatted}</div><div class=""stat-label"">Giá thuê</div></div>
                                    <div class=""stat-item""><div class=""stat-value"">{purchasePriceFormatted}</div><div class=""stat-label"">Giá mua</div></div>
                                </div>
                            </div>
                        </div>");
                }

                string totalValueFormatted = totalValue.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
                string html = GetEquipmentsHtmlTemplate(equipments.Count, totalQuantity, totalAvailable, totalValueFormatted, equipmentsHtml.ToString());

                Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                File.WriteAllText(htmlPath, html, Encoding.UTF8);
            }
            catch (Exception ex) { /* Log error */ }
        }

        public static void GenerateBookingsHtml()
        {
            try
            {
                string xmlPath = DataPaths.GetXmlFilePath("Bookings.xml");
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "bookings.html");

                if (!File.Exists(xmlPath)) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                StringBuilder bookingsHtml = new StringBuilder();
                XmlNodeList bookings = xmlDoc.SelectNodes("//Booking");

                foreach (XmlNode booking in bookings)
                {
                    string id = GetAttributeValue(booking, "id");
                    string customer = GetNodeValue(booking, "customer");
                    string field = GetNodeValue(booking, "field");
                    string type = GetNodeValue(booking, "type");
                    string date = GetNodeValue(booking, "date");
                    string time = GetNodeValue(booking, "time");
                    string duration = GetNodeValue(booking, "duration");
                    string note = GetNodeValue(booking, "note");

                    bookingsHtml.AppendLine($@"
                        <div class=""booking-card"">
                            <div class=""booking-content"">
                                <div class=""booking-header"">
                                    <div class=""booking-id"">{id}</div>
                                    <span class=""booking-type"">{type}</span>
                                </div>
                                <div class=""booking-info"">
                                    <div class=""info-row""><span class=""info-icon"">👤</span><span>Khách: {customer}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">⚽</span><span>Sân: {field}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">📅</span><span>Ngày: {date}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">🕐</span><span>Giờ: {time} ({duration}p)</span></div>
                                </div>
                                {(string.IsNullOrEmpty(note) ? "" : $@"<div class=""booking-note"">📝 {note}</div>")}
                            </div>
                        </div>");
                }

                string html = GetBookingsHtmlTemplate(bookings.Count, bookingsHtml.ToString());
                Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                File.WriteAllText(htmlPath, html, Encoding.UTF8);
            }
            catch (Exception ex) { /* Log error */ }
        }

        public static void GenerateCustomersHtml()
        {
            try
            {
                string xmlPath = DataPaths.GetXmlFilePath("Customers.xml");
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "customers.html");

                if (!File.Exists(xmlPath)) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                StringBuilder customersHtml = new StringBuilder();
                XmlNodeList customers = xmlDoc.SelectNodes("//customer");

                foreach (XmlNode customer in customers)
                {
                    string id = GetAttributeValue(customer, "id");
                    string name = GetNodeValue(customer, "name");
                    string phone = GetNodeValue(customer, "phone");
                    string email = GetNodeValue(customer, "email");
                    string city = GetNodeValue(customer, "address/city");
                    string district = GetNodeValue(customer, "address/district");
                    string street = GetNodeValue(customer, "address/street");
                    string membership = GetNodeValue(customer, "membership");
                    string notes = GetNodeValue(customer, "notes");

                    string address = $"{street}, {district}, {city}";
                    string membershipClass = membership == "VIP" ? "membership-vip" :
                                           membership == "Gold" ? "membership-gold" : "membership-regular";

                    customersHtml.AppendLine($@"
                        <div class=""customer-card"">
                            <div class=""customer-content"">
                                <div class=""customer-header"">
                                    <div class=""customer-name"">{name}</div>
                                    <span class=""membership-badge {membershipClass}"">{membership}</span>
                                </div>
                                <div class=""customer-id"">Mã KH: {id}</div>
                                <div class=""customer-info"">
                                    <div class=""info-row""><span class=""info-icon"">📞</span><span>{phone}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">✉️</span><span>{email}</span></div>
                                    <div class=""info-row""><span class=""info-icon"">📍</span><span>{address}</span></div>
                                </div>
                                {(string.IsNullOrEmpty(notes) ? "" : $@"<div class=""customer-note"">📝 {notes}</div>")}
                            </div>
                        </div>");
                }

                string html = GetCustomersHtmlTemplate(customers.Count, customersHtml.ToString());
                Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                File.WriteAllText(htmlPath, html, Encoding.UTF8);
            }
            catch (Exception ex) { /* Log error */ }
        }

        // ==========================================
        // PHẦN 2: CÁC HÀM HELPER
        // ==========================================

        private static string GetNodeValue(XmlNode parentNode, string xpath)
        {
            XmlNode node = parentNode.SelectSingleNode(xpath);
            return node?.InnerText ?? "";
        }

        private static string GetAttributeValue(XmlNode node, string attributeName)
        {
            return node?.Attributes?[attributeName]?.Value ?? "";
        }

        // ==========================================
        // PHẦN 3: CÁC HÀM TEMPLATE (GIAO DIỆN WEB)
        // ==========================================

        private static string GetBranchesHtmlTemplate(int totalBranches, int activeBranches, int totalStaff, string totalRevenue, string branchesContent)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Danh Sách Chi Nhánh - Hệ Thống Sân Bóng</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', sans-serif; background: #f5f5f5; }}
        .header {{ background: url('https://images.unsplash.com/photo-1529900748604-07564a03e7a6?w=1600&q=80') center/cover; color: white; padding: 60px 20px; text-align: center; }}
        .header h1 {{ font-size: 2.5em; text-shadow: 2px 2px 4px rgba(0,0,0,0.5); }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; display: flex; justify-content: space-around; flex-wrap: wrap; gap: 20px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
        .stat-item {{ text-align: center; }}
        .stat-value {{ font-size: 1.8em; font-weight: bold; color: #4CAF50; }}
        .stat-label {{ color: #666; font-size: 0.9em; }}
        
        .toolbar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 25px; display: flex; gap: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); flex-wrap: wrap; }}
        #searchInput {{ flex: 2; padding: 10px; border: 1px solid #ddd; border-radius: 5px; min-width: 200px; }}
        #cityFilter, #statusFilter {{ flex: 1; padding: 10px; border: 1px solid #ddd; border-radius: 5px; min-width: 150px; }}

        .branches-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .branch-card {{ background: white; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: transform 0.3s; position: relative; }}
        .branch-card:hover {{ transform: translateY(-5px); }}
        .branch-image {{ width: 100%; height: 200px; object-fit: cover; }}
        .branch-content {{ padding: 20px; }}
        .branch-name {{ font-size: 1.4em; font-weight: bold; color: #333; margin-bottom: 5px; }}
        .branch-code {{ background: #4CAF50; color: white; padding: 4px 10px; border-radius: 20px; font-size: 0.8em; }}
        .status-badge {{ position: absolute; top: 15px; right: 15px; padding: 6px 12px; border-radius: 20px; font-size: 0.8em; color: white; font-weight: bold; }}
        .status-active {{ background: #4CAF50; }} .status-inactive {{ background: #f44336; }}
        .info-row {{ display: flex; align-items: center; margin: 8px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; }}
        .branch-description {{ background: #f1f8f4; padding: 10px; border-radius: 8px; font-size: 0.9em; color: #555; margin: 15px 0; }}
        .branch-stats {{ display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px; margin-top: 15px; padding-top: 15px; border-top: 1px solid #eee; }}
    </style>
</head>
<body>
    <div class=""header"">
        <h1>Hệ Thống Chi Nhánh</h1>
        <p>Quản lý sân bóng chuyên nghiệp</p>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-item""><div class=""stat-value"">{totalBranches}</div><div class=""stat-label"">Chi nhánh</div></div>
            <div class=""stat-item""><div class=""stat-value"">{activeBranches}</div><div class=""stat-label"">Hoạt động</div></div>
            <div class=""stat-item""><div class=""stat-value"">{totalStaff}</div><div class=""stat-label"">Nhân viên</div></div>
            <div class=""stat-item""><div class=""stat-value"">{totalRevenue}</div><div class=""stat-label"">Doanh thu</div></div>
        </div>
        
        <div class=""toolbar"">
            <input type=""text"" id=""searchInput"" placeholder=""Tìm tên, mã chi nhánh, quản lý..."">
            <select id=""cityFilter"">
                <option value="""">Tất cả thành phố</option>
            </select>
            <select id=""statusFilter"">
                <option value="""">Tất cả trạng thái</option>
                <option value=""Active"">Hoạt động</option>
                <option value=""Inactive"">Tạm đóng</option>
            </select>
        </div>

        <div class=""branches-grid"">
            {branchesContent}
        </div>
    </div>

    <script>
        const searchInput = document.getElementById('searchInput');
        const cityFilter = document.getElementById('cityFilter');
        const statusFilter = document.getElementById('statusFilter');
        const cards = document.querySelectorAll('.branch-card');

        // Populate Cities
        const cities = new Set();
        cards.forEach(card => {{
            const city = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.split(',').pop().trim();
            cities.add(city);
        }});
        cities.forEach(city => {{
            const opt = document.createElement('option');
            opt.value = city;
            opt.textContent = city;
            cityFilter.appendChild(opt);
        }});

        function filterBranches() {{
            const term = searchInput.value.toLowerCase();
            const city = cityFilter.value;
            const status = statusFilter.value;

            cards.forEach(card => {{
                const name = card.querySelector('.branch-name').textContent.toLowerCase();
                const code = card.querySelector('.branch-code').textContent.toLowerCase();
                const cardCity = card.querySelector('.info-row:nth-child(1) span:last-child').textContent;
                const cardStatus = card.querySelector('.status-badge').classList.contains('status-active') ? 'Active' : 'Inactive';

                const matchSearch = name.includes(term) || code.includes(term);
                const matchCity = city === '' || cardCity.includes(city);
                const matchStatus = status === '' || cardStatus === status;

                card.style.display = (matchSearch && matchCity && matchStatus) ? 'block' : 'none';
            }});
        }}

        searchInput.addEventListener('input', filterBranches);
        cityFilter.addEventListener('change', filterBranches);
        statusFilter.addEventListener('change', filterBranches);
    </script>
</body>
</html>";
        }

        private static string GetEquipmentsHtmlTemplate(int totalEquipments, int totalQuantity, int totalAvailable, string totalValue, string equipmentsContent)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Danh Sách Dụng Cụ</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', sans-serif; background: #f5f5f5; }}
        .header {{ background: url('https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=1600&q=80') center/cover; color: white; padding: 60px 20px; text-align: center; }}
        .header h1 {{ font-size: 2.5em; text-shadow: 2px 2px 4px rgba(0,0,0,0.5); }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; display: flex; justify-content: space-around; flex-wrap: wrap; gap: 20px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
        .stat-item {{ text-align: center; }}
        .stat-value {{ font-size: 1.8em; font-weight: bold; color: #2196F3; }}
        .stat-label {{ color: #666; font-size: 0.9em; }}
        
        .toolbar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 25px; display: flex; gap: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); flex-wrap: wrap; }}
        #searchInput {{ flex: 2; padding: 10px; border: 1px solid #ddd; border-radius: 5px; }}
        #categoryFilter, #statusFilter {{ flex: 1; padding: 10px; border: 1px solid #ddd; border-radius: 5px; }}

        .branches-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .equipment-card {{ background: white; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: transform 0.3s; position: relative; }}
        .equipment-card:hover {{ transform: translateY(-5px); }}
        .equipment-image {{ width: 100%; height: 200px; object-fit: cover; }}
        .equipment-content {{ padding: 20px; }}
        .equipment-name {{ font-size: 1.4em; font-weight: bold; color: #333; margin-bottom: 5px; }}
        .equipment-code {{ background: #2196F3; color: white; padding: 4px 10px; border-radius: 20px; font-size: 0.8em; }}
        .status-badge {{ position: absolute; top: 15px; right: 15px; padding: 6px 12px; border-radius: 20px; font-size: 0.8em; color: white; font-weight: bold; }}
        .status-active {{ background: #4CAF50; }} .status-inactive {{ background: #f44336; }}
        .info-row {{ display: flex; align-items: center; margin: 8px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; }}
        .equipment-description {{ background: #e3f2fd; padding: 10px; border-radius: 8px; font-size: 0.9em; color: #555; margin: 15px 0; }}
        .equipment-stats {{ display: grid; grid-template-columns: repeat(4, 1fr); gap: 5px; margin-top: 15px; padding-top: 15px; border-top: 1px solid #eee; }}
        .equipment-stats .stat-value {{ font-size: 1.2em; }}
    </style>
</head>
<body>
    <div class=""header"">
        <h1>Kho Dụng Cụ</h1>
        <p>Quản lý tài sản & thiết bị</p>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-item""><div class=""stat-value"">{totalEquipments}</div><div class=""stat-label"">Loại</div></div>
            <div class=""stat-item""><div class=""stat-value"">{totalQuantity}</div><div class=""stat-label"">Tổng SL</div></div>
            <div class=""stat-item""><div class=""stat-value"">{totalAvailable}</div><div class=""stat-label"">Có sẵn</div></div>
            <div class=""stat-item""><div class=""stat-value"">{totalValue}</div><div class=""stat-label"">Giá trị (VNĐ)</div></div>
        </div>

        <div class=""toolbar"">
            <input type=""text"" id=""searchInput"" placeholder=""Tìm tên dụng cụ, mã..."">
            <select id=""categoryFilter"">
                <option value="""">Tất cả danh mục</option>
            </select>
            <select id=""statusFilter"">
                <option value="""">Tất cả trạng thái</option>
                <option value=""Active"">Có sẵn</option>
                <option value=""Inactive"">Hết hàng</option>
            </select>
        </div>

        <div class=""branches-grid"">
            {equipmentsContent}
        </div>
    </div>

    <script>
        const searchInput = document.getElementById('searchInput');
        const categoryFilter = document.getElementById('categoryFilter');
        const statusFilter = document.getElementById('statusFilter');
        const cards = document.querySelectorAll('.equipment-card');

        // Populate Categories
        const categories = new Set();
        cards.forEach(card => {{
            const cat = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.replace('DM: ', '').trim();
            categories.add(cat);
        }});
        categories.forEach(c => {{
            const opt = document.createElement('option');
            opt.value = c;
            opt.textContent = c;
            categoryFilter.appendChild(opt);
        }});

        function filterEquipments() {{
            const term = searchInput.value.toLowerCase();
            const cat = categoryFilter.value;
            const status = statusFilter.value;

            cards.forEach(card => {{
                const name = card.querySelector('.equipment-name').textContent.toLowerCase();
                const code = card.querySelector('.equipment-code').textContent.toLowerCase();
                const cardCat = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.replace('DM: ', '');
                const cardStatus = card.querySelector('.status-badge').classList.contains('status-active') ? 'Active' : 'Inactive';

                const matchSearch = name.includes(term) || code.includes(term);
                const matchCat = cat === '' || cardCat === cat;
                const matchStatus = status === '' || cardStatus === status;

                card.style.display = (matchSearch && matchCat && matchStatus) ? 'block' : 'none';
            }});
        }}

        searchInput.addEventListener('input', filterEquipments);
        categoryFilter.addEventListener('change', filterEquipments);
        statusFilter.addEventListener('change', filterEquipments);
    </script>
</body>
</html>";
        }

        private static string GetCustomersHtmlTemplate(int totalCustomers, string customersContent)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Danh Sách Khách Hàng</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', sans-serif; background: #f5f5f5; }}
        .header {{ background: url('https://images.unsplash.com/photo-1517649763962-0c623066013b?w=1600&q=80') center/cover; color: white; padding: 60px 20px; text-align: center; position: relative; }}
        .header::before {{ content: ''; position: absolute; top: 0; left: 0; right: 0; bottom: 0; background: rgba(0,0,0,0.5); }}
        .header-content {{ position: relative; z-index: 2; }}
        .header h1 {{ font-size: 2.5em; text-shadow: 2px 2px 4px rgba(0,0,0,0.5); }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; display: flex; justify-content: center; gap: 40px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
        .stat-item {{ text-align: center; }}
        .stat-value {{ font-size: 2em; font-weight: bold; color: #FF9800; }}
        .stat-label {{ color: #666; }}
        .toolbar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 25px; display: flex; gap: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); flex-wrap: wrap; }}
        #searchInput {{ flex: 2; padding: 10px; border: 1px solid #ddd; border-radius: 5px; }}
        #membershipFilter {{ flex: 1; padding: 10px; border: 1px solid #ddd; border-radius: 5px; }}
        .grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(350px, 1fr)); gap: 25px; }}
        .customer-card {{ background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.05); transition: transform 0.2s; border-top: 4px solid #FF9800; }}
        .customer-card:hover {{ transform: translateY(-5px); }}
        .customer-content {{ padding: 20px; }}
        .customer-header {{ display: flex; justify-content: space-between; align-items: start; margin-bottom: 10px; }}
        .customer-name {{ font-size: 1.3em; font-weight: bold; color: #333; }}
        .customer-id {{ color: #999; font-size: 0.85em; margin-bottom: 15px; font-family: monospace; }}
        .membership-badge {{ padding: 4px 12px; border-radius: 15px; font-size: 0.8em; font-weight: bold; text-transform: uppercase; }}
        .membership-vip {{ background: #FFD700; color: #8a6d0b; }}
        .membership-gold {{ background: #C0C0C0; color: #555; }}
        .membership-regular {{ background: #E0E0E0; color: #666; }}
        .info-row {{ display: flex; align-items: center; margin: 8px 0; color: #555; }}
        .info-icon {{ width: 25px; margin-right: 5px; color: #FF9800; }}
        .customer-note {{ background: #fff8e1; padding: 10px; border-radius: 6px; font-size: 0.9em; color: #795548; margin-top: 15px; }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""header-content"">
            <h1>Danh Sách Khách Hàng</h1>
            <p>Quản lý thông tin thành viên</p>
        </div>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-item"">
                <div class=""stat-value"">{totalCustomers}</div>
                <div class=""stat-label"">Tổng khách hàng</div>
            </div>
        </div>
        <div class=""toolbar"">
            <input type=""text"" id=""searchInput"" placeholder=""Tìm tên, số điện thoại..."">
            <select id=""membershipFilter"">
                <option value="""">Tất cả hạng</option>
                <option value=""VIP"">VIP</option>
                <option value=""Gold"">Gold</option>
                <option value=""Regular"">Thường</option>
            </select>
        </div>
        <div id=""customersContainer"" class=""grid"">
            {customersContent}
        </div>
    </div>
    <script>
        const searchInput = document.getElementById('searchInput');
        const membershipFilter = document.getElementById('membershipFilter');
        const cards = document.querySelectorAll('.customer-card');

        function filterCustomers() {{
            const term = searchInput.value.toLowerCase();
            const mem = membershipFilter.value;
            cards.forEach(card => {{
                const name = card.querySelector('.customer-name').textContent.toLowerCase();
                const phone = card.querySelector('.info-row:nth-child(1)').textContent.toLowerCase();
                const badge = card.querySelector('.membership-badge').textContent;
                const matchSearch = name.includes(term) || phone.includes(term);
                const matchMem = mem === '' || badge === mem;
                card.style.display = (matchSearch && matchMem) ? 'block' : 'none';
            }});
        }}
        searchInput.addEventListener('input', filterCustomers);
        membershipFilter.addEventListener('change', filterCustomers);
    </script>
</body>
</html>";
        }

        private static string GetBookingsHtmlTemplate(int totalBookings, string bookingsContent)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Danh Sách Đặt Sân</title>
    <style>
        body {{ font-family: 'Segoe UI', sans-serif; background: #f0f2f5; padding: 20px; }}
        .header {{ text-align: center; margin-bottom: 30px; color: #1a237e; }}
        .stats {{ background: white; padding: 15px; border-radius: 8px; text-align: center; margin-bottom: 20px; font-weight: bold; color: #283593; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
        .grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); gap: 20px; }}
        .booking-card {{ background: white; padding: 20px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1); border-left: 5px solid #3949ab; }}
        .booking-header {{ display: flex; justify-content: space-between; margin-bottom: 15px; border-bottom: 1px solid #eee; padding-bottom: 10px; }}
        .booking-id {{ font-weight: bold; color: #555; }}
        .booking-type {{ background: #e8eaf6; color: #3949ab; padding: 3px 10px; border-radius: 12px; font-size: 0.85em; }}
        .info-row {{ margin: 8px 0; color: #444; display: flex; align-items: center; }}
        .info-icon {{ width: 25px; margin-right: 8px; }}
        .booking-note {{ margin-top: 15px; font-style: italic; color: #666; background: #fffde7; padding: 10px; border-radius: 5px; }}
        .toolbar {{ margin-bottom: 20px; background: white; padding: 15px; border-radius: 8px; display: flex; gap: 15px; }}
        #searchInput {{ flex: 1; padding: 8px; border: 1px solid #ddd; border-radius: 4px; }}
    </style>
</head>
<body>
    <div class=""header""><h1>📅 Lịch Đặt Sân</h1></div>
    <div class=""stats"">Tổng số lượt đặt: {totalBookings}</div>
    
    <div class=""toolbar"">
        <input type=""text"" id=""searchInput"" placeholder=""Tìm mã đặt, tên khách, ngày..."">
    </div>

    <div class=""grid"">
        {bookingsContent}
    </div>

    <script>
        const searchInput = document.getElementById('searchInput');
        const cards = document.querySelectorAll('.booking-card');

        searchInput.addEventListener('input', function() {{
            const term = this.value.toLowerCase();
            cards.forEach(card => {{
                const text = card.textContent.toLowerCase();
                card.style.display = text.includes(term) ? 'block' : 'none';
            }});
        }});
    </script>
</body>
</html>";
        }
    }
}