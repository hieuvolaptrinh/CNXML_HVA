IF EXISTS (SELECT * FROM sys.databases WHERE name = N'dbSANBONG')
BEGIN
    USE master;
    ALTER DATABASE dbSANBONG SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE dbSANBONG;
END
GO

CREATE DATABASE dbSANBONG;
GO
USE dbSANBONG;
GO



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
    status VARCHAR(20)
);

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

CREATE TABLE Customers (
    id VARCHAR(10) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100),
    address NVARCHAR(255),
    registration_date DATE,
    status VARCHAR(20)
);

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
    CONSTRAINT FK_Equipments_Branches FOREIGN KEY (branch_id) REFERENCES Branches(id)
);

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
    CONSTRAINT FK_Matches_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id)
);

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

CREATE TABLE Bookings (
    id VARCHAR(10) PRIMARY KEY,
    customer_id VARCHAR(10) NOT NULL,  
    field_id VARCHAR(10) NOT NULL,
    type NVARCHAR(50),
    booking_date DATE,
    booking_time INT,  
    duration INT,     
    note NVARCHAR(255),
    CONSTRAINT FK_Bookings_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id),
    CONSTRAINT FK_Bookings_Fields FOREIGN KEY (field_id) REFERENCES Fields(id)
);


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
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (customer_id) REFERENCES Customers(id),
    CONSTRAINT FK_Orders_Branches FOREIGN KEY (branch_id) REFERENCES Branches(id),
    CONSTRAINT FK_Orders_Staff FOREIGN KEY (staff_id) REFERENCES Users(id)
);
