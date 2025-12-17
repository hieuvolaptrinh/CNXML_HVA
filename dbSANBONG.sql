USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = N'dbSANBONG')
BEGIN
    ALTER DATABASE dbSANBONG SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE dbSANBONG;
END
GO

CREATE DATABASE dbSANBONG;
GO
USE dbSANBONG;
GO

/* =============================================
   PHẦN 1: TẠO CÁC BẢNG DANH MỤC
   ============================================= */

-- 1. Bảng Chi nhánh
CREATE TABLE Branches (
    id VARCHAR(10) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    code VARCHAR(20),
    city NVARCHAR(50),
    district NVARCHAR(50),
    street NVARCHAR(100),
    house_number NVARCHAR(20),
    postal_code VARCHAR(10),
    phone VARCHAR(20),
    email VARCHAR(100),
    fax VARCHAR(20),
    manager_id VARCHAR(10),
    manager_name NVARCHAR(100),
    weekday_hours VARCHAR(20),
    weekend_hours VARCHAR(20),
    total_fields INT,
    established_date DATE,
    monthly_revenue BIGINT,
    staff_count INT,
    description NVARCHAR(255),
    status VARCHAR(20),
    image_url NVARCHAR(500)
);

-- 2. Bảng Khách Hàng (ĐÃ CHUẨN HÓA TIẾNG ANH)
-- Phù hợp với cấu trúc phân cấp của XML
CREATE TABLE Customers (
    id VARCHAR(10) PRIMARY KEY,     -- XML: id
    name NVARCHAR(100),             -- XML: name
    phone VARCHAR(20),              -- XML: phone
    email VARCHAR(100),             -- XML: email
    city NVARCHAR(50),              -- XML: address/city
    district NVARCHAR(50),          -- XML: address/district
    street NVARCHAR(200),           -- XML: address/street
    membership NVARCHAR(20),        -- XML: membership
    notes NVARCHAR(MAX)             -- XML: notes
);

-- 3. Bảng Loại sân
CREATE TABLE FieldTypes (
    id VARCHAR(10) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    code VARCHAR(20),
    length DECIMAL(5,2),
    width DECIMAL(5,2),
    dimension_unit NVARCHAR(10),
    size_display NVARCHAR(20),
    players_per_team INT,
    total_capacity INT,
    goal_height DECIMAL(5,2),
    goal_width DECIMAL(5,2),
    goal_unit NVARCHAR(10),
    surface_type NVARCHAR(50),
    base_price BIGINT,
    peak_hour_multiplier DECIMAL(3,2),
    weekend_multiplier DECIMAL(3,2),
    description NVARCHAR(255),
    features NVARCHAR(255),
    minimum_booking_hours INT,
    maximum_booking_hours INT,
    status VARCHAR(20)
);

/* =============================================
   PHẦN 2: TẠO CÁC BẢNG CÓ KHÓA NGOẠI
   ============================================= */

-- 4. Bảng Users
CREATE TABLE Users (
    id VARCHAR(10) PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    full_name NVARCHAR(100) NOT NULL,
    email VARCHAR(100),
    phone VARCHAR(20),
    role VARCHAR(30),        
    branch_id VARCHAR(10) NULL,
    created_at DATETIME DEFAULT GETDATE(),
    last_login DATETIME,
    FOREIGN KEY (branch_id) REFERENCES Branches(id)
);

-- 5. Bảng Thiết bị
CREATE TABLE Equipments (
    id VARCHAR(10) PRIMARY KEY,
    name NVARCHAR(150) NOT NULL,
    category NVARCHAR(50),
    brand NVARCHAR(50),
    model NVARCHAR(50),
    quantity_total INT,
    quantity_available INT,
    rental_price BIGINT,
    purchase_price BIGINT,
    condition NVARCHAR(50),
    description NVARCHAR(255),
    branch_id VARCHAR(10),
    supplier NVARCHAR(100),
    purchase_date DATE,
    warranty_period INT, 
    status VARCHAR(20),
    image_url NVARCHAR(500),
    CONSTRAINT FK_Equipments_Branches FOREIGN KEY (branch_id) REFERENCES Branches(id)
);

