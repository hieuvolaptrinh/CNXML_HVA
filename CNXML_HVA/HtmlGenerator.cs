using System;
using System.IO;
using System.Xml;
using System.Text;

namespace CNXML_HVA
{
    public static class HtmlGenerator
    {
        public static void GenerateBranchesHtml()
        {
            string xmlPath = DataPaths.GetXmlFilePath("Branches.xml");
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "branches.html");
            
            if (!File.Exists(xmlPath))
            {
                throw new Exception("Không tìm thấy file Branches.xml");
            }
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            
            // Đọc tất cả branches và tạo HTML
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
                
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "https://via.placeholder.com/400x220?text=No+Image";
                }
                
                string address = $"{street}, {district}, {city}";
                string statusClass = status == "Active" ? "status-active" : "status-inactive";
                string statusText = status == "Active" ? "Hoạt động" : "Tạm đóng";
                
                int fields = int.Parse(branchFields);
                int staff = int.Parse(staffCount);
                long revenue = long.Parse(monthlyRevenue);
                
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
                                <div class=""info-row"">
                                    <span class=""info-icon"">📍</span>
                                    <span>{address}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">📞</span>
                                    <span>{phone}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">✉️</span>
                                    <span>{email}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">👤</span>
                                    <span>Quản lý: {managerName}</span>
                                </div>
                            </div>
                            
                            {(string.IsNullOrEmpty(description) ? "" : $@"<div class=""branch-description"">{description}</div>")}
                            
