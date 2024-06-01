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

        public static bool addFuncionario(int userid, string nome, float salario, string cargo)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO FUNCIONARIOS VALUES (@userid, @nome, @salario, @cargo)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@salario", salario);
                        cmd.Parameters.AddWithValue("@cargo", cargo);
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
                    string query = "SELECT ID, DESCRICAO, VALOR, DATA FROM GANHOS WHERE USER_ID = @userId ORDER BY DATA DESC";
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
                    string query = "SELECT ID, DESCRICAO, VALOR, DATA FROM DESPESAS WHERE USER_ID = @userId ORDER BY DATA DESC";
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

        public static DataTable GetFuncionarios(int userId)
        {
            DataTable funcionariosTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, NOME, SALARIO, CARGO FROM FUNCIONARIOS WHERE USER_ID = @userId ORDER BY NOME";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(funcionariosTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os funcionários: " + ex.Message);
            }

            return funcionariosTable;
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

        public static bool AddTarefa(int userID, string descricao)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "INSERT INTO TAREFAS (USER_ID, DESCRICAO, CONCLUIDO) VALUES (@userID, @descricao, 0)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar a tarefa: " + ex.Message);
                return false;
            }
        }

        public static bool AddFornecedor(int userID, string nome, string email, string telefone, int categoria)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "INSERT INTO FORNECEDORES (USER_ID, NOME, EMAIL, TELEFONE, ID_CATEGORIA) VALUES (@userID, @nome, @email, @telefone, @categoria)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@categoria", categoria);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar o fornecedor: " + ex.Message);
                return false;
            }
        }

        public static bool AddCategoria(int userID, string nome)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "INSERT INTO CATEGORIAS (USER_ID, NOME) VALUES (@userID, @nome)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@nome", nome);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar a categoria: " + ex.Message);
                return false;
            }
        }

        public static DataTable GetCategorias(int userId)
        {
            DataTable categoriasTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, NOME FROM CATEGORIAS WHERE USER_ID = @userId ORDER BY NOME";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(categoriasTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter as categorias: " + ex.Message);
            }

            return categoriasTable;
        }



        public static DataTable GetEstoque(int userId)
        {
            DataTable estoqueTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"SELECT e.ID, e.PRODUTO, e.QUANTIDADE, c.NOME AS CATEGORIA 
                             FROM ESTOQUE e
                             JOIN CATEGORIAS c ON e.ID_CATEGORIA = c.ID
                             WHERE e.USER_ID = @userId 
                             ORDER BY e.PRODUTO";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(estoqueTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o estoque: " + ex.Message);
            }

            return estoqueTable;
        }



        public static bool AddProduto(int userId, string produto, int quantidade, int idCategoria)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "INSERT INTO ESTOQUE (USER_ID, PRODUTO, QUANTIDADE, ID_CATEGORIA) VALUES (@userId, @produto, @quantidade, @idCategoria)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.Parameters.AddWithValue("@quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar o produto ao estoque: " + ex.Message);
                return false;
            }
        }

        public static DataTable GetFornecedores(int userId)
        {
            DataTable fornecedoresTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"
                        SELECT f.ID, f.NOME, f.EMAIL, f.TELEFONE, c.NOME AS CATEGORIA 
                        FROM FORNECEDORES f
                        INNER JOIN CATEGORIAS c ON f.ID_CATEGORIA = c.ID
                        WHERE f.USER_ID = @userId
                        ORDER BY f.NOME";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(fornecedoresTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os fornecedores: " + ex.Message);
            }

            return fornecedoresTable;
        }

        public static int GetNumeroDeCategorias(int userId)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return -1;
                    }

                    string query = "SELECT COUNT(*) FROM CATEGORIAS WHERE USER_ID = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        int count = (int)cmd.ExecuteScalar();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o número de categorias: " + ex.Message);
                return -1;
            }
        }

        public static bool RemoverGanho(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM GANHOS WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover o ganho: " + ex.Message);
                return false;
            }
        }

        public static bool RemoverDespesa(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM DESPESAS WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover a despesa: " + ex.Message);
                return false;
            }
        }

        public static bool RemoverFuncionario(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM FUNCIONARIOS WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover o funcionário: " + ex.Message);
                return false;
            }
        }

        public static bool RemoverFornecedor(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM FORNECEDORES WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover o fornecedor: " + ex.Message);
                return false;
            }
        }

        public static bool RemoverProduto(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM ESTOQUE WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover o produto: " + ex.Message);
                return false;
            }
        }

        public static bool RemoverCategoria(int id)
        {
            DialogResult result = MessageBox.Show("Remover esta categoria também removerá todos os produtos e fornecedores que pertencem à esta categoria. Deseja continuar?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = Connect())
                    {
                        if (connection == null)
                        {
                            return false;
                        }

                        string query = "DELETE FROM CATEGORIAS WHERE ID = @id";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover a categoria: " + ex.Message);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }


        public static bool EditGanho(int id, string desc, float valor)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE GANHOS SET DESCRICAO = @desc, VALOR = @valor WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o ganho: " + ex.Message);
                return false;
            }
        }

        public static bool EditFuncionario(int id, string nome, float salario, string cargo)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE FUNCIONARIOS SET NOME = @nome, SALARIO = @salario, CARGO = @cargo WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@salario", salario);
                        cmd.Parameters.AddWithValue("@cargo", cargo);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o funcionário: " + ex.Message);
                return false;
            }
        }

        public static bool EditFornecedor(int id, string nome, string email, string telefone, int categoria)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE FORNECEDORES SET NOME = @nome, EMAIL = @email, TELEFONE = @telefone, ID_CATEGORIA = @categoria WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o fornecedor: " + ex.Message);
                return false;
            }
        }

        public static bool EditProduto(int id, string produto, int quantidade, int idCategoria)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE ESTOQUE SET PRODUTO = @produto, QUANTIDADE = @quantidade, ID_CATEGORIA = @idCategoria WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.Parameters.AddWithValue("@quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o produto: " + ex.Message);
                return false;
            }
        }

        public static bool EditDespesa(int id, string desc, float valor)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE DESPESAS SET DESCRICAO = @desc, VALOR = @valor WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar a despesa: " + ex.Message);
                return false;
            }
        }

        public static bool EditCategoria(int id, string nome)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE CATEGORIAS SET NOME = @nome WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar a categoria: " + ex.Message);
                return false;
            }
        }
    }
}