-- 6. Bảng Sân bóng
CREATE TABLE Fields (
    id VARCHAR(10) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    field_type_id VARCHAR(10) NOT NULL, 
    branch_id VARCHAR(10) NOT NULL,     
    city NVARCHAR(50),
    district NVARCHAR(50),
    street NVARCHAR(100),
    house_number NVARCHAR(20),
    price_per_hour BIGINT,
    capacity INT,
    description NVARCHAR(255),
    facilities NVARCHAR(255),
    status VARCHAR(20),
    created_date DATE,
    last_maintenance DATE,
    CONSTRAINT FK_Fields_Branches FOREIGN KEY (branch_id) REFERENCES Branches(id),
    CONSTRAINT FK_Fields_FieldTypes FOREIGN KEY (field_type_id) REFERENCES FieldTypes(id)
);

/* =============================================
   PHẦN 3: TẠO CÁC BẢNG NGHIỆP VỤ
   ============================================= */

-- 7. Bảng Thiết bị theo Sân
CREATE TABLE FieldEquipment (
    id VARCHAR(10) PRIMARY KEY,
    field_id VARCHAR(10) NOT NULL,   
    equipment_id VARCHAR(10) NOT NULL,   
    quantity_allocated INT,
    storage_location NVARCHAR(100),
    last_maintenance DATE,
    condition NVARCHAR(50),
    notes NVARCHAR(255),
    status VARCHAR(20),
    CONSTRAINT FK_FieldEquipment_Fields FOREIGN KEY (field_id) REFERENCES Fields(id),
    CONSTRAINT FK_FieldEquipment_Equipments FOREIGN KEY (equipment_id) REFERENCES Equipments(id)
);

-- 8. Bảng Trận đấu
CREATE TABLE Matches (
    id VARCHAR(10) PRIMARY KEY,
    field_id VARCHAR(10) NOT NULL,      
    customer_id VARCHAR(10) NOT NULL,   
    match_date DATE,
    start_time TIME,
    end_time TIME,
    duration INT,           
    price BIGINT,
    status VARCHAR(20),
    notes NVARCHAR(255),
    CONSTRAINT FK_Matches_Fields FOREIGN KEY (field_id) REFERENCES Fields(id),
    -- Cập nhật FK trỏ về Customers(id)
    CONSTRAINT FK_Matches_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id)
);

-- 9. Bảng Thuê thiết bị trận đấu
CREATE TABLE EquipmentMatch (
    id VARCHAR(10) PRIMARY KEY,
    match_id VARCHAR(10) NOT NULL,      
    equipment_id VARCHAR(10) NOT NULL, 
    quantity_used INT,
    rental_price BIGINT,
    total_cost BIGINT,
    checkout_time DATETIME,
    checkin_time DATETIME,
    condition_checkout NVARCHAR(50),
    condition_checkin NVARCHAR(50),
    staff_checkout VARCHAR(10),        
    staff_checkin VARCHAR(10),        
    notes NVARCHAR(255),
    status VARCHAR(20),
    CONSTRAINT FK_EquipmentMatch_Matches FOREIGN KEY (match_id) REFERENCES Matches(id),
    CONSTRAINT FK_EquipmentMatch_Equipments FOREIGN KEY (equipment_id) REFERENCES Equipments(id),
    CONSTRAINT FK_EquipmentMatch_StaffCheckout FOREIGN KEY (staff_checkout) REFERENCES Users(id),
    CONSTRAINT FK_EquipmentMatch_StaffCheckin FOREIGN KEY (staff_checkin) REFERENCES Users(id)
);

-- 10. Bảng Đặt sân
CREATE TABLE Bookings (
    id VARCHAR(10) PRIMARY KEY,
    customer_id VARCHAR(10) NOT NULL,  
    field_id VARCHAR(10) NOT NULL,
    type NVARCHAR(50),
    booking_date DATE,
    booking_time INT,  
    duration INT,     
    note NVARCHAR(255),
    -- Cập nhật FK trỏ về Customers(id)
    CONSTRAINT FK_Bookings_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id),
    CONSTRAINT FK_Bookings_Fields FOREIGN KEY (field_id) REFERENCES Fields(id)
);