                            <div class=""branch-stats"">
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{branchFields}</div>
                                    <div class=""stat-label"">Sân</div>
                                </div>
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{staffCount}</div>
                                    <div class=""stat-label"">Nhân viên</div>
                                </div>
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{revenueFormatted}</div>
                                    <div class=""stat-label"">Doanh thu/tháng</div>
                                </div>
                            </div>
                        </div>
                    </div>");
            }
            
            string totalRevenueFormatted = totalRevenue.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
            
            // Tạo HTML hoàn chỉnh
            string html = GetBranchesHtmlTemplate(
                branches.Count, 
                activeBranches, 
                totalStaff, 
                totalRevenueFormatted, 
                branchesHtml.ToString()
            );
            
            Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
            File.WriteAllText(htmlPath, html, Encoding.UTF8);
        }
        
        public static void GenerateEquipmentsHtml()
        {
            string xmlPath = DataPaths.GetXmlFilePath("Equipments.xml");
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web", "equipments.html");
            
            if (!File.Exists(xmlPath))
            {
                throw new Exception("Không tìm thấy file Equipments.xml");
            }
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            
            // Đọc tất cả equipments và tạo HTML
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
                string condition = GetNodeValue(equipment, "condition");
                string branchId = GetNodeValue(equipment, "branch_id");
                string description = GetNodeValue(equipment, "description");
                string status = GetNodeValue(equipment, "status");
                string imageUrl = GetNodeValue(equipment, "image_url");
                
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "https://via.placeholder.com/400x220?text=No+Image";
                }
                
                int qtyTotal = int.Parse(quantityTotal);
                int qtyAvailable = int.Parse(quantityAvailable);
                long price = long.Parse(purchasePrice);
                
                totalQuantity += qtyTotal;
                totalAvailable += qtyAvailable;
                totalValue += price * qtyTotal;
                
                string rentalPriceFormatted = long.Parse(rentalPrice).ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
                string purchasePriceFormatted = price.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
                
                string statusClass = status == "Active" ? "status-active" : "status-inactive";
                string statusText = status == "Active" ? "Có sẵn" : "Không có sẵn";
                
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
                                <div class=""info-row"">
                                    <span class=""info-icon"">📦</span>
                                    <span>Danh mục: {category}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">🏷️</span>
                                    <span>Thương hiệu: {brand}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">🔧</span>
                                    <span>Model: {model}</span>
                                </div>
                                <div class=""info-row"">
                                    <span class=""info-icon"">🏢</span>
                                    <span>Chi nhánh: {branchId}</span>
                                </div>
                            </div>
                            
                            {(string.IsNullOrEmpty(description) ? "" : $@"<div class=""equipment-description"">{description}</div>")}
                            
                            <div class=""equipment-stats"">
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{quantityTotal}</div>
                                    <div class=""stat-label"">Tổng SL</div>
                                </div>
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{quantityAvailable}</div>
                                    <div class=""stat-label"">Có sẵn</div>
                                </div>
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{rentalPriceFormatted}</div>
                                    <div class=""stat-label"">Giá thuê</div>
                                </div>
                                <div class=""stat-item"">
                                    <div class=""stat-value"">{purchasePriceFormatted}</div>
                                    <div class=""stat-label"">Giá mua</div>
                                </div>
                            </div>
                        </div>
                    </div>");
            }
            
            string totalValueFormatted = totalValue.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
            
            // Tạo HTML hoàn chỉnh
            string html = GetEquipmentsHtmlTemplate(
                equipments.Count,
                totalQuantity,
                totalAvailable,
                totalValueFormatted,
                equipmentsHtml.ToString()
            );
            
            Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
            File.WriteAllText(htmlPath, html, Encoding.UTF8);
        }
        
        private static string GetNodeValue(XmlNode parentNode, string xpath)
        {
            XmlNode node = parentNode.SelectSingleNode(xpath);
            return node?.InnerText ?? "";
        }
        
        private static string GetAttributeValue(XmlNode node, string attributeName)
        {
            return node?.Attributes?[attributeName]?.Value ?? "";
        }
        
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
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f5f5f5; min-height: 100vh; }}
        .header {{ background: url('https://images.unsplash.com/photo-1529900748604-07564a03e7a6?w=1600&q=80') center/cover; color: white; padding: 80px 20px; box-shadow: 0 4px 20px rgba(0,0,0,0.15); position: relative; overflow: hidden; }}
        .header-content {{ max-width: 1400px; margin: 0 auto; text-align: center; position: relative; z-index: 2; }}
        .header h1 {{ font-size: 3em; margin-bottom: 15px; font-weight: 700; text-shadow: 2px 2px 8px rgba(0,0,0,0.3); letter-spacing: 1px; }}
        .header p {{ font-size: 1.3em; opacity: 0.95; text-shadow: 1px 1px 4px rgba(0,0,0,0.2); font-weight: 300; }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); display: flex; justify-content: space-around; flex-wrap: wrap; gap: 20px; }}
        .stat-item {{ text-align: center; }}
        .stat-value {{ font-size: 2em; font-weight: bold; color: #4CAF50; }}
        .stat-label {{ color: #666; font-size: 0.9em; margin-top: 5px; }}
        .toolbar {{ background: white; padding: 25px; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); margin-bottom: 25px; }}
        .search-box {{ position: relative; margin-bottom: 20px; }}
        .search-icon {{ position: absolute; left: 15px; top: 50%; transform: translateY(-50%); font-size: 1.2em; }}
        #searchInput {{ width: 100%; padding: 15px 15px 15px 50px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 1em; transition: all 0.3s ease; }}
        #searchInput:focus {{ outline: none; border-color: #4CAF50; box-shadow: 0 0 0 3px rgba(76, 175, 80, 0.1); }}
        .filters {{ display: flex; gap: 15px; flex-wrap: wrap; align-items: center; }}
        .filter-select {{ flex: 1; min-width: 180px; padding: 12px 15px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 0.95em; background: white; cursor: pointer; transition: all 0.3s ease; }}
        .filter-select:hover {{ border-color: #4CAF50; }}
        .filter-select:focus {{ outline: none; border-color: #4CAF50; box-shadow: 0 0 0 3px rgba(76, 175, 80, 0.1); }}
        .reset-btn {{ padding: 12px 25px; background: #f44336; color: white; border: none; border-radius: 10px; font-size: 0.95em; font-weight: 600; cursor: pointer; transition: all 0.3s ease; white-space: nowrap; }}
        .reset-btn:hover {{ background: #d32f2f; transform: translateY(-2px); box-shadow: 0 4px 12px rgba(244, 67, 54, 0.3); }}
        .result-info {{ background: #e8f5e9; padding: 15px 20px; border-radius: 10px; margin-bottom: 20px; color: #2e7d32; font-size: 0.95em; border-left: 4px solid #4CAF50; display: none; }}
        .no-results {{ text-align: center; padding: 60px 20px; background: white; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
        .no-results-icon {{ font-size: 4em; margin-bottom: 20px; }}
        .no-results-text {{ font-size: 1.3em; color: #666; margin-bottom: 10px; }}
        .no-results-hint {{ color: #999; font-size: 0.95em; }}
        .branches-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .branch-card {{ background: white; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: all 0.3s ease; position: relative; border: 1px solid #e0e0e0; }}
        .branch-card:hover {{ transform: translateY(-8px); box-shadow: 0 8px 25px rgba(76, 175, 80, 0.2); border-color: #4CAF50; }}
        .branch-image {{ width: 100%; height: 220px; object-fit: cover; background: linear-gradient(135deg, #e8f5e9 0%, #c8e6c9 100%); }}
        .branch-content {{ padding: 25px; }}
        .branch-name {{ font-size: 1.5em; font-weight: bold; color: #333; margin-bottom: 5px; }}
        .branch-code {{ display: inline-block; background: #4CAF50; color: white; padding: 6px 14px; border-radius: 20px; font-size: 0.85em; font-weight: 600; }}
        .status-badge {{ position: absolute; top: 15px; right: 15px; padding: 8px 16px; border-radius: 20px; font-size: 0.85em; font-weight: 600; text-transform: uppercase; box-shadow: 0 2px 8px rgba(0,0,0,0.15); }}
        .status-active {{ background: #4CAF50; color: white; }}
        .status-inactive {{ background: #f44336; color: white; }}
        .branch-info {{ margin: 15px 0; }}
        .info-row {{ display: flex; align-items: center; margin: 10px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; color: #4CAF50; }}
        .branch-description {{ color: #666; font-size: 0.9em; line-height: 1.6; margin: 15px 0; padding: 15px; background: #f1f8f4; border-radius: 8px; border-left: 4px solid #4CAF50; }}
        .branch-stats {{ display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px; margin-top: 20px; padding-top: 20px; border-top: 2px solid #f0f0f0; }}
        .branch-stats .stat-item {{ text-align: center; }}
        .branch-stats .stat-value {{ font-size: 1.5em; font-weight: bold; color: #4CAF50; }}
        .branch-stats .stat-label {{ font-size: 0.8em; color: #888; margin-top: 5px; }}
        @media (max-width: 768px) {{ .branches-grid {{ grid-template-columns: 1fr; }} .header h1 {{ font-size: 2em; }} }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""header-content"">
            <h1>Hệ Thống Chi Nhánh</h1>
            <p>Quản lý sân bóng chuyên nghiệp</p>
        </div>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-item""><div class=""stat-value"" id=""totalBranches"">{totalBranches}</div><div class=""stat-label"">Tổng chi nhánh</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""activeBranches"">{activeBranches}</div><div class=""stat-label"">Đang hoạt động</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""totalStaff"">{totalStaff}</div><div class=""stat-label"">Tổng nhân viên</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""totalRevenue"">{totalRevenue}</div><div class=""stat-label"">Doanh thu/tháng (VNĐ)</div></div>
        </div>
        
        <div class=""toolbar"">
            <div class=""search-box"">
                <span class=""search-icon"">🔍</span>
                <input type=""text"" id=""searchInput"" placeholder=""Tìm kiếm theo tên, thành phố, quản lý..."" />
            </div>
            <div class=""filters"">
                <select id=""cityFilter"" class=""filter-select"">
                    <option value="""">Tất cả thành phố</option>
                </select>
                <select id=""statusFilter"" class=""filter-select"">
                    <option value="""">Tất cả trạng thái</option>
                    <option value=""Active"">Đang hoạt động</option>
                    <option value=""Inactive"">Tạm đóng</option>
                </select>
                <select id=""sortBy"" class=""filter-select"">
                    <option value=""name-asc"">Tên A-Z</option>
                    <option value=""name-desc"">Tên Z-A</option>
                    <option value=""revenue-desc"">Doanh thu cao → thấp</option>
                    <option value=""revenue-asc"">Doanh thu thấp → cao</option>
                    <option value=""staff-desc"">Nhân viên nhiều nhất</option>
                </select>
                <button id=""resetBtn"" class=""reset-btn"">🔄 Đặt lại</button>
            </div>
        </div>
        
        <div id=""resultInfo"" class=""result-info"">
            Hiển thị <strong id=""resultCount"">0</strong> kết quả
        </div>
        
        <div id=""branchesContainer"" class=""branches-grid"">
{branchesContent}
        </div>
    </div>
    
    <script>
        // Lưu tất cả branches để filter
        const allBranches = [];
        document.querySelectorAll('.branch-card').forEach(card => {{
            const name = card.querySelector('.branch-name').textContent;
            const city = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.split(',').pop().trim();
            const manager = card.querySelector('.info-row:nth-child(4) span:last-child').textContent.replace('Quản lý: ', '');
            const status = card.querySelector('.status-badge').classList.contains('status-active') ? 'Active' : 'Inactive';
            const revenue = parseInt(card.querySelector('.branch-stats .stat-item:nth-child(3) .stat-value').textContent.replace(/\./g, ''));
            const staff = parseInt(card.querySelector('.branch-stats .stat-item:nth-child(2) .stat-value').textContent);
            
            allBranches.push({{
                element: card,
                name: name.toLowerCase(),
                city: city,
                manager: manager.toLowerCase(),
                status: status,
                revenue: revenue,
                staff: staff
            }});
        }});
        
        // Populate city filter
        const cities = [...new Set(allBranches.map(b => b.city))];
        const cityFilter = document.getElementById('cityFilter');
        cities.forEach(city => {{
            const option = document.createElement('option');
            option.value = city;
            option.textContent = city;
            cityFilter.appendChild(option);
        }});
        
        // Event listeners
        document.getElementById('searchInput').addEventListener('input', filterBranches);
        document.getElementById('cityFilter').addEventListener('change', filterBranches);
        document.getElementById('statusFilter').addEventListener('change', filterBranches);
        document.getElementById('sortBy').addEventListener('change', filterBranches);
        document.getElementById('resetBtn').addEventListener('click', resetFilters);
        
        function resetFilters() {{
            document.getElementById('searchInput').value = '';
            document.getElementById('cityFilter').value = '';
            document.getElementById('statusFilter').value = '';
            document.getElementById('sortBy').value = 'name-asc';
            filterBranches();
        }}
        
        function filterBranches() {{
            const searchText = document.getElementById('searchInput').value.toLowerCase();
            const cityValue = document.getElementById('cityFilter').value;
            const statusValue = document.getElementById('statusFilter').value;
            const sortValue = document.getElementById('sortBy').value;
            
            let filtered = allBranches.filter(branch => {{
                const matchSearch = !searchText || branch.name.includes(searchText) || 
                                   branch.city.toLowerCase().includes(searchText) || 
                                   branch.manager.includes(searchText);
                const matchCity = !cityValue || branch.city === cityValue;
                const matchStatus = !statusValue || branch.status === statusValue;
                
                return matchSearch && matchCity && matchStatus;
            }});
            
            // Sort
            filtered.sort((a, b) => {{
                switch(sortValue) {{
                    case 'name-asc': return a.name.localeCompare(b.name);
                    case 'name-desc': return b.name.localeCompare(a.name);
                    case 'revenue-asc': return a.revenue - b.revenue;
                    case 'revenue-desc': return b.revenue - a.revenue;
                    case 'staff-desc': return b.staff - a.staff;
                    default: return 0;
                }}
            }});
            
            displayBranches(filtered);
        }}
        
        function displayBranches(branches) {{
            const container = document.getElementById('branchesContainer');
            const resultInfo = document.getElementById('resultInfo');
            const resultCount = document.getElementById('resultCount');
            
            // Hide all cards first
            allBranches.forEach(b => b.element.style.display = 'none');
            
            if (branches.length === 0) {{
                container.innerHTML = `
                    <div class=""no-results"">
                        <div class=""no-results-icon"">🔍</div>
                        <div class=""no-results-text"">Không tìm thấy kết quả</div>
                        <div class=""no-results-hint"">Thử thay đổi bộ lọc hoặc từ khóa tìm kiếm</div>
                    </div>
                `;
                resultInfo.style.display = 'none';
                return;
            }}
            
            resultInfo.style.display = 'block';
            resultCount.textContent = branches.length;
            
            // Clear container and re-add filtered cards
            container.innerHTML = '';
            branches.forEach(branch => {{
                container.appendChild(branch.element);
                branch.element.style.display = 'block';
            }});
        }}
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
    <title>Danh Sách Dụng Cụ - Hệ Thống Sân Bóng</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f5f5f5; min-height: 100vh; }}
        .header {{ background: url('https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=1600&q=80') center/cover; color: white; padding: 80px 20px; box-shadow: 0 4px 20px rgba(0,0,0,0.15); position: relative; overflow: hidden; }}
        .header-content {{ max-width: 1400px; margin: 0 auto; text-align: center; position: relative; z-index: 2; }}
        .header h1 {{ font-size: 3em; margin-bottom: 15px; font-weight: 700; text-shadow: 2px 2px 8px rgba(0,0,0,0.3); letter-spacing: 1px; }}
        .header p {{ font-size: 1.3em; opacity: 0.95; text-shadow: 1px 1px 4px rgba(0,0,0,0.2); font-weight: 300; }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); display: flex; justify-content: space-around; flex-wrap: wrap; gap: 20px; }}
        .stat-item {{ text-align: center; }}
        .stat-value {{ font-size: 2em; font-weight: bold; color: #2196F3; }}
        .stat-label {{ color: #666; font-size: 0.9em; margin-top: 5px; }}
        .toolbar {{ background: white; padding: 25px; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); margin-bottom: 25px; }}
        .search-box {{ position: relative; margin-bottom: 20px; }}
        .search-icon {{ position: absolute; left: 15px; top: 50%; transform: translateY(-50%); font-size: 1.2em; }}
        #searchInput {{ width: 100%; padding: 15px 15px 15px 50px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 1em; transition: all 0.3s ease; }}
        #searchInput:focus {{ outline: none; border-color: #2196F3; box-shadow: 0 0 0 3px rgba(33, 150, 243, 0.1); }}
        .filters {{ display: flex; gap: 15px; flex-wrap: wrap; align-items: center; }}
        .filter-select {{ flex: 1; min-width: 180px; padding: 12px 15px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 0.95em; background: white; cursor: pointer; transition: all 0.3s ease; }}
        .filter-select:hover {{ border-color: #2196F3; }}
        .filter-select:focus {{ outline: none; border-color: #2196F3; box-shadow: 0 0 0 3px rgba(33, 150, 243, 0.1); }}
        .reset-btn {{ padding: 12px 25px; background: #f44336; color: white; border: none; border-radius: 10px; font-size: 0.95em; font-weight: 600; cursor: pointer; transition: all 0.3s ease; white-space: nowrap; }}
        .reset-btn:hover {{ background: #d32f2f; transform: translateY(-2px); box-shadow: 0 4px 12px rgba(244, 67, 54, 0.3); }}
        .result-info {{ background: #e3f2fd; padding: 15px 20px; border-radius: 10px; margin-bottom: 20px; color: #1565c0; font-size: 0.95em; border-left: 4px solid #2196F3; display: none; }}
        .no-results {{ text-align: center; padding: 60px 20px; background: white; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
        .no-results-icon {{ font-size: 4em; margin-bottom: 20px; }}
        .no-results-text {{ font-size: 1.3em; color: #666; margin-bottom: 10px; }}
        .no-results-hint {{ color: #999; font-size: 0.95em; }}
        .branches-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .equipment-card {{ background: white; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: all 0.3s ease; position: relative; border: 1px solid #e0e0e0; }}
        .equipment-card:hover {{ transform: translateY(-8px); box-shadow: 0 8px 25px rgba(33, 150, 243, 0.2); border-color: #2196F3; }}
        .equipment-image {{ width: 100%; height: 220px; object-fit: cover; background: linear-gradient(135deg, #e3f2fd 0%, #bbdefb 100%); }}
        .equipment-content {{ padding: 25px; }}
        .equipment-name {{ font-size: 1.5em; font-weight: bold; color: #333; margin-bottom: 5px; }}
        .equipment-code {{ display: inline-block; background: #2196F3; color: white; padding: 6px 14px; border-radius: 20px; font-size: 0.85em; font-weight: 600; }}
        .status-badge {{ position: absolute; top: 15px; right: 15px; padding: 8px 16px; border-radius: 20px; font-size: 0.85em; font-weight: 600; text-transform: uppercase; box-shadow: 0 2px 8px rgba(0,0,0,0.15); }}
        .status-active {{ background: #4CAF50; color: white; }}
        .status-inactive {{ background: #f44336; color: white; }}
        .equipment-info {{ margin: 15px 0; }}
        .info-row {{ display: flex; align-items: center; margin: 10px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; color: #2196F3; }}
        .equipment-description {{ color: #666; font-size: 0.9em; line-height: 1.6; margin: 15px 0; padding: 15px; background: #e3f2fd; border-radius: 8px; border-left: 4px solid #2196F3; }}
        .equipment-stats {{ display: grid; grid-template-columns: repeat(4, 1fr); gap: 10px; margin-top: 20px; padding-top: 20px; border-top: 2px solid #f0f0f0; }}
        .equipment-stats .stat-item {{ text-align: center; }}
        .equipment-stats .stat-value {{ font-size: 1.3em; font-weight: bold; color: #2196F3; }}
        .equipment-stats .stat-label {{ font-size: 0.75em; color: #888; margin-top: 5px; }}
        @media (max-width: 768px) {{ .branches-grid {{ grid-template-columns: 1fr; }} .header h1 {{ font-size: 2em; }} }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""header-content"">
            <h1>Hệ Thống Dụng Cụ</h1>
            <p>Quản lý thiết bị sân bóng</p>
        </div>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-item""><div class=""stat-value"" id=""totalEquipments"">{totalEquipments}</div><div class=""stat-label"">Tổng loại dụng cụ</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""totalQuantity"">{totalQuantity}</div><div class=""stat-label"">Tổng số lượng</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""totalAvailable"">{totalAvailable}</div><div class=""stat-label"">Có sẵn</div></div>
            <div class=""stat-item""><div class=""stat-value"" id=""totalValue"">{totalValue}</div><div class=""stat-label"">Tổng giá trị (VNĐ)</div></div>
        </div>
        
        <div class=""toolbar"">
            <div class=""search-box"">
                <span class=""search-icon"">🔍</span>
                <input type=""text"" id=""searchInput"" placeholder=""Tìm kiếm theo tên, danh mục, thương hiệu..."" />
            </div>
            <div class=""filters"">
                <select id=""categoryFilter"" class=""filter-select"">
                    <option value="""">Tất cả danh mục</option>
                </select>
                <select id=""brandFilter"" class=""filter-select"">
                    <option value="""">Tất cả thương hiệu</option>
                </select>
                <select id=""statusFilter"" class=""filter-select"">
                    <option value="""">Tất cả trạng thái</option>
                    <option value=""Active"">Có sẵn</option>
                    <option value=""Inactive"">Không có sẵn</option>
                </select>
                <select id=""sortBy"" class=""filter-select"">
                    <option value=""name-asc"">Tên A-Z</option>
                    <option value=""name-desc"">Tên Z-A</option>
                    <option value=""quantity-desc"">Số lượng nhiều nhất</option>
                    <option value=""price-desc"">Giá cao → thấp</option>
                    <option value=""price-asc"">Giá thấp → cao</option>
                </select>
                <button id=""resetBtn"" class=""reset-btn"">🔄 Đặt lại</button>
            </div>
        </div>
        
        <div id=""resultInfo"" class=""result-info"">
            Hiển thị <strong id=""resultCount"">0</strong> kết quả
        </div>
        
        <div id=""equipmentsContainer"" class=""branches-grid"">
{equipmentsContent}
        </div>
    </div>
    
    <script>
        // Lưu tất cả equipments để filter
        const allEquipments = [];
        document.querySelectorAll('.equipment-card').forEach(card => {{
            const name = card.querySelector('.equipment-name').textContent;
            const category = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.replace('Danh mục: ', '');
            const brand = card.querySelector('.info-row:nth-child(2) span:last-child').textContent.replace('Thương hiệu: ', '');
            const status = card.querySelector('.status-badge').classList.contains('status-active') ? 'Active' : 'Inactive';
            const quantity = parseInt(card.querySelector('.equipment-stats .stat-item:nth-child(1) .stat-value').textContent);
            const price = parseInt(card.querySelector('.equipment-stats .stat-item:nth-child(4) .stat-value').textContent.replace(/\./g, ''));
            
            allEquipments.push({{
                element: card,
                name: name.toLowerCase(),
                category: category,
                brand: brand,
                status: status,
                quantity: quantity,
                price: price
            }});
        }});
        
        // Populate filters
        const categories = [...new Set(allEquipments.map(e => e.category))];
        const brands = [...new Set(allEquipments.map(e => e.brand))];
        
        const categoryFilter = document.getElementById('categoryFilter');
        categories.forEach(cat => {{
            const option = document.createElement('option');
            option.value = cat;
            option.textContent = cat;
            categoryFilter.appendChild(option);
        }});
        
        const brandFilter = document.getElementById('brandFilter');
        brands.forEach(brand => {{
            const option = document.createElement('option');
            option.value = brand;
            option.textContent = brand;
            brandFilter.appendChild(option);
        }});
        
        // Event listeners
        document.getElementById('searchInput').addEventListener('input', filterEquipments);
        document.getElementById('categoryFilter').addEventListener('change', filterEquipments);
        document.getElementById('brandFilter').addEventListener('change', filterEquipments);
        document.getElementById('statusFilter').addEventListener('change', filterEquipments);
        document.getElementById('sortBy').addEventListener('change', filterEquipments);
        document.getElementById('resetBtn').addEventListener('click', resetFilters);
        
        function resetFilters() {{
            document.getElementById('searchInput').value = '';
            document.getElementById('categoryFilter').value = '';
            document.getElementById('brandFilter').value = '';
            document.getElementById('statusFilter').value = '';
            document.getElementById('sortBy').value = 'name-asc';
            filterEquipments();
        }}
        
        function filterEquipments() {{
            const searchText = document.getElementById('searchInput').value.toLowerCase();
            const categoryValue = document.getElementById('categoryFilter').value;
            const brandValue = document.getElementById('brandFilter').value;
            const statusValue = document.getElementById('statusFilter').value;
            const sortValue = document.getElementById('sortBy').value;
            
            let filtered = allEquipments.filter(equipment => {{
                const matchSearch = !searchText || equipment.name.includes(searchText) || 
                                   equipment.category.toLowerCase().includes(searchText) || 
                                   equipment.brand.toLowerCase().includes(searchText);
                const matchCategory = !categoryValue || equipment.category === categoryValue;
                const matchBrand = !brandValue || equipment.brand === brandValue;
                const matchStatus = !statusValue || equipment.status === statusValue;
                
                return matchSearch && matchCategory && matchBrand && matchStatus;
            }});
            
            // Sort
            filtered.sort((a, b) => {{
                switch(sortValue) {{
                    case 'name-asc': return a.name.localeCompare(b.name);
                    case 'name-desc': return b.name.localeCompare(a.name);
                    case 'quantity-desc': return b.quantity - a.quantity;
                    case 'price-asc': return a.price - b.price;
                    case 'price-desc': return b.price - a.price;
                    default: return 0;
                }}
            }});
            
            displayEquipments(filtered);
        }}
        
        function displayEquipments(equipments) {{
            const container = document.getElementById('equipmentsContainer');
            const resultInfo = document.getElementById('resultInfo');
            const resultCount = document.getElementById('resultCount');
            
            // Hide all cards first
            allEquipments.forEach(e => e.element.style.display = 'none');
            
            if (equipments.length === 0) {{
                container.innerHTML = `
                    <div class=""no-results"">
                        <div class=""no-results-icon"">🔍</div>
                        <div class=""no-results-text"">Không tìm thấy kết quả</div>
                        <div class=""no-results-hint"">Thử thay đổi bộ lọc hoặc từ khóa tìm kiếm</div>
                    </div>
                `;
                resultInfo.style.display = 'none';
                return;
            }}
            
            resultInfo.style.display = 'block';
            resultCount.textContent = equipments.length;
            
            // Clear container and re-add filtered cards
            container.innerHTML = '';
            equipments.forEach(equipment => {{
                container.appendChild(equipment.element);
                equipment.element.style.display = 'block';
            }});
        }}
    </script>
</body>
</html>";
        }
    }
}
