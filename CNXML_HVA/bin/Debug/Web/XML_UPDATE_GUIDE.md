# HƯỚNG DẪN KIỂM TRA VÀ SỬ DỤNG HỆ THỐNG

## 🔧 CÁC THAY ĐỔI ĐÃ THỰC HIỆN

### 1. Cập nhật CRUDManager (js/crud.js)

✅ Thêm hàm `parseXMLElement()` để parse XML elements có cấu trúc nested (address, contact, dimensions, etc.)
✅ Cập nhật `getMergedData()` để xử lý đúng root tags của từng file XML

### 2. Cập nhật Tên File XML

- `ChiNhanh.xml` → **`Branches.xml`** (tag: `branch`)
- `San.xml` → **`Fields.xml`** (tag: `field`)
- `LoaiSan.xml` → **`FieldTypes.xml`** (tag: `field_type`)
- `DatLich.xml` → **`Bookings.xml`** (tag: `Booking`)
- `DungCu.xml` → **`Equipments.xml`** (tag: `equipment`)
- `KhachHang.xml` → **`Customers.xml`** (tag: `customer`)

### 3. Mapping Fields từ XML

#### Branches (Chi nhánh)

```javascript
XML Fields → Display Fields
- name → Tên chi nhánh
- code → Mã chi nhánh
- address.city → Thành phố
- address.district → Quận/Huyện
- address.street → Đường
- address.housenumber → Số nhà
- contact.phone → Số điện thoại
- contact.email → Email
- managername → Tên quản lý
- totalfields → Số sân
- staffcount → Số nhân viên
- monthlyrevenue → Doanh thu tháng
- status → Trạng thái (Active/Inactive)
- description → Mô tả
- imageurl → Hình ảnh
```

#### Fields (Sân bóng)

```javascript
XML Fields:
- name → Tên sân
- fieldtypeid → Mã loại sân
- branchid → Mã chi nhánh
- address.city, address.district, address.street, address.housenumber
- priceperhour → Giá/giờ
- capacity → Sức chứa
- description → Mô tả
- facilities → Tiện nghi
- status → Trạng thái (Available/Maintenance/Booked)
- createddate → Ngày tạo
- lastmaintenance → Bảo trì lần cuối
```

#### FieldTypes (Loại sân)

```javascript
XML Fields:
- name → Tên loại sân
- code → Mã loại
- dimensions.length, dimensions.width → Kích thước
- playersperteam → Số người/đội
- totalcapacity → Tổng sức chứa
- baseprice → Giá cơ bản
- description → Mô tả
- features → Tính năng
- status → Trạng thái
```

#### Bookings (Đặt sân)

```javascript
XML Fields:
- customer → Tên khách hàng
- field → Tên sân
- type → Loại sân
- date → Ngày đặt
- time → Giờ đặt
- duration → Thời lượng
- note → Ghi chú
```

#### Equipments (Thiết bị)

```javascript
XML Fields:
- name → Tên thiết bị
- category → Danh mục
- brand → Thương hiệu
- model → Model
- quantitytotal → Tổng số lượng
- quantityavailable → Số lượng có sẵn
- rentalprice → Giá thuê
- purchaseprice → Giá mua
- condition → Tình trạng
- description → Mô tả
- branchid → Chi nhánh
- status → Trạng thái
- imageurl → Hình ảnh
```

#### Customers (Khách hàng)

```javascript
XML Fields:
- name → Tên khách hàng
- phone → Số điện thoại
- email → Email
- address.city, address.district, address.street → Địa chỉ
- membership → Loại thành viên (VIP/Gold/Silver/Regular)
- notes → Ghi chú
```

## 🧪 KIỂM TRA HỆ THỐNG

### Bước 1: Test XML Loading

1. Mở file `test-xml.html` trong trình duyệt
2. Click nút "Test All XML Files"
3. Kiểm tra xem tất cả 6 files có được load thành công không
4. Xem dữ liệu được parse có đúng cấu trúc không

### Bước 2: Test từng trang

