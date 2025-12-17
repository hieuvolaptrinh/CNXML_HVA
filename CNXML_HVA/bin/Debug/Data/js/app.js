// Authentication and XML utilities
class XMLManager {
  constructor() {
    // File XML giờ nằm cùng thư mục với HTML (không cần ../Data/)
    this.dataPath = "";
  }

  // Load XML file - Sử dụng XMLHttpRequest để tương thích với file:// protocol
  async loadXML(filename) {
    return new Promise((resolve, reject) => {
      try {
        const xhr = new XMLHttpRequest();
        const fullPath = this.dataPath + filename;

        xhr.onload = function () {
          if (xhr.status === 200 || xhr.status === 0) {
            // 0 cho file:// protocol
            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(xhr.responseText, "text/xml");

            // Kiểm tra lỗi parse
            const parseError = xmlDoc.getElementsByTagName("parsererror");
            if (parseError.length > 0) {
              console.error("XML Parse Error:", parseError[0].textContent);
              reject(new Error("Lỗi parse XML: " + filename));
            } else {
              console.log("✓ Loaded XML:", filename);
              resolve(xmlDoc);
            }
          } else {
            reject(new Error(`HTTP Error ${xhr.status}: ${filename}`));
          }
        };

        xhr.onerror = function () {
          console.error("XHR Error loading:", fullPath);
          reject(new Error("Không thể tải file: " + filename));
        };

        xhr.open("GET", fullPath, true);
        xhr.send();
      } catch (error) {
        console.error("Error in loadXML:", error);
        reject(error);
      }
    });
  }

  // Get element text content
  getElementText(element, tagName, defaultValue = "") {
    const el = element.getElementsByTagName(tagName)[0];
    return el ? el.textContent : defaultValue;
  }

  // Parse XML elements to array
  xmlToArray(xmlDoc, tagName) {
    const elements = xmlDoc.getElementsByTagName(tagName);
    return Array.from(elements);
  }
}

// Authentication Manager
class AuthManager {
  constructor() {
    this.currentUser = null;
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const userStr = sessionStorage.getItem("currentUser");
    if (userStr) {
      this.currentUser = JSON.parse(userStr);
    }
  }

  isAuthenticated() {
    return this.currentUser !== null;
  }

  requireAuth() {
    // No authentication required
    return true;
  }

  logout() {
    // No logout needed
    window.location.href = "dashboard.html";
  }

  getCurrentUser() {
    return this.currentUser;
  }
}

// Initialize global instances
const xmlManager = new XMLManager();
const authManager = new AuthManager();

// Check authentication on protected pages
function requireAuth() {
  // No authentication required - always return true
  const userNameElement = document.getElementById("user-name");
  const userRoleElement = document.getElementById("user-role");

  if (userNameElement) {
    userNameElement.textContent = "Admin";
  }
  if (userRoleElement) {
    userRoleElement.textContent = "Administrator";
  }

  return true;
}

// Logout handler
function handleLogout() {
  if (confirm("Bạn có chắc chắn muốn đăng xuất?")) {
    authManager.logout();
  }
}

// Format currency
function formatCurrency(amount) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(amount);
}

// Format date
function formatDate(dateString) {
  if (!dateString) return "";
  const date = new Date(dateString);
  return date.toLocaleDateString("vi-VN");
}

// Format datetime
function formatDateTime(dateString) {
  if (!dateString) return "";
  const date = new Date(dateString);
  return date.toLocaleString("vi-VN");
}

// Show notification
function showNotification(message, type = "success") {
  const notification = document.createElement("div");
  const bgColor =
    type === "success"
      ? "bg-green-500"
      : type === "error"
      ? "bg-red-500"
      : "bg-blue-500";

  notification.className = `fixed top-4 right-4 ${bgColor} text-white px-6 py-4 rounded-lg shadow-lg z-50 transform transition-all duration-300 translate-x-0`;
  notification.innerHTML = `
        <div class="flex items-center space-x-3">
            <i class="fas fa-${
              type === "success"
                ? "check-circle"
                : type === "error"
                ? "exclamation-circle"
                : "info-circle"
            } text-xl"></i>
            <span>${message}</span>
        </div>
    `;

  document.body.appendChild(notification);

  setTimeout(() => {
    notification.style.transform = "translateX(400px)";
    setTimeout(() => notification.remove(), 300);
  }, 3000);
}

// Confirm dialog
function confirmAction(message) {
  return confirm(message);
}

// Loading spinner
function showLoading() {
  const loading = document.createElement("div");
  loading.id = "loading-overlay";
  loading.className =
    "fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50";
  loading.innerHTML = `
        <div class="bg-white rounded-lg p-6">
            <i class="fas fa-spinner fa-spin text-4xl text-blue-600"></i>
            <p class="mt-4 text-gray-700">Đang xử lý...</p>
        </div>
    `;
  document.body.appendChild(loading);
}

function hideLoading() {
  const loading = document.getElementById("loading-overlay");
  if (loading) {
    loading.remove();
  }
}

// Export utilities
window.XMLManager = XMLManager;
window.AuthManager = AuthManager;
window.xmlManager = xmlManager;
window.authManager = authManager;
window.requireAuth = requireAuth;
window.handleLogout = handleLogout;
window.formatCurrency = formatCurrency;
window.formatDate = formatDate;
window.formatDateTime = formatDateTime;
window.showNotification = showNotification;
window.confirmAction = confirmAction;
window.showLoading = showLoading;
window.hideLoading = hideLoading;
