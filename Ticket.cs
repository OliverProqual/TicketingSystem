using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }            // "Incomplete", "In Progress", "Completed", etc.
        public int CustomerID { get; set; }
        public int? OperatorID { get; set; }          // nullable, since ticket may not be assigned yet
        public DateTime TimeRequested { get; set; }
        public DateTime? TimeAccepted { get; set; }
        public DateTime? TimeCompleted { get; set; }

        public Ticket(int ticketID, string description, int customerID, int? operatorID,
                      string status, DateTime requested)
        {
            TicketID = ticketID;
            Description = description;
            CustomerID = customerID;
            OperatorID = operatorID;
            Status = status;
            TimeRequested = requested;
            TimeAccepted = null;
            TimeCompleted = null;
        }
    }
}
