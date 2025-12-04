// Sidebar component - reusable
function createSidebar(activePage) {
  return `
    <div class="fixed inset-y-0 left-0 w-64 bg-gradient-to-b from-blue-800 to-blue-900 text-white shadow-2xl z-50">
        <div class="p-6">
            <div class="flex items-center space-x-3 mb-8">
                <i class="fas fa-futbol text-3xl"></i>
                <div>
                    <h1 class="text-xl font-bold">Sân Bóng</h1>
                    <p class="text-xs text-blue-200">Management System</p>
                </div>
            </div>

            <nav class="space-y-2">
                <a href="dashboard.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "dashboard"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-chart-line"></i><span>Dashboard</span>
                </a>
                <a href="branches.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "branches"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-building"></i><span>Chi nhánh</span>
                </a>
                <a href="fields.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "fields" ? "bg-blue-700" : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-map-marked-alt"></i><span>Sân bóng</span>
                </a>
                <a href="field-types.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "field-types"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-list"></i><span>Loại sân</span>
                </a>
                <a href="bookings.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "bookings"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-calendar-check"></i><span>Đặt sân</span>
                </a>
                <a href="equipments.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "equipments"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-toolbox"></i><span>Thiết bị</span>
                </a>
                <a href="customers.html" class="flex items-center space-x-3 px-4 py-3 ${
                  activePage === "customers"
                    ? "bg-blue-700"
                    : "hover:bg-blue-700"
                } rounded-lg transition">
                    <i class="fas fa-users"></i><span>Khách hàng</span>
                </a>
            </nav>
        </div>

        <div class="absolute bottom-0 left-0 right-0 p-6 border-t border-blue-700">
            <div class="flex items-center space-x-3 mb-3">
                <div class="w-10 h-10 bg-blue-600 rounded-full flex items-center justify-center">
                    <i class="fas fa-user"></i>
                </div>
                <div class="flex-1 min-w-0">
                    <p class="text-sm font-semibold truncate" id="user-name">Admin</p>
                    <p class="text-xs text-blue-200 truncate" id="user-role">Administrator</p>
                </div>
            </div>
            <button onclick="handleLogout()" class="w-full px-4 py-2 bg-red-600 hover:bg-red-700 rounded-lg transition flex items-center justify-center space-x-2">
                <i class="fas fa-sign-out-alt"></i><span>Đăng xuất</span>
            </button>
        </div>
    </div>
    `;
}

// Export
window.createSidebar = createSidebar;
