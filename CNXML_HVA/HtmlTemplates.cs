using System;

namespace CNXML_HVA
{
    public static partial class HtmlGenerator
    {
        private static string GetBookingsHtmlTemplate(int totalBookings, string bookingsContent)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Danh Sách Đặt Lịch - Hệ Thống Sân Bóng</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f5f5f5; min-height: 100vh; }}
        .header {{ background: linear-gradient(135deg, #FF5722 0%, #FF9800 100%); color: white; padding: 80px 20px; box-shadow: 0 4px 20px rgba(0,0,0,0.15); }}
        .header-content {{ max-width: 1400px; margin: 0 auto; text-align: center; }}
        .header h1 {{ font-size: 3em; margin-bottom: 15px; font-weight: 700; text-shadow: 2px 2px 8px rgba(0,0,0,0.3); }}
        .header p {{ font-size: 1.3em; opacity: 0.95; text-shadow: 1px 1px 4px rgba(0,0,0,0.2); }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); text-align: center; }}
        .stat-value {{ font-size: 2.5em; font-weight: bold; color: #FF5722; }}
        .stat-label {{ color: #666; font-size: 1em; margin-top: 5px; }}
        .toolbar {{ background: white; padding: 25px; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); margin-bottom: 25px; }}
        .search-box {{ position: relative; }}
        .search-icon {{ position: absolute; left: 15px; top: 50%; transform: translateY(-50%); font-size: 1.2em; }}
        #searchInput {{ width: 100%; padding: 15px 15px 15px 50px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 1em; }}
        #searchInput:focus {{ outline: none; border-color: #FF5722; box-shadow: 0 0 0 3px rgba(255, 87, 34, 0.1); }}
        .bookings-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .booking-card {{ background: white; border-radius: 15px; padding: 25px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: all 0.3s ease; border: 1px solid #e0e0e0; }}
        .booking-card:hover {{ transform: translateY(-8px); box-shadow: 0 8px 25px rgba(255, 87, 34, 0.2); border-color: #FF5722; }}
        .booking-header {{ display: flex; justify-content: space-between; align-items: center; margin-bottom: 15px; }}
        .booking-id {{ font-size: 1.3em; font-weight: bold; color: #FF5722; }}
        .booking-type {{ background: #FF9800; color: white; padding: 6px 14px; border-radius: 20px; font-size: 0.85em; font-weight: 600; }}
        .booking-info {{ margin: 15px 0; }}
        .info-row {{ display: flex; align-items: center; margin: 10px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; }}
        .booking-note {{ color: #666; font-size: 0.9em; line-height: 1.6; margin-top: 15px; padding: 15px; background: #fff3e0; border-radius: 8px; border-left: 4px solid #FF9800; }}
        @media (max-width: 768px) {{ .bookings-grid {{ grid-template-columns: 1fr; }} }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""header-content"">
            <h1>Danh Sách Đặt Lịch</h1>
            <p>Quản lý lịch đặt sân</p>
        </div>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-value"">{totalBookings}</div>
            <div class=""stat-label"">Tổng số lịch đặt</div>
        </div>
        <div class=""toolbar"">
            <div class=""search-box"">
                <span class=""search-icon"">🔍</span>
                <input type=""text"" id=""searchInput"" placeholder=""Tìm kiếm theo khách hàng, sân..."" />
            </div>
        </div>
        <div id=""bookingsContainer"" class=""bookings-grid"">
{bookingsContent}
        </div>
    </div>
    <script>
        const allBookings = [];
        document.querySelectorAll('.booking-card').forEach(card => {{
            const customer = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.replace('Khách hàng: ', '');
            const field = card.querySelector('.info-row:nth-child(2) span:last-child').textContent.replace('Sân: ', '');
            allBookings.push({{ element: card, customer: customer.toLowerCase(), field: field.toLowerCase() }});
        }});
        document.getElementById('searchInput').addEventListener('input', function() {{
            const searchText = this.value.toLowerCase();
            allBookings.forEach(booking => {{
                const match = !searchText || booking.customer.includes(searchText) || booking.field.includes(searchText);
                booking.element.style.display = match ? 'block' : 'none';
            }});
        }});
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
    <title>Danh Sách Khách Hàng - Hệ Thống Sân Bóng</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f5f5f5; min-height: 100vh; }}
        .header {{ background: linear-gradient(135deg, #2196F3 0%, #03A9F4 100%); color: white; padding: 80px 20px; box-shadow: 0 4px 20px rgba(0,0,0,0.15); }}
        .header-content {{ max-width: 1400px; margin: 0 auto; text-align: center; }}
        .header h1 {{ font-size: 3em; margin-bottom: 15px; font-weight: 700; text-shadow: 2px 2px 8px rgba(0,0,0,0.3); }}
        .header p {{ font-size: 1.3em; opacity: 0.95; text-shadow: 1px 1px 4px rgba(0,0,0,0.2); }}
        .container {{ max-width: 1400px; margin: 0 auto; padding: 30px 20px; }}
        .stats-bar {{ background: white; padding: 20px; border-radius: 10px; margin-bottom: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); text-align: center; }}
        .stat-value {{ font-size: 2.5em; font-weight: bold; color: #2196F3; }}
        .stat-label {{ color: #666; font-size: 1em; margin-top: 5px; }}
        .toolbar {{ background: white; padding: 25px; border-radius: 15px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); margin-bottom: 25px; }}
        .search-box {{ position: relative; }}
        .search-icon {{ position: absolute; left: 15px; top: 50%; transform: translateY(-50%); font-size: 1.2em; }}
        #searchInput {{ width: 100%; padding: 15px 15px 15px 50px; border: 2px solid #e0e0e0; border-radius: 10px; font-size: 1em; }}
        #searchInput:focus {{ outline: none; border-color: #2196F3; box-shadow: 0 0 0 3px rgba(33, 150, 243, 0.1); }}
        .customers-grid {{ display: grid; grid-template-columns: repeat(auto-fill, minmax(360px, 1fr)); gap: 25px; }}
        .customer-card {{ background: white; border-radius: 15px; padding: 25px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); transition: all 0.3s ease; border: 1px solid #e0e0e0; }}
        .customer-card:hover {{ transform: translateY(-8px); box-shadow: 0 8px 25px rgba(33, 150, 243, 0.2); border-color: #2196F3; }}
        .customer-header {{ display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px; }}
        .customer-name {{ font-size: 1.5em; font-weight: bold; color: #333; }}
        .membership-badge {{ padding: 6px 14px; border-radius: 20px; font-size: 0.85em; font-weight: 600; color: white; }}
        .membership-vip {{ background: linear-gradient(135deg, #FFD700 0%, #FFA500 100%); }}
        .membership-gold {{ background: linear-gradient(135deg, #C0C0C0 0%, #808080 100%); }}
        .membership-regular {{ background: linear-gradient(135deg, #90CAF9 0%, #42A5F5 100%); }}
        .customer-id {{ color: #2196F3; font-size: 0.9em; margin-bottom: 15px; font-weight: 600; }}
        .customer-info {{ margin: 15px 0; }}
        .info-row {{ display: flex; align-items: center; margin: 10px 0; color: #555; font-size: 0.95em; }}
        .info-icon {{ width: 20px; margin-right: 10px; }}
        .customer-note {{ color: #666; font-size: 0.9em; line-height: 1.6; margin-top: 15px; padding: 15px; background: #e3f2fd; border-radius: 8px; border-left: 4px solid #2196F3; }}
        @media (max-width: 768px) {{ .customers-grid {{ grid-template-columns: 1fr; }} }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""header-content"">
            <h1>Danh Sách Khách Hàng</h1>
            <p>Quản lý thông tin khách hàng</p>
        </div>
    </div>
    <div class=""container"">
        <div class=""stats-bar"">
            <div class=""stat-value"">{totalCustomers}</div>
            <div class=""stat-label"">Tổng số khách hàng</div>
        </div>
        <div class=""toolbar"">
            <div class=""search-box"">
                <span class=""search-icon"">🔍</span>
                <input type=""text"" id=""searchInput"" placeholder=""Tìm kiếm theo tên, số điện thoại, email..."" />
            </div>
        </div>
        <div id=""customersContainer"" class=""customers-grid"">
{customersContent}
        </div>
    </div>
    <script>
        const allCustomers = [];
        document.querySelectorAll('.customer-card').forEach(card => {{
            const name = card.querySelector('.customer-name').textContent.toLowerCase();
            const phone = card.querySelector('.info-row:nth-child(1) span:last-child').textContent.toLowerCase();
            const email = card.querySelector('.info-row:nth-child(2) span:last-child').textContent.toLowerCase();
            allCustomers.push({{ element: card, name: name, phone: phone, email: email }});
        }});
        document.getElementById('searchInput').addEventListener('input', function() {{
            const searchText = this.value.toLowerCase();
            allCustomers.forEach(customer => {{
                const match = !searchText || customer.name.includes(searchText) || 
                             customer.phone.includes(searchText) || customer.email.includes(searchText);
                customer.element.style.display = match ? 'block' : 'none';
            }});
        }});
    </script>
</body>
</html>";
        }
    }
}