1. **Dashboard** (`dashboard.html`) - Trang tổng quan

   - Kiểm tra charts có hiển thị không
   - Kiểm tra statistics cards

2. **Chi nhánh** (`branches.html`)

   - Xem danh sách chi nhánh hiển thị đúng không
   - Test tìm kiếm, lọc theo thành phố
   - Test CRUD: Thêm, Sửa, Xóa chi nhánh

3. **Sân bóng** (`fields.html`)

   - Kiểm tra danh sách sân
   - Test filter theo chi nhánh, trạng thái
   - Test CRUD operations

4. **Loại sân** (`field-types.html`)

   - Kiểm tra danh sách loại sân
   - Test search và CRUD

5. **Đặt sân** (`bookings.html`)

   - Kiểm tra danh sách booking (dạng table)
   - Test filter theo trạng thái
   - Test CRUD

6. **Thiết bị** (`equipments.html`)

   - Kiểm tra danh sách thiết bị
   - Test filter theo trạng thái
   - Test CRUD

7. **Khách hàng** (`customers.html`)
   - Kiểm tra danh sách khách hàng
   - Phân biệt VIP và Regular customers
   - Test CRUD

## ⚠️ LƯU Ý

### 1. CORS Issues

Nếu gặp lỗi CORS khi load XML:

```
Solution 1: Sử dụng Live Server extension trong VS Code
Solution 2: Chạy local server:
  - Python: python -m http.server 8000
  - Node.js: npx serve
  - PHP: php -S localhost:8000
```

### 2. Path Issues

- XML files phải nằm trong `../Data/` relative to Web folder
- Cấu trúc: `Debug/Web/` và `Debug/Data/` cùng cấp

### 3. LocalStorage

- CRUD operations lưu vào localStorage
- Data sẽ mất khi clear browser data
- XML files chỉ đọc, không ghi lại

## 🔄 CẬP NHẬT THÊM CẦN THIẾT

### branches.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: Branches.xml với tag `branch`
- ✅ Parse nested objects: address, contact
- ⚠️ CẦN: Cập nhật filterBranches() - line ~377
- ⚠️ CẦN: Cập nhật editBranch() và showAddBranchForm() forms

### fields.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: Fields.xml với tag `field`
- ⚠️ CẦN: Cập nhật renderFields() để dùng đúng fields
- ⚠️ CẦN: Cập nhật forms và filters

### field-types.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: FieldTypes.xml với tag `field_type`
- ⚠️ CẦN: Cập nhật render và forms

### bookings.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: Bookings.xml với tag `Booking`
- ⚠️ CẦN: Cập nhật renderBookings() table rows
- ⚠️ CẦN: Cập nhật forms

### equipments.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: Equipments.xml với tag `equipment`
- ⚠️ CẦN: Cập nhật render và forms

### customers.html - CẦN CẬP NHẬT

- ✅ Load XML đúng: Customers.xml với tag `customer`
- ⚠️ CẦN: Cập nhật render (membership field)
- ⚠️ CẦN: Cập nhật forms

## 📝 NEXT STEPS

1. **Test `test-xml.html`** để confirm XML được load
2. **Open Console** (F12) để xem logs khi load mỗi trang
3. **Kiểm tra** field mappings trong render functions
4. **Cập nhật** các forms để match với XML structure
5. **Test CRUD** operations với localStorage

## 🚀 DEPLOY

Khi deploy lên production:

1. Cần backend API để xử lý XML writes
2. Implement authentication system
3. Add data validation
4. Error handling và logging
5. Backup và restore mechanisms

## ❓ TROUBLESHOOTING

### Không thấy dữ liệu?

- Check Console (F12) for errors
- Verify XML path: `../Data/filename.xml`
- Check CORS settings
- Run từ local server, không mở file trực tiếp

### Dữ liệu hiển thị sai?

- Check field mapping trong render functions
- Verify XML structure match code expectations
- Check nested object access (address.city vs diachi)

### CRUD không hoạt động?

- Check localStorage trong DevTools
- Verify form data being captured correctly
- Check saveToXML() implementation
