using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformCRUDOperation
{
    public class MySQLDataAccess
    {
        private string MyCS = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

        //Retrieve data from database
        public DataTable GetData(string query)
        {
            using (MySqlConnection con = new MySqlConnection(MyCS))
            {
                DataTable myDataTable = new DataTable();
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        using (MySqlDataAdapter adr = new MySqlDataAdapter(cmd))
                        {
                            adr.Fill(myDataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (GetData)", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                }

                return myDataTable;

            }
        }

        //This method should be used to update or insert data into database
        public void ExecuteQuery(string query)
        {
            using(MySqlConnection con = new MySqlConnection(MyCS))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (ExecuteQuery)", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }
    }
}
