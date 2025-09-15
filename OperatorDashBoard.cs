using System;
using System.Data;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class OperatorDashBoard : Form
    {
        private Operator op;
        private TicketRepository ticketRepo;

        public OperatorDashBoard(Operator rat)
        {
            InitializeComponent();
            this.op = rat;
            this.ticketRepo = new TicketRepository();

            LoadTickets();
        }

        private void LoadTickets()
        {
            // ✅ Tickets assigned to this operator
            DataTable dtAssigned = ticketRepo.GetTicketsByOperatorId(op.UserID);
            dataGridView1.DataSource = dtAssigned;

            // ✅ Tickets with no operator assigned
            DataTable dtUnassigned = ticketRepo.GetUnassignedTickets();
            dataGridView2.DataSource = dtUnassigned;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignore header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int ticketId = Convert.ToInt32(row.Cells["ticket_id"].Value);

                // Open new form for this ticket
                TicketViewForm tvf = new TicketViewForm(ticketId);
                tvf.Show();
                this.Hide();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignore header row
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                int ticketId = Convert.ToInt32(row.Cells["ticket_id"].Value);

                // Open new form for this unassigned ticket
                UnassignedTicketView tvf = new UnassignedTicketView(ticketId, this.op);
                tvf.Show();
                this.Hide();
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
}

