using System;
using System.Data;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class UnassignedTicketView : Form
    {
        private int ticketId;
        private Operator CurrentOperator;
        private TicketRepository repo;

        public UnassignedTicketView(int ticketId, Operator op)
        {
            InitializeComponent();
            this.ticketId = ticketId;
            this.CurrentOperator = op;
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

        private void Accept_Click(object sender, EventArgs e)
        {
            if (repo.AcceptTicket(ticketId, CurrentOperator.UserID))
            {
                MessageBox.Show("Ticket accepted successfully!");
                OperatorDashBoard dash = new OperatorDashBoard(CurrentOperator);
                dash.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("This ticket has already been accepted by another operator.");
            }
        }
    }
}
