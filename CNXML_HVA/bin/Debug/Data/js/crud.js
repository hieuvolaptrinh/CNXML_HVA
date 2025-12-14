// CRUD Operations Manager for XML Files
class CRUDManager {
  constructor() {
    this.xmlManager = new XMLManager();
  }

  // Save data to XML (simulated - stores in localStorage and can export to XML)
  async saveToXML(filename, data, operation = "create") {
    console.log(`${operation.toUpperCase()} operation:`, filename, data);

    // Determine the storage key based on the actual filename
    const storageKey = `xml_${filename}`;
    let existingData = JSON.parse(localStorage.getItem(storageKey) || "[]");

    if (operation === "create") {
      // Generate ID if not exists
      if (!data.id) {
        data.id = this.generateNewId(existingData);
      }
      existingData.push(data);
      console.log(`✅ Created new item with ID: ${data.id}`);
    } else if (operation === "update") {
      const index = existingData.findIndex((item) => item.id === data.id);
      if (index !== -1) {
        existingData[index] = { ...existingData[index], ...data };
        console.log(`✅ Updated item with ID: ${data.id}`);
      } else {
        // If not found in localStorage, add it as new
        existingData.push(data);
        console.log(`✅ Added item to localStorage: ${data.id}`);
      }
    } else if (operation === "delete") {
      const initialLength = existingData.length;
      existingData = existingData.filter((item) => item.id !== data.id);
      if (existingData.length < initialLength) {
        console.log(`✅ Deleted item with ID: ${data.id}`);
      } else {
        console.log(`⚠️ Item with ID ${data.id} not found in localStorage`);
      }
    }

    localStorage.setItem(storageKey, JSON.stringify(existingData));
    return { success: true, message: "Operation completed successfully" };
  }

  // Generate a new unique ID
  generateNewId(existingData) {
    const maxId = existingData.reduce((max, item) => {
      const idNum = parseInt(item.id?.replace(/\D/g, "") || "0");
      return idNum > max ? idNum : max;
    }, 0);
    return `NEW${String(maxId + 1).padStart(3, "0")}`;
  }

  // Export localStorage data to XML string
  exportToXML(filename, rootElement, itemElement) {
    const storageKey = `xml_${filename}`;
    const data = JSON.parse(localStorage.getItem(storageKey) || "[]");

    let xml = `<?xml version="1.0" encoding="UTF-8"?>\n<${rootElement}>\n`;

    data.forEach((item) => {
      xml += `  <${itemElement} id="${item.id}">\n`;
      Object.entries(item).forEach(([key, value]) => {
        if (
          key !== "id" &&
          key !== "_source" &&
          value !== undefined &&
          value !== null
        ) {
          xml += `    <${key}>${this.escapeXML(String(value))}</${key}>\n`;
        }
      });
      xml += `  </${itemElement}>\n`;
    });

    xml += `</${rootElement}>`;
    return xml;
  }

  // Escape special XML characters
  escapeXML(str) {
    return str
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&apos;");
  }

