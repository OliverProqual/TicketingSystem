using System;
using System.Collections.Generic;
using System.Data;

namespace TicketingSystem
{
    public class TicketRepository
    {
        private readonly DataAccess db;

        public TicketRepository()
        {
            db = new DataAccess();
        }

        // 🔹 Get all tickets for a given operator
        public DataTable GetTicketsByOperatorId(int operatorId)
        {
            string query = "SELECT * FROM Tickets WHERE operator_id = @opId";
            var parameters = new Dictionary<string, object>
            {
                { "@opId", operatorId }
            };
            return db.ExecuteQuery(query, parameters);
        }

        // 🔹 Get all unassigned tickets
        public DataTable GetUnassignedTickets()
        {
            string query = "SELECT * FROM Tickets WHERE operator_id IS NULL";
            return db.ExecuteQuery(query);
        }

        // 🔹 Get all tickets for a specific customer
        public DataTable GetTicketsByCustomerId(int customerId)
        {
            string query = "SELECT * FROM Tickets WHERE customer_id = @custId";
            var parameters = new Dictionary<string, object>
            {
                { "@custId", customerId }
            };
            return db.ExecuteQuery(query, parameters);
        }

        // 🔹 Get details of one ticket
        public DataRow GetTicketById(int ticketId)
        {
            string query = @"
                SELECT t.ticket_id, t.description, s.status_name, 
                       t.time_requested, t.time_accepted, t.time_completed,
                       c.full_name AS customer_name, c.email AS customer_email
                FROM Tickets t
                JOIN TicketStatus s ON t.status_id = s.status_id
                JOIN Users c ON t.customer_id = c.user_id
                WHERE t.ticket_id = @tid";

            var parameters = new Dictionary<string, object>
            {
                { "@tid", ticketId }
            };

            DataTable dt = db.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // 🔹 Insert a new ticket
        public bool InsertTicket(int customerId, string description)
        {
            string query = @"
                INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_requested)
                VALUES (@custId, NULL, 
                        (SELECT status_id FROM TicketStatus WHERE status_name = 'Incomplete'),
                        @desc, NOW())";

            var parameters = new Dictionary<string, object>
            {
                { "@custId", customerId },
                { "@desc", description }
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        // 🔹 Update ticket status
        public bool UpdateTicketStatus(int ticketId, int newStatusId)
        {
            string query = @"
                UPDATE Tickets
                SET status_id = @statusId,
                    time_completed = CASE 
                        WHEN @statusId = (SELECT status_id FROM TicketStatus WHERE status_name = 'Completed')
                        THEN NOW()
                        ELSE time_completed
                    END
                WHERE ticket_id = @tid";

            var parameters = new Dictionary<string, object>
            {
                { "@statusId", newStatusId },
                { "@tid", ticketId }
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        // 🔹 Get all possible statuses
        public DataTable GetAllStatuses()
        {
            string query = "SELECT status_id, status_name FROM TicketStatus";
            return db.ExecuteQuery(query);
        }

        public DataTable GetStatuses()
        {
            string query = "SELECT status_id, status_name FROM TicketStatus";
            return db.ExecuteQuery(query);
        }

        public bool UpdateStatus(int ticketId, int newStatusId)
        {
            string query = @"
        UPDATE Tickets
        SET status_id = @statusId,
            time_completed = CASE 
                                WHEN @statusId = (SELECT status_id FROM TicketStatus WHERE status_name = 'Completed')
                                THEN NOW()
                                ELSE time_completed
                             END
        WHERE ticket_id = @tid";

            var parameters = new Dictionary<string, object>
            {
                { "@statusId", newStatusId },
                { "@tid", ticketId }
            };

            int rows = db.ExecuteNonQuery(query, parameters);
            return rows > 0;
        }

        public bool CreateTicket(int customerId, string description)
        {
            string query = @"
        INSERT INTO Tickets (customer_id, operator_id, status_id, description, time_requested)
        VALUES (@custId, NULL, 
               (SELECT status_id FROM TicketStatus WHERE status_name = 'Incomplete'),
               @desc, NOW())";

            var parameters = new Dictionary<string, object>
    {
        { "@custId", customerId },
        { "@desc", description }
    };

            int rows = db.ExecuteNonQuery(query, parameters);
            return rows > 0;
        }

        public bool AcceptTicket(int ticketId, int operatorId)
        {
            string query = @"
        UPDATE Tickets
        SET operator_id = @opId,
            time_accepted = CASE 
                               WHEN time_accepted IS NULL THEN NOW()
                               ELSE time_accepted
                            END,
            status_id = (SELECT status_id FROM TicketStatus WHERE status_name = 'In Progress')
        WHERE ticket_id = @tid
          AND (operator_id IS NULL OR operator_id = @opId)";

            var parameters = new Dictionary<string, object>
    {
        { "@opId", operatorId },
        { "@tid", ticketId }
    };

            int rows = db.ExecuteNonQuery(query, parameters);
            return rows > 0;
        }


    }
}
