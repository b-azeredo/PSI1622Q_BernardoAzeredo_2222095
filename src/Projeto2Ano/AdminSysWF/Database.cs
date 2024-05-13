using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdminSysWF
{
    public static class Database
    {
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=AdminSys;Integrated Security=True";

        public static SqlConnection Connect()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar a base de dados: " + ex.Message);
                return null;
            }
        }

        public static int GetIdByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT ID FROM UTILIZADORES WHERE USERNAME = @username", connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userID;
                                if (int.TryParse(reader["ID"].ToString(), out userID))
                                {
                                    return userID;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao conectar com a base de dados.");
                return -1;
            }

        }

        public static bool Login(string username, string password)
        {
            string query = "SELECT PASSWORD FROM UTILIZADORES WHERE USERNAME = @username";

            using (SqlConnection connection = Connect())
            {
                if (connection == null)
                {
                    return false;
                }

                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    try
                    {
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader.GetString(0);
                                if (storedPassword == password)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao executar a consulta: " + ex.Message);
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool Register(string username, string password)
        {
            string query = "SELECT PASSWORD FROM UTILIZADORES WHERE USERNAME = @username";

            using (SqlConnection connection = Connect())
            {
                if (connection == null)
                {
                    return false;
                }

                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    try
                    {
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao executar a consulta: " + ex.Message);
                        return false;
                    }
                }
            }

            query = "INSERT INTO UTILIZADORES VALUES (@username, @password)";
            using (SqlConnection connection = Connect())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool addDespesa(int userid ,string desc, float valor)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO DESPESAS VALUES (@userid, @desc, @valor, @data)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao conectar à base de dados.");
                return false;
            }
        }

        public static bool addLucro(int userid, string desc, float valor)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO GANHOS VALUES (@userid, @desc, @valor, @data)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao conectar à base de dados.");
                return false;
            }
        }

        public static float GetLucroDia(DateTime dia, int userId)
        {
            float lucroDia = 0;

            try
            {
                float lucroDiaTotal = GetGanhosDia(dia, userId);
                float despesaDiaTotal = GetDespesaDia(dia, userId);

                lucroDia = lucroDiaTotal - despesaDiaTotal;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o lucro - despesa para o dia " + dia.ToShortDateString() + ": " + ex.Message);
            }

            return lucroDia;
        }

        public static float GetGanhosDia(DateTime dia, int userId)
        {
            float lucroDia = 0;

            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(VALOR) FROM GANHOS WHERE CONVERT(date, DATA) = @data AND USER_ID = @userId", connection))
                    {
                        cmd.Parameters.AddWithValue("@data", dia.Date);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            lucroDia = Convert.ToSingle(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o lucro para o dia " + dia.ToShortDateString() + ": " + ex.Message);
            }

            return lucroDia;
        }


        public static float GetDespesaDia(DateTime dia, int userId)
        {
            float despesaDia = 0;

            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(VALOR) FROM DESPESAS WHERE CONVERT(date, DATA) = @data AND USER_ID = @userId", connection))
                    {
                        cmd.Parameters.AddWithValue("@data", dia.Date);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            despesaDia = Convert.ToSingle(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a despesa para o dia " + dia.ToShortDateString() + ": " + ex.Message);
            }

            return despesaDia;
        }

        public static DataTable GetGanhos(int userId)
        {
            DataTable GANHOSTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT DESCRICAO, VALOR, DATA FROM GANHOS WHERE USER_ID = @userId ORDER BY DATA DESC";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(GANHOSTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os GANHOS: " + ex.Message);
            }

            return GANHOSTable;
        }

        public static DataTable GetDespesas(int userId)
        {
            DataTable despesasTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT DESCRICAO, VALOR, DATA FROM DESPESAS WHERE USER_ID = @userId ORDER BY DATA DESC";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(despesasTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os lucros: " + ex.Message);
            }

            return despesasTable;
        }

        public static DataTable GetTarefas(int userId)
        {
            DataTable tarefasTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, DESCRICAO FROM TAREFAS WHERE USER_ID = @userId AND CONCLUIDO = 0";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(tarefasTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter as tarefas: " + ex.Message);
            }

            return tarefasTable;
        }

        public static void ConcluirTarefa(int idTarefa)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "UPDATE TAREFAS SET CONCLUIDO = 1 WHERE ID = @idTarefa";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@idTarefa", idTarefa);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao concluir a tarefa: " + ex.Message);
            }
        }

    }
}