  // Download XML file
  downloadXML(filename, rootElement, itemElement) {
    const xml = this.exportToXML(filename, rootElement, itemElement);
    const blob = new Blob([xml], { type: "application/xml" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
  }

  // Clear localStorage for a specific file
  clearLocalStorage(filename) {
    const storageKey = `xml_${filename}`;
    localStorage.removeItem(storageKey);
    console.log(`🗑️ Cleared localStorage for ${filename}`);
  }

  // Parse XML element recursively
  parseXMLElement(element) {
    const obj = {};

    // Get attributes (như id="B01")
    if (element.attributes) {
      for (let attr of element.attributes) {
        obj[attr.name] = attr.value;
      }
    }

    // Parse children
    for (let child of element.children) {
      const tagName = child.tagName.toLowerCase();

      // Check if child has children (nested object như address, contact)
      if (child.children.length > 0 && child.textContent.trim().length === 0) {
        // Nếu là nested object không có text content
        obj[tagName] = this.parseXMLElement(child);
      } else if (child.children.length > 0) {
        // Nếu có children nhưng cũng có text content
        const nestedObj = this.parseXMLElement(child);
        if (Object.keys(nestedObj).length === 0) {
          // Nếu nested object rỗng, lấy text content
          obj[tagName] = child.textContent.trim();
        } else {
          obj[tagName] = nestedObj;
        }
      } else {
        // Simple text content
        obj[tagName] = child.textContent.trim();
      }
    }

    return obj;
  }

  // Get merged data (XML + localStorage)
  async getMergedData(filename, rootTag) {
    try {
      console.log(`📂 Loading ${filename} with tag "${rootTag}"...`);
      const xmlDoc = await this.xmlManager.loadXML(filename);

      // Handle different root tag formats - thử cả chữ hoa và chữ thường
      let xmlElements = xmlDoc.getElementsByTagName(rootTag);

      // Nếu không tìm thấy, thử với chữ hoa đầu
      if (xmlElements.length === 0) {
        const capitalizedTag =
          rootTag.charAt(0).toUpperCase() + rootTag.slice(1);
        xmlElements = xmlDoc.getElementsByTagName(capitalizedTag);
        console.log(
          `  → Thử tag "${capitalizedTag}": ${xmlElements.length} items`
        );
      }

      // Nếu vẫn không tìm thấy, thử các biến thể khác
      if (xmlElements.length === 0) {
        const alternativeTags = {
          booking: "Booking",
          field: "field",
          customer: "customer",
          branch: "branch",
          equipment: "equipment",
          fieldType: "field_type",
          field_type: "field_type",
        };

        const altTag = alternativeTags[rootTag];
        if (altTag) {
          xmlElements = xmlDoc.getElementsByTagName(altTag);
          console.log(
            `  → Thử alternative tag "${altTag}": ${xmlElements.length} items`
          );
        }
      }

      console.log(`  ✓ Found ${xmlElements.length} ${rootTag} elements`);

      const storageKey = `xml_${filename}`;
      const localData = JSON.parse(localStorage.getItem(storageKey) || "[]");

      // Convert XML elements to objects with proper parsing
      const xmlObjects = Array.from(xmlElements).map((element) => {
        const obj = this.parseXMLElement(element);
        obj._source = "xml";

        // Chuẩn hóa field names cho Customer
        if (rootTag === "customer") {
          obj.tenkhachhang = obj.name || obj.tenkhachhang;
          obj.makhachhang = obj.id || obj.makhachhang;
          obj.sdt = obj.phone || obj.sdt;
          obj.loaikhachhang = obj.membership || obj.loaikhachhang;
          // Parse address nếu là object
          if (obj.address && typeof obj.address === "object") {
            obj.diachi = `${obj.address.street || ""}, ${
              obj.address.district || ""
            }, ${obj.address.city || ""}`.trim();
          } else {
            obj.diachi = obj.address || obj.diachi;
          }
        }

        // Chuẩn hóa field names cho Booking
        if (rootTag === "booking" || rootTag === "Booking") {
          obj.madatlich = obj.id || obj.madatlich;
          obj.makhachhang = obj.customer || obj.makhachhang;
          obj.masan = obj.field || obj.masan;
          obj.loaisan = obj.type || obj.loaisan;
          obj.ngaydat = obj.date || obj.ngaydat;
          obj.giobatdau = obj.time ? obj.time + ":00" : obj.giobatdau;
          obj.gioketthuc =
            obj.time && obj.duration
              ? parseInt(obj.time) + parseInt(obj.duration || 1) + ":00"
              : obj.gioketthuc;
          obj.thoigian = obj.duration || obj.thoigian;
          obj.ghichu = obj.note || obj.ghichu;
          obj.tongtien = obj.price || obj.tongtien || 0;
          obj.trangthai = obj.status || obj.trangthai || "Confirmed";
        }

        // Chuẩn hóa field names cho Branch - giữ nguyên image_url từ XML
        if (rootTag === "branch") {
          // Không cần mapping vì XML đã dùng image_url
        }

        // Chuẩn hóa field names cho Equipment - giữ nguyên image_url từ XML
        if (rootTag === "equipment") {
          // Không cần mapping vì XML đã dùng image_url
        }

        // Chuẩn hóa field names cho FieldType
        if (rootTag === "field_type" || rootTag === "fieldType") {
          obj.maloaisan = obj.id || obj.code || obj.maloaisan;
          obj.tenloaisan = obj.name || obj.tenloaisan;
          obj.songuoi =
            obj.total_capacity || obj.players_per_team || obj.songuoi;
          obj.kichthuoc = obj.size_display || obj.kichthuoc;
          obj.mota = obj.description || obj.mota;
          obj.trangthai = obj.status || obj.trangthai;
        }

        // Chuẩn hóa field names cho Field
        if (rootTag === "field") {
          obj.masan = obj.id || obj.masan;
          obj.tensan = obj.name || obj.tensan;
          obj.maloaisan = obj.field_type_id || obj.maloaisan;
          obj.machinhanh = obj.branch_id || obj.machinhanh;
          obj.gia = obj.price_per_hour || obj.gia;
          obj.mota = obj.description || obj.mota;
          obj.trangthai = obj.status || obj.trangthai;
          obj.hinhanh = obj.image_url || obj.hinhanh;
        }

        return obj;
      });

      // Merge with localStorage data
      const merged = [
        ...xmlObjects,
        ...localData.map((item) => ({ ...item, _source: "local" })),
      ];

      console.log(`✓ Loaded ${merged.length} items from ${filename}`);
      if (merged.length > 0) {
        console.log(`  Sample item:`, merged[0]);
      }
      return merged;
    } catch (error) {
      console.error("Error loading data:", error);
      return [];
    }
  }
}

// Image Modal Handler
class ImageModal {
  constructor() {
    this.modal = null;
    this.initialized = false;
    this.init();
  }

  init() {
    // Wait for DOM to be ready
    if (document.readyState === "loading") {
      document.addEventListener("DOMContentLoaded", () => this._initModal());
    } else {
      this._initModal();
    }
  }

  _initModal() {
    if (this.initialized) return;

    // Create modal HTML
    const modalHTML = `
      <div id="imageModal" class="modal">
        <div class="modal-content" style="max-width: 800px;">
          <div class="modal-header">
            <h3 id="imageModalTitle">Image Preview</h3>
            <button class="btn btn-icon" onclick="imageModal.close()">
              <i class="fas fa-times"></i>
            </button>
          </div>
          <div class="modal-body text-center">
            <img id="modalImage" src="" alt="Preview" style="max-width: 100%; border-radius: 8px;">
          </div>
        </div>
      </div>
    `;

    if (!document.getElementById("imageModal") && document.body) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("imageModal");

      // Close on outside click
      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
      this.initialized = true;
    }
  }

  show(imageUrl, title = "Image Preview") {
    // Ensure modal is initialized
    if (!this.initialized) {
      this._initModal();
    }

    const modalElement = document.getElementById("imageModal");
    if (!modalElement) {
      console.error("ImageModal element not found!");
      return;
    }

    document.getElementById("modalImage").src = imageUrl;
    document.getElementById("imageModalTitle").textContent = title;
    modalElement.classList.add("active");
    document.body.style.overflow = "hidden";
  }

  close() {
    const modalElement = document.getElementById("imageModal");
    if (modalElement) {
      modalElement.classList.remove("active");
      document.body.style.overflow = "";
    }
  }
}

// Detail Modal Handler
class DetailModal {
  constructor() {
    this.modal = null;
    this.initialized = false;
    this.init();
  }

