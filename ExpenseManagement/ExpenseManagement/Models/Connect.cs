using System.Data.SqlClient;
using System;
using System.Data;

namespace ExpenseManagement.Model
{
    internal class Connect
    {
        private readonly string connectionString = @"Data Source=DESKTOP-HCNCO2M\SQLEXPRESS;Initial Catalog=expense_management;Integrated Security=True;TrustServerCertificate=True";

        private SqlConnection connection;

        public SqlConnection Connection { get; internal set; }

        /// <summary>
        /// Khởi tạo kết nối.
        /// </summary>
        public Connect()
        {
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Mở kết nối đến cơ sở dữ liệu.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error opening connection: {ex.Message}");
                throw; // Ném ngoại lệ lên tầng trên để xử lý
            }
        }

        /// <summary>
        /// Đóng kết nối với cơ sở dữ liệu.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái kết nối hiện tại.
        /// </summary>
        public string CheckConnectionState()
        {
            return connection.State.ToString();
        }

        /// <summary>
        /// Thực thi câu lệnh không trả về kết quả (INSERT, UPDATE, DELETE).
        /// </summary>
        public bool ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing non-query: {ex.Message}");
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Thực thi câu lệnh trả về dữ liệu dạng DataTable (SELECT).
        /// </summary>
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        /// <summary>
        /// Thực thi câu lệnh trả về giá trị đơn lẻ (ví dụ: COUNT, MAX, MIN).
        /// </summary>
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing scalar: {ex.Message}");
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
