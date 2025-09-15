DROP DATABASE IF EXISTS thisdb;

CREATE DATABASE IF NOT EXISTS thisdb;
USE thisdb;

CREATE TABLE Departments (
    department_id INT AUTO_INCREMENT PRIMARY KEY,
    department_name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE ClearanceLevels (
    clearance_id INT AUTO_INCREMENT PRIMARY KEY,
    clearance_name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    department_id INT,
    clearance_id INT NOT NULL,
    FOREIGN KEY (department_id) REFERENCES Departments(department_id)
        ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (clearance_id) REFERENCES ClearanceLevels(clearance_id)
        ON UPDATE CASCADE ON DELETE RESTRICT
);

CREATE TABLE TicketStatus (
    status_id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Tickets (
    ticket_id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    operator_id INT,
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
);


INSERT INTO Departments (department_name) VALUES 
('IT'), ('HR'), ('Finance'), ('Sales');

INSERT INTO ClearanceLevels (clearance_name) VALUES 
('Customer'), ('Operator');

INSERT INTO TicketStatus (status_name) VALUES 
('Incomplete'), ('In Progress'), ('Completed'), ('Closed');

INSERT INTO Users (username, password_hash, full_name, email, department_id, clearance_id)
VALUES
('alice', 'hash_pw1', 'Alice Smith', 'alice@example.com', 
    (SELECT department_id FROM Departments WHERE department_name = 'HR'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer')),
('bob', 'hash_pw2', 'Bob Johnson', 'bob@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'Finance'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer')),
('carol', 'hash_pw3', 'Carol White', 'carol@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'Sales'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Customer'));

-- -------------------------
-- Users: Operators
-- -------------------------
INSERT INTO Users (username, password_hash, full_name, email, department_id, clearance_id)
VALUES
('it_john', 'hash_pw4', 'John IT', 'john.it@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'IT'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Operator')),
('it_sarah', 'hash_pw5', 'Sarah IT', 'sarah.it@example.com',
    (SELECT department_id FROM Departments WHERE department_name = 'IT'),
    (SELECT clearance_id FROM ClearanceLevels WHERE clearance_name = 'Operator'));

-- -------------------------
-- Tickets
-- -------------------------

-- 1. Unassigned ticket (Incomplete)
INSERT INTO Tickets (customer_id, status_id, description)
VALUES (
    (SELECT user_id FROM Users WHERE username = 'alice'),
    (SELECT status_id FROM TicketStatus WHERE status_name = 'Incomplete'),
    'Printer in HR is jammed.'
);

-- 2. Assigned but not completed (In Progress)
INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_accepted)
VALUES (
    (SELECT user_id FROM Users WHERE username = 'bob'),
    (SELECT user_id FROM Users WHERE username = 'it_john'),
    (SELECT status_id FROM TicketStatus WHERE status_name = 'In Progress'),
    'Laptop keeps restarting randomly.',
    NOW()
);

-- 3. Completed ticket
INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_accepted, time_completed)
VALUES (
    (SELECT user_id FROM Users WHERE username = 'carol'),
    (SELECT user_id FROM Users WHERE username = 'it_sarah'),
    (SELECT status_id FROM TicketStatus WHERE status_name = 'Completed'),
    'Cannot connect to WiFi network.',
    NOW() - INTERVAL 2 HOUR,
    NOW()
);

-- 4. Closed ticket
INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_accepted, time_completed)
VALUES (
    (SELECT user_id FROM Users WHERE username = 'alice'),
    (SELECT user_id FROM Users WHERE username = 'it_sarah'),
    (SELECT status_id FROM TicketStatus WHERE status_name = 'Closed'),
    'Email account locked out.',
    NOW() - INTERVAL 5 HOUR,
    NOW() - INTERVAL 1 HOUR
);