  init() {
    // Wait for DOM to be ready
    if (document.readyState === "loading") {
      document.addEventListener("DOMContentLoaded", () => this._initModal());
    } else {
      this._initModal();
    }
  }

  _initModal() {
    if (this.initialized) return;

    const modalHTML = `
      <div id="detailModal" class="modal">
        <div class="modal-content" style="max-width: 700px;">
          <div class="modal-header">
            <h3 id="detailModalTitle">Details</h3>
            <button class="btn btn-icon" onclick="detailModal.close()">
              <i class="fas fa-times"></i>
            </button>
          </div>
          <div class="modal-body" id="detailModalBody">
            <!-- Content will be inserted here -->
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" onclick="detailModal.close()">
              <i class="fas fa-times"></i> Close
            </button>
          </div>
        </div>
      </div>
    `;

    if (!document.getElementById("detailModal") && document.body) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("detailModal");

      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
      this.initialized = true;
    }
  }

  show(title, content) {
    // Ensure modal is initialized
    if (!this.initialized) {
      this._initModal();
    }

    const modalElement = document.getElementById("detailModal");
    if (!modalElement) {
      console.error("DetailModal element not found!");
      return;
    }

    document.getElementById("detailModalTitle").textContent = title;
    document.getElementById("detailModalBody").innerHTML = content;
    modalElement.classList.add("active");
    document.body.style.overflow = "hidden";
  }