-- 11. Bảng Hóa đơn
CREATE TABLE Orders (
    id VARCHAR(10) PRIMARY KEY,
    customer_id VARCHAR(10) NOT NULL,  
    branch_id VARCHAR(10) NOT NULL,    
    order_date DATE,
    total_amount BIGINT,
    payment_method NVARCHAR(50),
    payment_status NVARCHAR(50),
    status VARCHAR(20),
    staff_id VARCHAR(10),               
    notes NVARCHAR(255),
    -- Cập nhật FK trỏ về Customers(id)
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id),
    CONSTRAINT FK_Orders_Branches FOREIGN KEY (branch_id) REFERENCES Branches(id),
    CONSTRAINT FK_Orders_Staff FOREIGN KEY (staff_id) REFERENCES Users(id)
);

-- DATA MẪU
-- Insert Branches first (required for Fields foreign key)
INSERT INTO Branches (id, name, code, city, district, street, house_number, phone, email, manager_name, weekday_hours, weekend_hours, total_fields, established_date, monthly_revenue, staff_count, description, status, image_url)
VALUES
('B01', N'Chi nhánh Hà Nội', 'HN001', N'Hà Nội', N'Cầu Giấy', N'Trần Thái Tông', N'123', '0241234567', 'hanoi@sanbong.vn', N'Nguyễn Văn Tùng', '6:00-22:00', '5:00-23:00', 5, '2023-01-15', 150000000, 12, N'Chi nhánh trung tâm Hà Nội', 'Active', 'https://images.unsplash.com/photo-1624880357913-a8539238245b?w=800'),

('B02', N'Chi nhánh TP.HCM', 'HCM001', N'TP Hồ Chí Minh', N'Bình Thạnh', N'Xô Viết Nghệ Tĩnh', N'789', '0281234567', 'hcm@sanbong.vn', N'Trần Minh Khoa', '6:00-22:00', '5:00-23:00', 8, '2023-03-20', 250000000, 18, N'Chi nhánh trung tâm TP.HCM', 'Active', 'https://images.unsplash.com/photo-1529900748604-07564a03e7a6?w=800'),

('B03', N'Chi nhánh Hải Phòng', 'HP001', N'Hải Phòng', N'Lê Chân', N'Điện Biên Phủ', N'567', '0251234567', 'haiphong@sanbong.vn', N'Phạm Đức Anh', '6:00-22:00', '5:00-23:00', 4, '2023-06-10', 80000000, 8, N'Chi nhánh Hải Phòng', 'Active', 'https://images.unsplash.com/photo-1551958219-acbc608c6377?w=800');

INSERT INTO Customers (id, name, phone, email, city, district, street, membership, notes)
VALUES
('C001', N'Nguyễn Văn A', '0912345678', 'a@gmail.com', N'Đà Nẵng', N'Hải Châu', N'Bạch Đằng', 'VIP', N'Test từ SQL'),

('C002', N'Trần Thị Thu Hà', '0988777666', 'thuha@gmail.com', N'Hà Nội', N'Cầu Giấy', N'Xuân Thủy', 'Gold', N'Khách hàng thân thiết'),

('C003', N'Lê Quang Huy', '0909555444', 'huy.le@outlook.com', N'TP. Hồ Chí Minh', N'Quận 1', N'Nguyễn Huệ', 'Silver', N'Thường đặt sân cuối tuần'),

('C004', N'Phạm Minh Tuấn', '0913222111', 'tuanpm@yahoo.com', N'Cần Thơ', N'Ninh Kiều', N'Hai Bà Trưng', 'VIP', N'Đội trưởng đội bóng công ty'),

('C005', N'Hoàng Thùy Linh', '0868999888', 'linh.hoang@company.vn', N'Đà Nẵng', N'Sơn Trà', N'Phạm Văn Đồng', 'None', N'Mới đăng ký hôm qua'),

('C006', N'Đặng Văn Lâm', '0977123123', 'lam.dang@gmail.com', N'Hải Phòng', N'Ngô Quyền', N'Lạch Tray', 'Gold', N'Yêu cầu xuất hóa đơn đỏ');

SELECT * FROM Customers;

