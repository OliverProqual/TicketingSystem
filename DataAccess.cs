using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TicketingSystem
{
    public class DataAccess
    {
        private readonly string connectionString =
             "server=172.16.1.183;uid=olly;pwd=ollypass;database=thisdb;port=3306";

        // 🔹 Run SELECT queries and return results in a DataTable
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var con = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                AddParameters(cmd, parameters);

                using (var da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 🔹 Run INSERT, UPDATE, DELETE (returns # rows affected)
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var con = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                AddParameters(cmd, parameters);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Run scalar queries (return a single value, e.g. COUNT(*))
        public object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            using (var con = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                AddParameters(cmd, parameters);

                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        // 🔹 Internal helper to safely add parameters
        private void AddParameters(MySqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            foreach (var kvp in parameters)
            {
                cmd.Parameters.AddWithValue(kvp.Key, kvp.Value ?? DBNull.Value);
            }
        }
    }
}