  close() {
    document.getElementById("detailModal").classList.remove("active");
    document.body.style.overflow = "";
  }
}

// Form Modal Handler
class FormModal {
  constructor() {
    this.modal = null;
    this.onSubmitCallback = null;
    this.initialized = false;
    this.init();
  }

  init() {
    // Wait for DOM to be ready
    if (document.readyState === "loading") {
      document.addEventListener("DOMContentLoaded", () => this._initModal());
    } else {
      this._initModal();
    }
  }

  _initModal() {
    if (this.initialized) return;

    const modalHTML = `
      <div id="formModal" class="modal">
        <div class="modal-content">
          <div class="modal-header">
            <h3 id="formModalTitle">Form</h3>
            <button class="btn btn-icon" onclick="formModal.close()">
              <i class="fas fa-times"></i>
            </button>
          </div>
          <div class="modal-body" id="formModalBody">
            <!-- Form will be inserted here -->
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" onclick="formModal.close()">
              <i class="fas fa-times"></i> Cancel
            </button>
            <button class="btn btn-primary" onclick="formModal.submit()">
              <i class="fas fa-save"></i> Save
            </button>
          </div>
        </div>
      </div>
    `;

    if (!document.getElementById("formModal") && document.body) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("formModal");

      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
      this.initialized = true;
    }
  }

  show(title, formHTML, onSubmit) {
    // Ensure modal is initialized
    if (!this.initialized) {
      this._initModal();
    }

    const modalElement = document.getElementById("formModal");
    if (!modalElement) {
      console.error("FormModal element not found!");
      return;
    }

    document.getElementById("formModalTitle").textContent = title;
    document.getElementById("formModalBody").innerHTML = formHTML;
    this.onSubmitCallback = onSubmit;
    modalElement.classList.add("active");
    document.body.style.overflow = "hidden";
  }

  submit() {
    if (this.onSubmitCallback) {
      const formData = new FormData(
        document.querySelector("#formModalBody form")
      );
      const data = Object.fromEntries(formData.entries());
      this.onSubmitCallback(data);
    }
  }

  close() {
    document.getElementById("formModal").classList.remove("active");
    document.body.style.overflow = "";
    this.onSubmitCallback = null;
  }
}

// Toast Notification System
class Toast {
  static show(message, type = "success", duration = 3000) {
    const toast = document.createElement("div");
    toast.className = `toast toast-${type}`;
    toast.innerHTML = `
      <div class="toast-content">
        <i class="fas fa-${
          type === "success"
            ? "check-circle"
            : type === "error"
            ? "times-circle"
            : "info-circle"
        }"></i>
        <span>${message}</span>
      </div>
    `;

    // Add styles if not exists
    if (!document.getElementById("toast-styles")) {
      const style = document.createElement("style");
      style.id = "toast-styles";
      style.textContent = `
        .toast {
          position: fixed;
          bottom: 20px;
          right: 20px;
          background: white;
          padding: 16px 24px;
          border-radius: 8px;
          box-shadow: 0 4px 12px rgba(0,0,0,0.15);
          z-index: 10000;
          animation: slideInRight 0.3s ease-out, fadeOut 0.3s ease-out ${
            duration - 300
          }ms forwards;
          min-width: 300px;
        }
        .toast-content {
          display: flex;
          align-items: center;
          gap: 12px;
          font-size: 14px;
          font-weight: 500;
        }
        .toast-success { border-left: 4px solid #4caf50; }
        .toast-success i { color: #4caf50; }
        .toast-error { border-left: 4px solid #f44336; }
        .toast-error i { color: #f44336; }
        .toast-info { border-left: 4px solid #2196f3; }
        .toast-info i { color: #2196f3; }
        @keyframes fadeOut {
          to { opacity: 0; transform: translateX(100px); }
        }
      `;
      document.head.appendChild(style);
    }

    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), duration);
  }

  static success(message) {
    this.show(message, "success");
  }

  static error(message) {
    this.show(message, "error", 4000);
  }

  static info(message) {
    this.show(message, "info");
  }
}

