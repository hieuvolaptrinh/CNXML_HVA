// ============================================================================
// APP.JS - Quản lý đọc file XML và các tiện ích chung
// ============================================================================

// ============================================================================
// CLASS XMLManager - Đọc và xử lý file XML
// ============================================================================
class XMLManager {
  /**
   * Constructor - Khởi tạo XMLManager
   * dataPath: đường dẫn đến thư mục chứa file XML
   * Để trống vì XML nằm cùng thư mục với HTML
   */
  constructor() {
    this.dataPath = "";
  }

  /**
   * loadXML - Hàm bất đồng bộ để đọc file XML
   * @param {string} filename - Tên file XML cần đọc (vd: "Customers.xml")
   * @returns {Promise<XMLDocument>} - Trả về XML Document đã parse
   *
   * Quy trình:
   * 1. Tạo XMLHttpRequest để gửi HTTP GET request
   * 2. Server trả về nội dung XML dạng text
   * 3. DOMParser chuyển text thành XML Document (DOM)
   * 4. Trả về xmlDoc để JavaScript có thể truy cập như HTML DOM
   */
  async loadXML(filename) {
    return new Promise((resolve, reject) => {
      try {
        // Bước 1: Tạo đối tượng XMLHttpRequest
        const xhr = new XMLHttpRequest();

        // Ghép đường dẫn đầy đủ đến file XML
        const fullPath = this.dataPath + filename;

        // Bước 2: Xử lý khi request hoàn thành
        xhr.onload = function () {
          // Kiểm tra status:
          // - 200: HTTP OK (khi chạy qua server)
          // - 0: file:// protocol (khi mở file trực tiếp trong trình duyệt)
          if (xhr.status === 200 || xhr.status === 0) {
            // Bước 3: Parse XML text thành DOM Document
            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(xhr.responseText, "text/xml");

            // Kiểm tra lỗi parse XML (cú pháp XML sai)
            const parseError = xmlDoc.getElementsByTagName("parsererror");
            if (parseError.length > 0) {
              console.error("XML Parse Error:", parseError[0].textContent);
              reject(new Error("Lỗi parse XML: " + filename));
            } else {
              // Thành công - log và trả về XML Document
              console.log("✓ Loaded XML:", filename);
              resolve(xmlDoc);
            }
          } else {
            // Lỗi HTTP (404, 500, etc.)
            reject(new Error(`HTTP Error ${xhr.status}: ${filename}`));
          }
        };

        // Xử lý lỗi network (không kết nối được)
        xhr.onerror = function () {
          console.error("XHR Error loading:", fullPath);
          reject(new Error("Không thể tải file: " + filename));
        };

        // Bước 4: Gửi GET request
        // open(method, url, async) - async=true để không block UI
        xhr.open("GET", fullPath, true);
        xhr.send();
      } catch (error) {
        console.error("Error in loadXML:", error);
        reject(error);
      }
    });
  }

  /**
   * getElementText - Lấy nội dung text của một element trong XML
   * @param {Element} element - Element cha
   * @param {string} tagName - Tên tag cần lấy
   * @param {string} defaultValue - Giá trị mặc định nếu không tìm thấy
   * @returns {string} - Nội dung text của element
   *
   * Ví dụ: <customer><name>Nguyễn Văn A</name></customer>
   * getElementText(customer, "name") → "Nguyễn Văn A"
   */
  getElementText(element, tagName, defaultValue = "") {
    const el = element.getElementsByTagName(tagName)[0];
    return el ? el.textContent : defaultValue;
  }

  /**
   * xmlToArray - Chuyển các element XML thành mảng JavaScript
   * @param {XMLDocument} xmlDoc - Document XML đã parse
   * @param {string} tagName - Tên tag cần lấy
   * @returns {Array} - Mảng các element
   */
  xmlToArray(xmlDoc, tagName) {
    const elements = xmlDoc.getElementsByTagName(tagName);
    return Array.from(elements);
  }
}

// ============================================================================
// CLASS AuthManager - Quản lý đăng nhập/xác thực người dùng
// ============================================================================
class AuthManager {
  /**
   * Constructor - Khởi tạo và load user hiện tại từ sessionStorage
   */
  constructor() {
    this.currentUser = null;
    this.loadCurrentUser();
  }

  /**
   * loadCurrentUser - Load thông tin user từ sessionStorage
   * sessionStorage: lưu trữ tạm trong phiên làm việc (đóng tab là mất)
   */
  loadCurrentUser() {
    const userStr = sessionStorage.getItem("currentUser");
    if (userStr) {
      this.currentUser = JSON.parse(userStr);
    }
  }

  /**
   * isAuthenticated - Kiểm tra user đã đăng nhập chưa
   * @returns {boolean}
   */
  isAuthenticated() {
    return this.currentUser !== null;
  }

  /**
   * requireAuth - Yêu cầu xác thực
   * Hiện tại: luôn return true (không yêu cầu đăng nhập)
   */
  requireAuth() {
    return true;
  }
}

// ============================================================================
// KHỞI TẠO CÁC INSTANCE GLOBAL
// ============================================================================
const xmlManager = new XMLManager(); // Instance để đọc XML
const authManager = new AuthManager(); // Instance quản lý auth

// ============================================================================
// HÀM requireAuth - Kiểm tra xác thực trên các trang bảo vệ
// Hiện tại: không yêu cầu đăng nhập, chỉ hiển thị tên mặc định
// ============================================================================

// ============================================================================
// HÀM handleLogout - Xử lý đăng xuất
// ============================================================================

// ============================================================================
// CÁC HÀM TIỆN ÍCH (UTILITY FUNCTIONS)
// ============================================================================

/**
 * formatCurrency - Format số thành tiền VNĐ
 * @param {number} amount - Số tiền
 * @returns {string} - Chuỗi đã format
 *
 * Ví dụ: formatCurrency(1000000) → "1.000.000 ₫"
 */
function formatCurrency(amount) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(amount);
}

/**
 * formatDate - Format ngày tháng theo định dạng Việt Nam
 * @param {string} dateString - Chuỗi ngày (vd: "2024-01-15")
 * @returns {string} - Ngày đã format (vd: "15/01/2024")
 */
function formatDate(dateString) {
  if (!dateString) return "";
  const date = new Date(dateString);
  return date.toLocaleDateString("vi-VN");
}

/**
 * formatDateTime - Format ngày giờ đầy đủ
 * @param {string} dateString - Chuỗi ngày giờ
 * @returns {string} - Ngày giờ đã format (vd: "15/01/2024 14:30:00")
 */
function formatDateTime(dateString) {
  if (!dateString) return "";
  const date = new Date(dateString);
  return date.toLocaleString("vi-VN");
}

/**
 * showLoading - Hiển thị spinner loading overlay
 * Dùng khi đang xử lý tác vụ nặng
 */
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

/**
 * hideLoading - Ẩn spinner loading
 */
function hideLoading() {
  const loading = document.getElementById("loading-overlay");
  if (loading) {
    loading.remove();
  }
}

// ============================================================================
// EXPORT RA WINDOW (GLOBAL SCOPE)
// Cho phép các file khác sử dụng các class và hàm này
// Ví dụ: xmlManager.loadXML("Customers.xml")
// ============================================================================
window.XMLManager = XMLManager;
window.AuthManager = AuthManager;
window.xmlManager = xmlManager;
window.authManager = authManager;
window.formatCurrency = formatCurrency;
window.formatDate = formatDate;
window.formatDateTime = formatDateTime;

window.showLoading = showLoading;
window.hideLoading = hideLoading;
