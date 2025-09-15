using System;
using System.Data;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class TicketViewForm : Form
    {
        private int ticketId;
        private TicketRepository repo;

        public TicketViewForm(int ticketId)
        {
            InitializeComponent();
            this.ticketId = ticketId;
            this.repo = new TicketRepository();

            LoadStatuses();
            LoadTicket();
        }

        private void LoadTicket()
        {
            DataRow row = repo.GetTicketById(ticketId);

            if (row != null)
            {
                lblTicketId.Text = row["ticket_id"].ToString();
                txtDescription.Text = row["description"].ToString();
                lblStatus.Text = row["status_name"].ToString();
                lblCustomer.Text = row["customer_name"].ToString();
                lblEmail.Text = row["customer_email"].ToString();

                lblRequested.Text = row["time_requested"].ToString();
                lblAccepted.Text = row["time_accepted"].ToString();
                lblCompleted.Text = row["time_completed"].ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // This button has no action anymore, safe to leave empty
        }


        private void UpdateStatus_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid status.");
                return;
            }

            int newStatusId = Convert.ToInt32(cmbStatus.SelectedValue);

            if (repo.UpdateStatus(ticketId, newStatusId))
            {
                MessageBox.Show("Ticket status updated successfully!");
                LoadTicket();
            }
            else
            {
                MessageBox.Show("Failed to update ticket status.");
            }
        }

        private void LoadStatuses()
        {
            DataTable dt = repo.GetStatuses();
            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = "status_name";
            cmbStatus.ValueMember = "status_id";
        }
    }
}