// Confirmation Dialog
class ConfirmDialog {
  static show(message, onConfirm, title = "Confirm Action") {
    const dialog = document.createElement("div");
    dialog.className = "modal active";
    dialog.innerHTML = `
      <div class="modal-content" style="max-width: 400px;">
        <div class="modal-header" style="background: linear-gradient(135deg, #f44336 0%, #e53935 100%);">
          <h3>${title}</h3>
        </div>
        <div class="modal-body">
          <p style="font-size: 16px; margin: 20px 0;">${message}</p>
        </div>
        <div class="modal-footer">
          <button class="btn btn-secondary" onclick="this.closest('.modal').remove(); document.body.style.overflow = '';">
            <i class="fas fa-times"></i> Cancel
          </button>
          <button class="btn btn-danger" id="confirmBtn">
            <i class="fas fa-check"></i> Confirm
          </button>
        </div>
      </div>
    `;

    document.body.appendChild(dialog);
    document.body.style.overflow = "hidden";

    dialog.querySelector("#confirmBtn").addEventListener("click", () => {
      onConfirm();
      dialog.remove();
      document.body.style.overflow = "";
    });

    dialog.addEventListener("click", (e) => {
      if (e.target === dialog) {
        dialog.remove();
        document.body.style.overflow = "";
      }
    });
  }
}

// Loading Overlay
class LoadingOverlay {
  static show() {
    if (document.getElementById("loadingOverlay")) return;

    const overlay = document.createElement("div");
    overlay.id = "loadingOverlay";
    overlay.className = "loading-overlay";
    overlay.innerHTML = `
      <div style="text-align: center;">
        <div class="spinner"></div>
        <p style="margin-top: 16px; color: var(--primary-green); font-weight: 600;">Loading...</p>
      </div>
    `;
    document.body.appendChild(overlay);
  }

  static hide() {
    const overlay = document.getElementById("loadingOverlay");
    if (overlay) overlay.remove();
  }
}

// Initialize CRUDManager immediately (doesn't need DOM)
const crudManager = new CRUDManager();
window.crudManager = crudManager;

// Initialize modals - delay until DOM is ready
let imageModal, detailModal, formModal;

function initializeModals() {
  if (!imageModal) imageModal = new ImageModal();
  if (!detailModal) detailModal = new DetailModal();
  if (!formModal) formModal = new FormModal();

  // Export to window
  window.imageModal = imageModal;
  window.detailModal = detailModal;
  window.formModal = formModal;
}

// Initialize modals when DOM is ready
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", initializeModals);
} else {
  // DOM already ready, initialize immediately
  initializeModals();
}

// Also initialize on next tick to catch any edge cases
setTimeout(() => {
  if (!window.formModal || !window.detailModal || !window.imageModal) {
    initializeModals();
  }
}, 0);

// Add to existing app.js functions
// Extend formatCurrency function
function formatCurrency(amount) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(amount);
}

// Extend formatDate function
function formatDate(dateString) {
  if (!dateString) return "N/A";
  try {
    const date = new Date(dateString);
    if (isNaN(date.getTime())) return dateString; // Return original if invalid

    // Manual formatting to avoid Intl issues
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  } catch (error) {
    console.error("Error formatting date:", error);
    return dateString;
  }
}

// Format DateTime
function formatDateTime(dateString) {
  if (!dateString) return "N/A";
  try {
    const date = new Date(dateString);
    if (isNaN(date.getTime())) return dateString;

    // Manual formatting
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, "0");
    const minutes = String(date.getMinutes()).padStart(2, "0");
    return `${day}/${month}/${year} ${hours}:${minutes}`;
  } catch (error) {
    console.error("Error formatting datetime:", error);
    return dateString;
  }
}

// Generate unique ID
function generateId(prefix = "ID") {
  return `${prefix}${Date.now()}${Math.random().toString(36).substr(2, 9)}`;
}

// Debounce function for search
function debounce(func, wait) {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      clearTimeout(timeout);
      func(...args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  };
}

// Animate number counting
function animateValue(element, start, end, duration = 1000) {
  const range = end - start;
  const increment = range / (duration / 16);
  let current = start;

  const timer = setInterval(() => {
    current += increment;
    if (
      (increment > 0 && current >= end) ||
      (increment < 0 && current <= end)
    ) {
      current = end;
      clearInterval(timer);
    }
    element.textContent = Math.floor(current).toLocaleString("vi-VN");
  }, 16);
}

// Export all utilities
window.CRUDManager = CRUDManager;
window.Toast = Toast;
window.ConfirmDialog = ConfirmDialog;
window.LoadingOverlay = LoadingOverlay;
window.generateId = generateId;
window.debounce = debounce;
window.animateValue = animateValue;
