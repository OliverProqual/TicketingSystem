using System;
using System.Windows.Forms;

namespace TicketingSystem
{
    public partial class TicketMaker : Form
    {
        private Customer currentCustomer;
        private TicketRepository repo;

        public TicketMaker(Customer customer)
        {
            InitializeComponent();
            this.currentCustomer = customer;
            this.repo = new TicketRepository();
        }

        private void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description for your issue.");
                return;
            }

            bool success = repo.CreateTicket(currentCustomer.UserID, txtDescription.Text);

            if (success)
            {
                MessageBox.Show("Your ticket has been submitted successfully!");
                this.Close(); // close form after submission
            }
            else
            {
                MessageBox.Show("There was a problem submitting your ticket.");
            }
        }
    }
}
