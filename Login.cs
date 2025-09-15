using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class Login : Form
    {
        private readonly DataAccess db;

        public Login()
        {
            InitializeComponent();
            db = new DataAccess();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"
                SELECT u.user_id, u.username, u.full_name, u.email, c.clearance_name
                FROM Users u
                JOIN ClearanceLevels c ON u.clearance_id = c.clearance_id
                WHERE u.username = @val1 AND u.password_hash = @val2";

            var parameters = new Dictionary<string, object>
            {
                { "@val1", UserIdBox.Text },
                { "@val2", PasswordBox.Text } // ⚠️ In production, hash this!
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                string clearance = dt.Rows[0]["clearance_name"].ToString();

                if (clearance.Equals("Operator", StringComparison.OrdinalIgnoreCase))
                {
                    Operator user = new Operator(
                        Convert.ToInt32(dt.Rows[0]["user_id"]),
                        dt.Rows[0]["username"].ToString(),
                        dt.Rows[0]["full_name"].ToString(),
                        dt.Rows[0]["email"].ToString()
                    );

                    new OperatorDashBoard(user).Show();
                    this.Hide();
                }
                else
                {
                    Customer user = new Customer(
                        Convert.ToInt32(dt.Rows[0]["user_id"]),
                        dt.Rows[0]["username"].ToString(),
                        dt.Rows[0]["full_name"].ToString(),
                        dt.Rows[0]["email"].ToString()
                    );

                    new CustomerDashboard(user).Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}

