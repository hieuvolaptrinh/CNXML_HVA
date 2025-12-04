// Sidebar component - reusable
function createSidebar(activePage) {
  return `
    <div style="position: fixed; top: 0; bottom: 0; left: 0; width: 256px; background: linear-gradient(180deg, #1e40af 0%, #1e3a8a 100%); color: white; box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25); z-index: 50;">
        <div style="padding: 24px;">
            <div style="display: flex; align-items: center; gap: 12px; margin-bottom: 32px;">
                <i class="fas fa-futbol" style="font-size: 1.875rem;"></i>
                <div>
                    <h1 style="font-size: 1.25rem; font-weight: bold; margin: 0;">Sân Bóng</h1>
                    <p style="font-size: 0.75rem; color: #bfdbfe; margin: 0;">Management System</p>
                </div>
            </div>

            <nav style="display: flex; flex-direction: column; gap: 8px;">
                <a href="dashboard.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "dashboard" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'dashboard') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'dashboard') this.style.background='transparent'">
                    <i class="fas fa-chart-line"></i><span>Dashboard</span>
                </a>
                <a href="branches.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "branches" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'branches') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'branches') this.style.background='transparent'">
                    <i class="fas fa-building"></i><span>Chi nhánh</span>
                </a>
                <a href="fields.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "fields" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'fields') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'fields') this.style.background='transparent'">
                    <i class="fas fa-map-marked-alt"></i><span>Sân bóng</span>
                </a>
                <a href="field-types.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "field-types" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'field-types') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'field-types') this.style.background='transparent'">
                    <i class="fas fa-list"></i><span>Loại sân</span>
                </a>
                <a href="bookings.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "bookings" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'bookings') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'bookings') this.style.background='transparent'">
                    <i class="fas fa-calendar-check"></i><span>Đặt sân</span>
                </a>
                <a href="equipments.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "equipments" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'equipments') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'equipments') this.style.background='transparent'">
                    <i class="fas fa-toolbox"></i><span>Thiết bị</span>
                </a>
                <a href="customers.html" style="display: flex; align-items: center; gap: 12px; padding: 12px 16px; background: ${
                  activePage === "customers" ? "#1d4ed8" : "transparent"
                }; border-radius: 8px; transition: background 0.3s; text-decoration: none; color: white;" onmouseover="if('${activePage}' !== 'customers') this.style.background='#1d4ed8'" onmouseout="if('${activePage}' !== 'customers') this.style.background='transparent'">
                    <i class="fas fa-users"></i><span>Khách hàng</span>
                </a>
            </nav>
        </div>

        <div style="position: absolute; bottom: 0; left: 0; right: 0; padding: 24px; border-top: 1px solid #1d4ed8;">
            <div style="display: flex; align-items: center; gap: 12px; margin-bottom: 12px;">
                <div style="width: 40px; height: 40px; background: #2563eb; border-radius: 50%; display: flex; align-items: center; justify-content: center;">
                    <i class="fas fa-user"></i>
                </div>
                <div style="flex: 1; min-width: 0;">
                    <p style="font-size: 0.875rem; font-weight: 600; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0;" id="user-name">Admin</p>
                    <p style="font-size: 0.75rem; color: #bfdbfe; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0;" id="user-role">Administrator</p>
                </div>
            </div>
            <button onclick="handleLogout()" style="width: 100%; padding: 8px 16px; background: #dc2626; border: none; border-radius: 8px; transition: background 0.3s; display: flex; align-items: center; justify-content: center; gap: 8px; color: white; font-size: 0.875rem; cursor: pointer;" onmouseover="this.style.background='#b91c1c'" onmouseout="this.style.background='#dc2626'">
                <i class="fas fa-sign-out-alt"></i><span>Đăng xuất</span>
            </button>
        </div>
    </div>
    `;
}

// Export
window.createSidebar = createSidebar;
