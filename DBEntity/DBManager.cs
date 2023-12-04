using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEntity
{
    public class DBManager
    {
        public string ConnectionString { get; set; }

        private SqlConnection sqlConnection { get; set; }

        public DBManager(string connectionstring)
        {
            ConnectionString = connectionstring;
            sqlConnection = new SqlConnection(ConnectionString);
        }

        public async Task<bool> OpenConnetion()
        {
            try
            {
                await sqlConnection.OpenAsync();
                return true;
            }
            catch (SqlException sql_ex)
            {
                throw new Exception(sql_ex.Message);
            }
        }

        public async Task<bool> CloseConnetion()
        {
            try
            {
                await sqlConnection.CloseAsync();
                return true;
            }
            catch (SqlException sql_ex)
            {
                throw new Exception(sql_ex.Message);
            }
        }

        public async Task ExecuteNonQuery(string query)
        {
            try
            {
                await OpenConnetion();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                await CloseConnetion();
            }
        }
        public async Task<DataTable> ExecuteQuery(string query)
        {
            DataTable result = new DataTable();
            try
            {
                await OpenConnetion();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(result);
                }
            }
            catch (SqlException sql_ex)
            {
                throw new Exception(sql_ex.Message);
            }
            finally
            {
                await CloseConnetion();
            }
            return result;
        }

    }
}