-- Insert sample data for Equipments
INSERT INTO Equipments (id, name, category, brand, model, quantity_total, quantity_available, rental_price, purchase_price, condition, description, branch_id, supplier, purchase_date, warranty_period, status, image_url)
VALUES
('EQ001', N'Bóng đá FIFA Quality Pro', N'Bóng', 'Adidas', 'Tango España', 50, 45, 50000, 800000, N'Tốt', N'Bóng đá chất lượng cao FIFA Quality Pro', 'B01', N'Adidas Vietnam', '2023-01-10', 12, 'Active', 'https://images.unsplash.com/photo-1614632537197-38a17061c2bd?w=800'),

('EQ002', N'Áo đấu Manchester United', N'Trang phục', 'Nike', 'Home Kit 2023', 100, 85, 100000, 1200000, N'Tốt', N'Áo đấu chính thức Manchester United mùa giải 2023', 'B01', N'Nike Store', '2023-02-15', 6, 'Active', 'https://images.unsplash.com/photo-1522071820081-009f0129c71c?w=800'),

('EQ003', N'Giày đá bóng Predator', N'Giày', 'Adidas', 'Predator Edge', 30, 28, 150000, 2500000, N'Tốt', N'Giày đá bóng cao cấp Adidas Predator Edge', 'B02', N'Adidas Vietnam', '2023-03-20', 12, 'Active', 'https://images.unsplash.com/photo-1600185365926-3a2ce3cdb9eb?w=800'),

('EQ004', N'Bóng đá Training', N'Bóng', 'Nike', 'Strike Team', 80, 70, 30000, 500000, N'Tốt', N'Bóng đá tập luyện Nike Strike Team', 'B02', N'Nike Store', '2023-04-05', 12, 'Active', 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?w=800'),

('EQ005', N'Găng tay thủ môn', N'Phụ kiện', 'Puma', 'Ultra Grip', 20, 18, 80000, 1000000, N'Tốt', N'Găng tay thủ môn chuyên nghiệp Puma Ultra Grip', 'B03', N'Puma Vietnam', '2023-05-10', 6, 'Active', 'https://images.unsplash.com/photo-1551958219-acbc608c6377?w=800'),

('EQ006', N'Bình nước thể thao', N'Phụ kiện', 'Decathlon', 'Isotonic 750ml', 150, 140, 10000, 50000, N'Tốt', N'Bình nước thể thao 750ml', 'B01', N'Decathlon Vietnam', '2023-06-01', 3, 'Active', 'https://images.unsplash.com/photo-1523362628745-0c100150b504?w=800');

SELECT * FROM Branches;

-- Insert sample data for FieldTypes (from XML)
INSERT INTO FieldTypes (id, name, code, length, width, dimension_unit, size_display, players_per_team, total_capacity, goal_height, goal_width, goal_unit, surface_type, base_price, peak_hour_multiplier, weekend_multiplier, description, features, minimum_booking_hours, maximum_booking_hours, status)
VALUES
('FT01', N'Sân 5 người', 'S5sss', 43.00, 20.00, N'mét', '20m x 43m', 5, 10, 2.90, 3.00, N'mét', N'Cỏ tự nhiên', 100000, 1.00, 1.30, N'Sân bóng đá mini 5 người, phù hợp cho các trận đấu giải trí', N'Mái che, Đèn chiếu sáng, Vây lưới bảo vệ', 1, 4, 'Active'),

('FT02', N'Sân 7 người', 'S7', 60.00, 40.00, N'mét', '40m x 60m', 7, 14, 2.10, 5.00, N'mét', N'Cỏ nhân tạo', 150000, 1.50, 1.30, N'Sân bóng đá 7 người, thích hợp cho các giải đấu nghiệp dư', N'Đèn chiếu sáng chuyên nghiệp, Hệ thống tưới, Khán đài nhỏ', 1, 6, 'Active'),

('FT03', N'Sân 11 người', 'S11', 105.00, 68.00, N'mét', '68m x 105m', 11, 22, 2.44, 7.32, N'mét', N'Cỏ tự nhiên', 200000, 1.50, 1.30, N'Sân bóng đá tiêu chuẩn quốc tế 11 người theo chuẩn FIFA', N'Cỏ tự nhiên, Hệ thống tưới tự động, Khán đài, Phòng thay đồ, Căng tin', 2, 8, 'Active');

SELECT * FROM FieldTypes;
