DROP DATABASE IF EXISTS thisdb;
CREATE DATABASE thisdb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE thisdb;

-- Departments
CREATE TABLE Departments (
    department_id INT AUTO_INCREMENT PRIMARY KEY,
    department_name VARCHAR(100) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ClearanceLevels (roles)
CREATE TABLE ClearanceLevels (
    clearance_id INT AUTO_INCREMENT PRIMARY KEY,
    clearance_name VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Users
CREATE TABLE Users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    department_id INT NULL,
    clearance_id INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (department_id) REFERENCES Departments(department_id)
        ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (clearance_id) REFERENCES ClearanceLevels(clearance_id)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- TicketStatus
CREATE TABLE TicketStatus (
    status_id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tickets
CREATE TABLE Tickets (
    ticket_id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    operator_id INT NULL,
    status_id INT NOT NULL,
    description TEXT NOT NULL,
    time_requested DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    time_accepted DATETIME NULL,
    time_completed DATETIME NULL,
    FOREIGN KEY (customer_id) REFERENCES Users(user_id)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    FOREIGN KEY (operator_id) REFERENCES Users(user_id)
        ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (status_id) REFERENCES TicketStatus(status_id)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Seed Departments
INSERT INTO Departments (department_name) VALUES
('IT'), ('HR'), ('Finance'), ('Sales')
ON DUPLICATE KEY UPDATE department_name = VALUES(department_name);

-- Seed ClearanceLevels
INSERT INTO ClearanceLevels (clearance_name) VALUES
('Customer'), ('Operator')
ON DUPLICATE KEY UPDATE clearance_name = VALUES(clearance_name);

-- Seed TicketStatus
INSERT INTO TicketStatus (status_name) VALUES
('Incomplete'), ('In Progress'), ('Completed'), ('Closed')
ON DUPLICATE KEY UPDATE status_name = VALUES(status_name);

-- Seed Customers (password=SHA2('password',256))
INSERT INTO Users (username, password_hash, full_name, email, department_id, clearance_id)
VALUES
('alice', SHA2('password', 256), 'Alice Smith', 'alice@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'HR'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer')),
('bob', SHA2('password', 256), 'Bob Johnson', 'bob@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'Finance'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer')),
('carol', SHA2('password', 256), 'Carol White', 'carol@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'Sales'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer'))
ON DUPLICATE KEY UPDATE
    full_name = VALUES(full_name),
    email = VALUES(email),
    department_id = VALUES(department_id),
    clearance_id = VALUES(clearance_id),
    password_hash = VALUES(password_hash);

-- Seed Operators (password=SHA2('password',256))
INSERT INTO Users (username, password_hash, full_name, email, department_id, clearance_id)
VALUES
('it_john', SHA2('password', 256), 'John IT', 'john.it@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'IT'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Operator')),
('it_sarah', SHA2('password', 256), 'Sarah IT', 'sarah.it@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'IT'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Operator')),
('it_amy', SHA2('password', 256), 'Amy Ops', 'amy.ops@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'IT'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Operator'))
ON DUPLICATE KEY UPDATE
    full_name = VALUES(full_name),
    email = VALUES(email),
    department_id = VALUES(department_id),
    clearance_id = VALUES(clearance_id),
    password_hash = VALUES(password_hash);

-- Seed Tickets
INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_requested)
VALUES
((SELECT user_id FROM Users WHERE username = 'alice'), NULL,
 (SELECT status_id FROM TicketStatus WHERE status_name = 'Incomplete'),
 'Printer in HR is jammed and shows error code E13.', NOW() - INTERVAL 6 HOUR),
((SELECT user_id FROM Users WHERE username = 'bob'),
 (SELECT user_id FROM Users WHERE username = 'it_john'),
 (SELECT status_id FROM TicketStatus WHERE status_name = 'In Progress'),
 'Laptop keeps restarting randomly; happens when opening Outlook.', NOW() - INTERVAL 2 DAY),
((SELECT user_id FROM Users WHERE username = 'carol'),
 (SELECT user_id FROM Users WHERE username = 'it_sarah'),
 (SELECT status_id FROM TicketStatus WHERE status_name = 'Completed'),
 'Cannot connect to company WiFi on floor 3.', NOW() - INTERVAL 3 DAY),
((SELECT user_id FROM Users WHERE username = 'alice'),
 (SELECT user_id FROM Users WHERE username = 'it_sarah'),
 (SELECT status_id FROM TicketStatus WHERE status_name = 'Closed'),
 'Email account locked out due to too many bad password attempts.', NOW() - INTERVAL 10 DAY),
((SELECT user_id FROM Users WHERE username = 'bob'), NULL,
 (SELECT status_id FROM TicketStatus WHERE status_name = 'Incomplete'),
 'Request: Need VPN access for new contractor.', NOW() - INTERVAL 3 HOUR);

-- Indexes
CREATE INDEX idx_tickets_operator ON Tickets(operator_id);
CREATE INDEX idx_tickets_status ON Tickets(status_id);
CREATE INDEX idx_users_clearance ON Users(clearance_id);

