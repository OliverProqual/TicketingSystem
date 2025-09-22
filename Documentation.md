# 🎫 IT Ticketing System

A lightweight IT support ticketing system built with **C# (WinForms)** and **MySQL**.  
Supports **Customers** submitting tickets and **IT Operators** resolving them.  

---

## 📌 Features

- 🔐 **User Authentication** → Customers and Operators  
- 🎟 **Ticket Management** → Create, accept, update, complete tickets  
- 📊 **Role-Based Dashboards** → Separate UI for Customers and Operators  
- 🗄 **MySQL Database** → Central shared backend for all clients  
- 🔄 **LAN Collaboration** → Multiple users can connect to a shared MySQL server  

---

## 🗄 Database Setup

### 1. Create the Database

```sql
CREATE DATABASE thisdb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE thisdb;

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    department VARCHAR(100),
    role ENUM('customer','operator') NOT NULL
);

CREATE TABLE tickets (
    ticket_id INT AUTO_INCREMENT PRIMARY KEY,
    requested_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    accepted_time DATETIME NULL,
    completed_time DATETIME NULL,
    customer_id INT NOT NULL,
    operator_id INT NULL,
    status ENUM('incomplete','in-progress','complete') DEFAULT 'incomplete',
    description TEXT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES users(user_id),
    FOREIGN KEY (operator_id) REFERENCES users(user_id)
);
```

### 2. Add Test Users & Tickets

```sql
INSERT INTO users (username,password,name,email,department,role) VALUES
('alice','alice123','Alice Smith','alice@example.com','Sales','customer'),
('bob','bob123','Bob Jones','bob@example.com','HR','customer'),
('operator1','op123','Charlie IT','charlie@example.com','IT','operator');

INSERT INTO tickets (customer_id, description) VALUES
(1, 'Laptop is not turning on'),
(2, 'Email client not syncing');
```

---

## 👥 User Roles

### 🔹 Customers
- Login with role = `customer`  
- Create new tickets  
- View ticket status  

### 🔹 Operators
- Login with role = `operator`  
- View tickets assigned to them  
- Accept new tickets  
- Update status → *in-progress* or *complete*  

---

## 💻 Application Setup

### Requirements
- **Windows**
- **.NET Framework / .NET 6+**
- **MySQL Server 8.0+**
- **MySQL Connector/NET**

### Connection String Example

```csharp
string connection = "server=192.168.1.50;uid=teamuser;pwd=team123;database=thisdb;port=3306;";
```

- `192.168.1.50` → your MySQL server IP  
- `teamuser` / `team123` → MySQL credentials  
- `thisdb` → schema  

---

## 🌐 Shared Database Setup

1. **Install MySQL Server** on office server.  
2. Edit `my.ini` → under `[mysqld]` add:  
   ```ini
   bind-address=0.0.0.0
   ```  
3. Restart MySQL service.  
4. Create a shared DB user:  
   ```sql
   CREATE USER 'teamuser'@'%' IDENTIFIED BY 'team123';
   GRANT ALL PRIVILEGES ON thisdb.* TO 'teamuser'@'%';
   FLUSH PRIVILEGES;
   ```
5. Open firewall port **3306**.  
6. Coworkers connect with server IP from `ipconfig`.  

---

## 🚀 Usage Flow

1. **Login**
   - App checks role (`customer` or `operator`)
   - Redirects to appropriate dashboard

2. **Customer Dashboard**
   - Button: *New Ticket* → fills `description` → saves to DB
   - Grid: *My Tickets* → view status

3. **Operator Dashboard**
   - Grid: *Assigned Tickets* → loads tickets with `operator_id = current user`
   - Button: *Accept Ticket* → assigns operator
   - Button: *Update Status* → sets ticket → *in-progress* or *complete*

---

## 🧑‍💻 Code Examples

### User Class

```csharp
public class User {
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public User(int id, string name, string email) {
        UserID = id;
        UserName = name;
        Email = email;
    }
}
```

### Customer

```csharp
internal class Customer : User {
    public Customer(int id, string name, string email) : base(id, name, email) {}

    public Ticket MakeTicket(string description) {
        return new Ticket(0, description, this.UserID, 0);
    }
}
```

### Operator

```csharp
public class Operator : User {
    public Operator(int id, string name, string email) : base(id, name, email) {}

    public void AcceptTicket(Ticket tick) {
        tick.OperatorID = this.UserID;
    }

    public void UpdateStatus(Ticket tick, bool complete) {
        tick.Status = complete ? "complete" : "in-progress";
    }
}
```

### Ticket

```csharp
public class Ticket {
    public int TicketID { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int CustomerID { get; set; }
    public int OperatorID { get; set; }

    public Ticket(int id, string desc, int customer, int op) {
        TicketID = id;
        Description = desc;
        Status = "incomplete";
        CustomerID = customer;
        OperatorID = op;
    }
}
```

### Login Button Example

```csharp
private void button1_Click(object sender, EventArgs e) {
    string connection = "server=192.168.1.50;uid=teamuser;pwd=team123;database=thisdb;port=3306;";
    using (MySqlConnection con = new MySqlConnection(connection)) {
        con.Open();
        string query = "SELECT * FROM users WHERE username = @user AND password = @pass";
        MySqlCommand cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@user", UserIdBox.Text);
        cmd.Parameters.AddWithValue("@pass", PasswordBox.Text);

        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read()) {
            string role = reader["role"].ToString();
            int id = Convert.ToInt32(reader["user_id"]);
            string name = reader["name"].ToString();
            string email = reader["email"].ToString();

            if (role == "operator") {
                Operator op = new Operator(id, name, email);
                OperatorDashboard dash = new OperatorDashboard(op);
                dash.Show();
                this.Hide();
            } else {
                Customer cust = new Customer(id, name, email);
                CustomerDashboard dash = new CustomerDashboard(cust);
                dash.Show();
                this.Hide();
            }
        } else {
            MessageBox.Show("Invalid login");
        }
    }
}
```

### Update Ticket Status Button Example

```csharp
private void btnUpdateStatus_Click(object sender, EventArgs e) {
    if (dataGridView1.SelectedRows.Count > 0) {
        int ticketId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ticket_id"].Value);

        using (MySqlConnection con = new MySqlConnection(connectionString)) {
            con.Open();
            string sql = "UPDATE tickets SET status = 'complete', completed_time = NOW() WHERE ticket_id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", ticketId);
            cmd.ExecuteNonQuery();
        }

        MessageBox.Show("Ticket updated successfully!");
        LoadTickets(); // reloads grid
    }
}
```

---

## 📂 Project Structure

```
IT-Ticketing-System/
│── Database/
│   └── schema.sql
│── Forms/
│   ├── LoginForm.cs
│   ├── CustomerDashboard.cs
│   ├── OperatorDashboard.cs
│   └── TicketView.cs
│── Models/
│   ├── User.cs
│   ├── Customer.cs
│   ├── Operator.cs
│   └── Ticket.cs
│── README.md
```

---

## 🔐 Security Notes

- Use **hashed passwords** (`SHA2(password, 256)`) instead of plain text.  
- Restrict MySQL user privileges (only `thisdb.*`).  
- Keep server LAN-only unless remote access is necessary.  

---

## 📜 License

Internal project for company IT support.
