using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem
{
    public class Customer : User
    {
        public Customer(int id, string userName, string fullName, string email)
            : base(id, userName, fullName, email)
        {
        }

        public Ticket MakeTicket(string description)
        {
            return new Ticket(0, description, this.UserID, null, "Incomplete", DateTime.Now);
        }

        public string QueryStatus(Ticket tick)
        {
            return tick.Status;
        }
    }
}
