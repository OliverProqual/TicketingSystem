using System;
using System.Data;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class CustomerDashboard : Form
    {
        private readonly Customer cust;
        private readonly TicketRepository ticketRepo;

        public CustomerDashboard(Customer rat)
        {
            InitializeComponent();
            this.cust = rat;
            this.ticketRepo = new TicketRepository();

            LoadTickets();
        }

        private void LoadTickets()
        {
            try
            {
                DataTable dt = ticketRepo.GetTicketsByCustomerId(cust.UserID);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tickets: {ex.Message}");
            }
        }

        private void AddTicket_Click(object sender, EventArgs e)
        {
            TicketMaker TM = new TicketMaker(cust);
            TM.ShowDialog();

            // Refresh tickets after adding a new one
            LoadTickets();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
}
