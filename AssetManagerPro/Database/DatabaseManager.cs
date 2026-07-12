using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace AssetManagerPro.Database
{
    public static class DatabaseManager
    {
        private static readonly string DbPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database",
                "AssetManager.db");

        public static readonly string ConnectionString =
            $"Data Source={DbPath}";

        public static void Initialize()
        {
            string? folder = Path.GetDirectoryName(DbPath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder!);
            }

            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            EnableForeignKeys(connection);

            CreateTables(connection);

            SeedData(connection);
        }

        private static void EnableForeignKeys(SqliteConnection connection)
        {
            using var command = new SqliteCommand(
                "PRAGMA foreign_keys = ON;",
                connection);

            command.ExecuteNonQuery();
        }

        private static void CreateTables(SqliteConnection connection)
        {
            using var command = connection.CreateCommand();

            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Departments
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Description TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Users
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Username TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Role TEXT NOT NULL,
    IsActive INTEGER NOT NULL DEFAULT 1,
    LastLogin TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Brands
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Description TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Categories
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Description TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Statuses
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Color TEXT,
    Description TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Suppliers
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    ManagerName TEXT,
    Phone TEXT,
    Email TEXT,
    Address TEXT,
    Description TEXT
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Locations
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DepartmentId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    Description TEXT,

    FOREIGN KEY (DepartmentId)
        REFERENCES Departments(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Receivers
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    PersonnelCode TEXT NOT NULL UNIQUE,
    DepartmentId INTEGER NOT NULL,
    Phone TEXT,
    Email TEXT,
    IsActive INTEGER NOT NULL DEFAULT 1,

    FOREIGN KEY (DepartmentId)
        REFERENCES Departments(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Assets
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    AssetCode TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,

    BrandId INTEGER NOT NULL,
    CategoryId INTEGER NOT NULL,
    SupplierId INTEGER,

    Model TEXT,
    SerialNumber TEXT UNIQUE,

    LocationId INTEGER NOT NULL,
    ReceiverId INTEGER,

    StatusId INTEGER NOT NULL,

    Price REAL DEFAULT 0,

    PurchaseDate TEXT,
    WarrantyEndDate TEXT,

    Description TEXT,

    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT,

    FOREIGN KEY (BrandId)
        REFERENCES Brands(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    FOREIGN KEY (SupplierId)
        REFERENCES Suppliers(Id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,

    FOREIGN KEY (LocationId)
        REFERENCES Locations(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    FOREIGN KEY (ReceiverId)
        REFERENCES Receivers(Id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,

    FOREIGN KEY (StatusId)
        REFERENCES Statuses(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS AssetTransactions
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    AssetId INTEGER NOT NULL,
    ReceiverId INTEGER,
    LocationId INTEGER NOT NULL,
    UserId INTEGER NOT NULL,

    TransactionType INTEGER NOT NULL,
    TransactionDate TEXT NOT NULL,
    Description TEXT,

    FOREIGN KEY (AssetId)
        REFERENCES Assets(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    FOREIGN KEY (ReceiverId)
        REFERENCES Receivers(Id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,

    FOREIGN KEY (LocationId)
        REFERENCES Locations(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS Maintenance
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    AssetId INTEGER NOT NULL,

    CompanyName TEXT NOT NULL,

    Cost REAL DEFAULT 0,

    StartDate TEXT NOT NULL,

    EndDate TEXT,

    Status TEXT,

    Description TEXT,

    FOREIGN KEY (AssetId)
        REFERENCES Assets(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS AssetImages
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    AssetId INTEGER NOT NULL,

    ImagePath TEXT NOT NULL,

    Description TEXT,

    FOREIGN KEY (AssetId)
        REFERENCES Assets(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS AuditLogs
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    UserId INTEGER NOT NULL,

    Action TEXT NOT NULL,

    TableName TEXT NOT NULL,

    RecordId INTEGER,

    ActionDate TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,

    Description TEXT,

    FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);";

            command.ExecuteNonQuery();
        }

        private static void SeedData(SqliteConnection connection)
        {
            using var command = connection.CreateCommand();

            command.CommandText = @"

INSERT OR IGNORE INTO Departments (Id, Name)
VALUES
(1,'فناوری اطلاعات'),
(2,'مالی'),
(3,'اداری');

INSERT OR IGNORE INTO Statuses (Id, Name, Color)
VALUES
(1,'سالم','#22C55E'),
(2,'در تعمیر','#F59E0B'),
(3,'خراب','#EF4444'),
(4,'اسقاط','#6B7280');

INSERT OR IGNORE INTO Categories (Id, Name)
VALUES
(1,'لپ تاپ'),
(2,'کامپیوتر'),
(3,'مانیتور'),
(4,'پرینتر'),
(5,'تجهیزات شبکه');

INSERT OR IGNORE INTO Brands (Id, Name)
VALUES
(1,'Dell'),
(2,'HP'),
(3,'Lenovo'),
(4,'Asus'),
(5,'Cisco');

INSERT OR IGNORE INTO Locations (Id, DepartmentId, Name)
VALUES
(1,1,'اتاق سرور'),
(2,1,'واحد IT'),
(3,2,'واحد مالی'),
(4,3,'بایگانی');

INSERT OR IGNORE INTO Users
(Id, FullName, Username, PasswordHash, Role, IsActive)
VALUES
(1,'مدیر سیستم','admin','123456','Admin',1);

INSERT OR IGNORE INTO Suppliers
(Id, Name)
VALUES
(1,'شرکت آریا'),
(2,'شرکت فناوری نوین');

INSERT OR IGNORE INTO Receivers
(Id, FullName, PersonnelCode, DepartmentId, IsActive)
VALUES
(1,'علی احمدی','1001',1,1),
(2,'رضا محمدی','1002',2,1);

INSERT OR IGNORE INTO Assets
(
    Id,
    AssetCode,
    Name,
    BrandId,
    CategoryId,
    SupplierId,
    Model,
    SerialNumber,
    LocationId,
    ReceiverId,
    StatusId,
    Price,
    PurchaseDate,
    Description,
    CreatedAt
)
VALUES
(
    1,
    'A-1001',
    'لپ تاپ مدیر',
    1,
    1,
    1,
    'Latitude 5540',
    'SN123456',
    1,
    1,
    1,
    55000000,
    '2026-01-15',
    'نمونه آزمایشی',
    '2026-01-15'
);

";

            command.ExecuteNonQuery();
        }
    }
}