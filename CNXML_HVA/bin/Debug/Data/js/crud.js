// CRUD Operations Manager for XML Files
class CRUDManager {
  constructor() {
    this.xmlManager = new XMLManager();
  }

  // Save data to XML (simulated - in real app, needs backend)
  async saveToXML(filename, data, operation = "create") {
    console.log(`${operation.toUpperCase()} operation:`, filename, data);

    // In a real implementation, this would call a backend API
    // For demo purposes, we'll store in localStorage
    const storageKey = `xml_${filename}`;
    let existingData = JSON.parse(localStorage.getItem(storageKey) || "[]");

    if (operation === "create") {
      existingData.push(data);
    } else if (operation === "update") {
      const index = existingData.findIndex((item) => item.id === data.id);
      if (index !== -1) {
        existingData[index] = { ...existingData[index], ...data };
      }
    } else if (operation === "delete") {
      existingData = existingData.filter((item) => item.id !== data.id);
    }

    localStorage.setItem(storageKey, JSON.stringify(existingData));
    return { success: true, message: "Operation completed successfully" };
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
        if (rootTag === "field_type") {
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
    this.init();
  }

  init() {
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

    if (!document.getElementById("imageModal")) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("imageModal");

      // Close on outside click
      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
    }
  }

  show(imageUrl, title = "Image Preview") {
    document.getElementById("modalImage").src = imageUrl;
    document.getElementById("imageModalTitle").textContent = title;
    document.getElementById("imageModal").classList.add("active");
    document.body.style.overflow = "hidden";
  }

  close() {
    document.getElementById("imageModal").classList.remove("active");
    document.body.style.overflow = "";
  }
}

// Detail Modal Handler
class DetailModal {
  constructor() {
    this.modal = null;
    this.init();
  }

  init() {
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

    if (!document.getElementById("detailModal")) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("detailModal");

      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
    }
  }

  show(title, content) {
    document.getElementById("detailModalTitle").textContent = title;
    document.getElementById("detailModalBody").innerHTML = content;
    document.getElementById("detailModal").classList.add("active");
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
    this.init();
  }

  init() {
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

    if (!document.getElementById("formModal")) {
      document.body.insertAdjacentHTML("beforeend", modalHTML);
      this.modal = document.getElementById("formModal");

      this.modal.addEventListener("click", (e) => {
        if (e.target === this.modal) {
          this.close();
        }
      });
    }
  }

  show(title, formHTML, onSubmit) {
    document.getElementById("formModalTitle").textContent = title;
    document.getElementById("formModalBody").innerHTML = formHTML;
    this.onSubmitCallback = onSubmit;
    document.getElementById("formModal").classList.add("active");
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

// Initialize global instances
const crudManager = new CRUDManager();
const imageModal = new ImageModal();
const detailModal = new DetailModal();
const formModal = new FormModal();

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
  const date = new Date(dateString);
  return new Intl.DateFormat("vi-VN").format(date);
}

// Format DateTime
function formatDateTime(dateString) {
  if (!dateString) return "N/A";
  const date = new Date(dateString);
  return new Intl.DateFormat("vi-VN", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  }).format(date);
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
window.crudManager = crudManager;
window.imageModal = imageModal;
window.detailModal = detailModal;
window.formModal = formModal;
window.Toast = Toast;
window.ConfirmDialog = ConfirmDialog;
window.LoadingOverlay = LoadingOverlay;
window.generateId = generateId;
window.debounce = debounce;
window.animateValue = animateValue;
