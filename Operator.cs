using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem
{
    public class Operator : User
    {
        public Operator(int id, string userName, string fullName, string email)
            : base(id, userName, fullName, email)
        {
        }

        public void AcceptTicket(Ticket tick)
        {
            tick.OperatorID = this.UserID;
            tick.TimeAccepted = DateTime.Now;
            tick.Status = "In Progress";
        }

        public void CompleteTicket(Ticket tick)
        {
            tick.Status = "Completed";
            tick.TimeCompleted = DateTime.Now;
        }
    }
}
