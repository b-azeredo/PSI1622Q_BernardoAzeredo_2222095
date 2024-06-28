using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdminSysWF
{
    public static class Database
    {
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=AdminSys;Integrated Security=True";


        // Método que retorna a conexão com a database
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


        // Retorna True se o utilizador for um Admin
        public static bool isAdmin(int userid)
        {
            bool isAdmin = false;
            string query = "SELECT ADMIN FROM UTILIZADORES WHERE ID = @userid";

            using (SqlConnection connection = Connect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userid);

                    try
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            isAdmin = Convert.ToBoolean(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao executar a consulta: " + ex.Message);
                    }
                }
            }

            return isAdmin;
        }


        // Retorna o ID de um utilizador pelo o nome
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


        // Retorna True caso o login for bem sucedido, caso contrário retorna false
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


        // Regista novos utilizadores
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

            query = "INSERT INTO UTILIZADORES VALUES (@username, @password, 28, 0)";
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


        // Retorna o lucro total do mês
        public static float GetLucroMensal(int userId)
        {
            float lucroMensal = 0;

            try
            {
                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaMes = new DateTime(hoje.Year, hoje.Month, 1);
                DateTime primeiroDiaProximoMes = primeiroDiaMes.AddMonths(1);

                for (DateTime dia = primeiroDiaMes; dia < primeiroDiaProximoMes; dia = dia.AddDays(1))
                {
                    float lucroDia = GetLucroDia(dia, userId);
                    lucroMensal += lucroDia;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o lucro mensal: " + ex.Message);
            }

            return lucroMensal;
        }

        public static float GetLucroMensalMesAnterior(int userId)
        {
            float lucroMensalAnterior = 0;

            try
            {
                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaMesAnterior = new DateTime(hoje.Year, hoje.Month, 1).AddMonths(-1);
                DateTime primeiroDiaMes = new DateTime(hoje.Year, hoje.Month, 1);

                for (DateTime dia = primeiroDiaMesAnterior; dia < primeiroDiaMes; dia = dia.AddDays(1))
                {
                    float lucroDia = GetLucroDia(dia, userId);
                    lucroMensalAnterior += lucroDia;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o lucro do mês anterior: " + ex.Message);
            }

            return lucroMensalAnterior;
        }


        //Método para adicionar uma despesa
        public static bool addDespesa(int userid, string desc, float valor)
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


        //Método para adicionar um novo funcionário
        public static bool addFuncionario(int userid, string nome, float salario, int cargo)
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

        // Método para adicionar um lucro
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


        // Retorna a soma do lucro (ganhos - despesas) em um dia específico
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

        // Retorna a soma do lucro total na semana atual
        public static float GetLucroSemanal(int userId)
        {
            float lucroSemanal = 0;

            try
            {
                DateTime hoje = DateTime.Today;
                DateTime ultimoDomingo = hoje.AddDays(-(int)hoje.DayOfWeek);

                for (int i = 0; i < 7; i++)
                {
                    DateTime dia = ultimoDomingo.AddDays(i);
                    float lucroDia = GetLucroDia(dia, userId);
                    lucroSemanal += lucroDia;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o lucro semanal a partir do último domingo: " + ex.Message);
            }

            return lucroSemanal;
        }

        // Retorrna a soma total de ganhos no mês atual
        public static float GetGanhosMensal(int userId)
        {
            float ganhoMensal = 0;

            try
            {
                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);
                DateTime primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
                DateTime ultimoDiaDoMes = primeiroDiaDoProximoMes.AddDays(-1);

                for (DateTime dia = primeiroDiaDoMes; dia <= ultimoDiaDoMes; dia = dia.AddDays(1))
                {
                    float lucroDia = GetGanhosDia(dia, userId);
                    ganhoMensal += lucroDia;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o ganho mensal: " + ex.Message);
            }

            return ganhoMensal;
        }

        // Retorna todos os ganhos de um dia específico
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

        // Calcula a porcentagem de variação de ganhos em relação ao mês mensal
        public static float CalcularPorcentagemVariacaoMensal(int userId)
        {
            float ganhoMensalAtual = GetGanhosMensal(userId);
            float ganhoMensalAnterior = GetGanhosMensalMesAnterior(userId);

            if (ganhoMensalAnterior == 0 || ganhoMensalAtual == 0)
            {
                return 0;
            }
            else
            {
                float variacaoPercentual = ((ganhoMensalAtual - ganhoMensalAnterior) / ganhoMensalAnterior) * 100;
                return variacaoPercentual;
            }
        }

        // Retorna os ganhos totais do mês anterior
        public static float GetGanhosMensalMesAnterior(int userId)
        {
            float ganhoMensal = 0;

            try
            {
                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaDoMesAtual = new DateTime(hoje.Year, hoje.Month, 1);
                DateTime primeiroDiaDoMesAnterior = primeiroDiaDoMesAtual.AddMonths(-1);
                DateTime ultimoDiaDoMesAnterior = primeiroDiaDoMesAtual.AddDays(-1);

                for (DateTime dia = primeiroDiaDoMesAnterior; dia <= ultimoDiaDoMesAnterior; dia = dia.AddDays(1))
                {
                    float lucroDia = GetGanhosDia(dia, userId);
                    ganhoMensal += lucroDia;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o ganho mensal do mês anterior: " + ex.Message);
            }

            return ganhoMensal;
        }

        // Calcula a soma total das despesas em um dia específico
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

            Definicoes def = GetDefinicoes(userId);
            if (dia.Day == def.diaFuncionario && dia.Day <= DateTime.Now.Day)
            {
                float despesaFuncionarios = GetDespesasFuncionario(userId);
                despesaDia += despesaFuncionarios;
            }

            return despesaDia;
        }

        // Retorna uma datatable com todos os utilizadores presentes na database
        public static DataTable GetUtilizadores(int userId)
        {
            DataTable UTILIZADORESTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, USERNAME, ADMIN FROM UTILIZADORES WHERE ID != @userId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(UTILIZADORESTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os utilizadores: " + ex.Message);
            }

            return UTILIZADORESTable;
        }

        // Retorna uma datatable com todos os ganhos presentes na database

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

        // Retorna uma datatable com todos as despesas presentes na database
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

                    var def = GetDefinicoes(userId);

                    if (def.diaFuncionario <= DateTime.Now.Day)
                    {
                        float despesaFuncionarios = GetDespesasFuncionario(userId);
                        DateTime DataDespesaFuncionario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, def.diaFuncionario, 0, 0, 0);

                        DataRow novaLinha = despesasTable.NewRow();
                        novaLinha["ID"] = -1;
                        novaLinha["DESCRICAO"] = "Salário Funcionários";
                        novaLinha["VALOR"] = despesaFuncionarios;
                        novaLinha["DATA"] = DataDespesaFuncionario;
                        despesasTable.Rows.Add(novaLinha);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter as despesas: " + ex.Message);
            }

            return despesasTable;
        }


        // Retorna a soma de todas despesas do mês atual presentes na database

        public static float GetDespesasMensal(int userId)
        {
            float despesasMensal = 0;

            try
            {
                DataTable despesasTable = GetDespesas(userId);

                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);
                DateTime primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
                DateTime ultimoDiaDoMes = primeiroDiaDoProximoMes.AddDays(-1);

                foreach (DataRow row in despesasTable.Rows)
                {
                    DateTime dataDespesa = Convert.ToDateTime(row["DATA"]);

                    if (dataDespesa >= primeiroDiaDoMes && dataDespesa <= ultimoDiaDoMes)
                    {
                        float valorDespesa = Convert.ToSingle(row["VALOR"]);
                        despesasMensal += valorDespesa;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular as despesas mensais: " + ex.Message);
            }

            return despesasMensal;
        }

        //Retorna a porcentagem de variação entre o mês atual e o mês anterior (despesas)
        public static float CalcularPorcentagemVariacaoDespesasMensal(int userId)
        {
            float despesasMensalAtual = GetDespesasMensal(userId);
            float despesasMensalAnterior = GetDespesasMensalMesAnterior(userId);

            if (despesasMensalAnterior == 0 || despesasMensalAtual == 0)
            {
                return 0;
            }
            else
            {
                float variacaoPercentual = ((despesasMensalAtual - despesasMensalAnterior) / despesasMensalAnterior) * 100;
                return variacaoPercentual;
            }
        }


        // Retorna a soma de todas as despesas no mês anterior
        public static float GetDespesasMensalMesAnterior(int userId)
        {
            float despesasMensal = 0;

            try
            {
                DataTable despesasTable = GetDespesas(userId);

                DateTime hoje = DateTime.Today;
                DateTime primeiroDiaDoMesAtual = new DateTime(hoje.Year, hoje.Month, 1);
                DateTime primeiroDiaDoMesAnterior = primeiroDiaDoMesAtual.AddMonths(-1);
                DateTime ultimoDiaDoMesAnterior = primeiroDiaDoMesAtual.AddDays(-1);

                foreach (DataRow row in despesasTable.Rows)
                {
                    DateTime dataDespesa = Convert.ToDateTime(row["DATA"]);

                    if (dataDespesa >= primeiroDiaDoMesAnterior && dataDespesa <= ultimoDiaDoMesAnterior)
                    {
                        float valorDespesa = Convert.ToSingle(row["VALOR"]);
                        despesasMensal += valorDespesa;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular as despesas mensais do mês anterior: " + ex.Message);
            }

            return despesasMensal;
        }

        //Retorna a soma de todos os salários dos funcionários
        public static float GetDespesasFuncionario(int userId)
        {
            float num = 0;
            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT SUM(SALARIO) FROM FUNCIONARIOS WHERE USER_ID = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            num = Convert.ToSingle(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter as despesas dos funcionários: " + ex.Message);
            }
            return num;
        }

        //Retorna uma datatable com todas as tarefas presentes na database.
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


        // Retorna o número de tarefas concluídas na semana atual
        public static int GetNumeroTarefasConcluidasUltimaSemana(int userId)
        {
            int numeroTarefasConcluidas = 0;

            try
            {
                using (SqlConnection conexao = Connect())
                {
                    DateTime hoje = DateTime.Today;
                    int diasDesdeUltimoDomingo = (int)hoje.DayOfWeek;
                    DateTime ultimoDomingo = hoje.AddDays(-diasDesdeUltimoDomingo);

                    DateTime domingoAnterior = ultimoDomingo.AddDays(-7);

                    string consulta = @"
                        SELECT COUNT(*) FROM TAREFAS WHERE USER_ID = @userId AND CONCLUIDO = 1
                          AND DATA_CONCLUSAO >= @domingoAnterior";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@domingoAnterior", domingoAnterior);
                        numeroTarefasConcluidas = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o número de tarefas concluídas: " + ex.Message);
            }

            return numeroTarefasConcluidas;
        }


        // Retorna uma datatable com todos os funcionários presentes na database
        public static DataTable GetFuncionarios(int userId)
        {
            DataTable funcionariosTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT f.ID, f.NOME, f.SALARIO, C.NOME FROM FUNCIONARIOS f INNER JOIN CARGOS c ON f.ID_CARGO = c.ID WHERE f.USER_ID = @userId ORDER BY f.NOME";
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


        // Altera o estado de conclusão de uma tarefa para "CONCLUIDA"
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
                    string query2 = "UPDATE TAREFAS SET DATA_CONCLUSAO = @dataAtual WHERE ID = @idTarefa";
                    using (SqlCommand cmd = new SqlCommand(query2, connection))
                    {
                        cmd.Parameters.AddWithValue("@dataAtual", DateTime.Now);
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


        // Adiciona uma tarefa na database
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


        // Adiciona um novo utilizador na database
        public static bool AdicionarUtilizador(string username, string password, bool isAdmin)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    if (GetIdByUsername(username) != -1)
                    {
                        MessageBox.Show("O nome de utilizador já existe. Por favor, escolha outro nome de utilizador.");
                        return false;
                    }

                    string query = "INSERT INTO UTILIZADORES VALUES (@username, @password, 28, @isAdmin)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@isAdmin", isAdmin);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar o utilizador: " + ex.Message);
                return false;
            }
        }


        // Adiciona um novo fornecedor na database
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


        // Adiciona uma nova categoria de produtos na database
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


        // Adiciona um novo cargo
        public static bool AddCargo(int userID, string nome)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "INSERT INTO CARGOS (USER_ID, NOME) VALUES (@userID, @nome)";
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
                MessageBox.Show("Erro ao adicionar o cargo: " + ex.Message);
                return false;
            }
        }


        // Retorna uma datatable com todos os cargos presentes na database
        public static DataTable GetCargos(int userId)
        {
            DataTable categoriasTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, NOME FROM CARGOS WHERE USER_ID = @userId ORDER BY NOME";
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


        // Retona uma datatable com todas as categorias presentes na database
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


        // Retorna uma datatable com todos os produtos e quantidades presentes na database
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


        // Retorna todos os investimentos e estatísticas presentes na datatable
        public static DataTable GetInvestimentos(int userId)
        {
            DataTable investimentosTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"
                SELECT i.ID, i.DESCRICAO, t.TIPO, i.VALOR_INVESTIDO, h.VALOR_TOTAL
                FROM INVESTIMENTOS i
                INNER JOIN TIPOS_INVESTIMENTOS t ON i.TIPO_INVESTIMENTO = t.ID
                LEFT JOIN (
                    SELECT hi1.INVESTIMENTO_ID, hi1.VALOR_TOTAL
                    FROM HISTORICO_INVESTIMENTO hi1
                    WHERE hi1.DATA = (
                        SELECT MAX(hi2.DATA)
                        FROM HISTORICO_INVESTIMENTO hi2
                        WHERE hi2.INVESTIMENTO_ID = hi1.INVESTIMENTO_ID
                    )
                ) h ON i.ID = h.INVESTIMENTO_ID
                WHERE i.USER_ID = @userId
                ORDER BY i.TIPO_INVESTIMENTO";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(investimentosTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os investimentos: " + ex.Message);
            }

            return investimentosTable;
        }


        // Rrtorna o investimento com maior taxa de variação positiva
        public static string GetMelhorInvestimento(int userId)
        {
            string melhorInvestimento = string.Empty;

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"
                SELECT TOP 1 i.DESCRICAO, (h.VALOR_TOTAL - i.VALOR_INVESTIDO) / i.VALOR_INVESTIDO AS MargemRetorno
                FROM INVESTIMENTOS i
                INNER JOIN TIPOS_INVESTIMENTOS t ON i.TIPO_INVESTIMENTO = t.ID
                LEFT JOIN (
                    SELECT hi1.INVESTIMENTO_ID, hi1.VALOR_TOTAL
                    FROM HISTORICO_INVESTIMENTO hi1
                    WHERE hi1.DATA = (
                        SELECT MAX(hi2.DATA)
                        FROM HISTORICO_INVESTIMENTO hi2
                        WHERE hi2.INVESTIMENTO_ID = hi1.INVESTIMENTO_ID
                    )
                ) h ON i.ID = h.INVESTIMENTO_ID
                WHERE i.USER_ID = @userId
                ORDER BY MargemRetorno DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                melhorInvestimento = reader["DESCRICAO"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o melhor investimento: " + ex.Message);
            }

            return melhorInvestimento;
        }


        // Retorna o investimento com maior taxa de variação negativa
        public static string GetPiorInvestimento(int userId)
        {
            string piorInvestimento = string.Empty;

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"
                SELECT TOP 1 i.DESCRICAO, (h.VALOR_TOTAL - i.VALOR_INVESTIDO) / i.VALOR_INVESTIDO AS MargemRetorno
                FROM INVESTIMENTOS i
                INNER JOIN TIPOS_INVESTIMENTOS t ON i.TIPO_INVESTIMENTO = t.ID
                LEFT JOIN (
                    SELECT hi1.INVESTIMENTO_ID, hi1.VALOR_TOTAL
                    FROM HISTORICO_INVESTIMENTO hi1
                    WHERE hi1.DATA = (
                        SELECT MAX(hi2.DATA)
                        FROM HISTORICO_INVESTIMENTO hi2
                        WHERE hi2.INVESTIMENTO_ID = hi1.INVESTIMENTO_ID
                    )
                ) h ON i.ID = h.INVESTIMENTO_ID
                WHERE i.USER_ID = @userId
                ORDER BY MargemRetorno ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                piorInvestimento = reader["DESCRICAO"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o pior investimento: " + ex.Message);
            }

            return piorInvestimento;
        }

        // Retorna o valor total dos investimentos de um utilizador
        public static float GetInvestimentosValorTotal(int userId)
        {
            DataTable investimentos = GetInvestimentos(userId);
            float totalValor = 0;

            foreach (DataRow row in investimentos.Rows)
            {
                if (row["VALOR_TOTAL"] != DBNull.Value)
                {
                    totalValor += Convert.ToSingle(row["VALOR_TOTAL"]);
                }
            }

            return totalValor;
        }

        public static float GetInvestimentosValorInicial(int userId)
        {
            DataTable investimentos = GetInvestimentos(userId);
            float totalValorInvestido = 0;

            foreach (DataRow row in investimentos.Rows)
            {
                if (row["VALOR_INVESTIDO"] != DBNull.Value)
                {
                    totalValorInvestido += Convert.ToSingle(row["VALOR_INVESTIDO"]);
                }
            }

            return totalValorInvestido;
        }

        public static DataTable GetTiposInvestimentos()
        {
            DataTable tiposInvestimentosTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT ID, TIPO FROM TIPOS_INVESTIMENTOS ORDER BY TIPO";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(tiposInvestimentosTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter os tipos de investimentos: " + ex.Message);
            }

            return tiposInvestimentosTable;
        }

        public static DataTable GetLowEstoque(int userId)
        {
            DataTable estoqueTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"SELECT PRODUTO FROM ESTOQUE WHERE QUANTIDADE <= 5 AND USER_ID = @userId";

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

        public static bool AddInvestimento(int userId, int idTipo, string descricao, decimal valorInvestido, decimal valorTotal)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "INSERT INTO INVESTIMENTOS (USER_ID, TIPO_INVESTIMENTO, DESCRICAO, VALOR_INVESTIDO, DATA) OUTPUT INSERTED.ID VALUES (@userId, @idTipo, @descricao, @valorInvestido, @dataAtual)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@idTipo", idTipo);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.Parameters.AddWithValue("@valorInvestido", valorInvestido);
                        cmd.Parameters.AddWithValue("@dataAtual", DateTime.Now);

                        int newInvestmentId = (int)cmd.ExecuteScalar();

                        string historicoQuery = "INSERT INTO HISTORICO_INVESTIMENTO (INVESTIMENTO_ID, VALOR_TOTAL, DATA) VALUES (@investimentoId, @valorTotal, @dataAtual)";
                        using (SqlCommand historicoCmd = new SqlCommand(historicoQuery, connection))
                        {
                            historicoCmd.Parameters.AddWithValue("@investimentoId", newInvestmentId);
                            historicoCmd.Parameters.AddWithValue("@valorTotal", valorTotal);
                            historicoCmd.Parameters.AddWithValue("@dataAtual", DateTime.Now);

                            historicoCmd.ExecuteNonQuery();
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar o investimento: " + ex.Message);
                return false;
            }
        }

        public class Definicoes
        {
            public int diaFuncionario;
        }


        // Retorna as definições definidas pelo utilizador
        public static Definicoes GetDefinicoes(int userid)
        {
            DataTable definicoes = new DataTable();
            Definicoes def = new Definicoes();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "SELECT * FROM UTILIZADORES WHERE ID = @userid";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(definicoes);
                        }
                    }
                }
                foreach (DataRow row in definicoes.Rows)
                {
                    def.diaFuncionario = int.Parse(row["DIA_FUNCIONARIO"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o histórico dos investimentos: " + ex.Message);
            }

            return def;
        }


        // Altera o cargo de um utilizador para Admin ou não
        public static bool AlterarAdmin(int userId, bool isAdmin)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    // Atualizar o estado do utilizador
                    string queryUpdate = "UPDATE UTILIZADORES SET ADMIN = @isAdmin WHERE ID = @userId";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, connection))
                    {
                        cmdUpdate.Parameters.AddWithValue("@isAdmin", isAdmin);
                        cmdUpdate.Parameters.AddWithValue("@userId", userId);

                        cmdUpdate.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar o estado do utilizador: " + ex.Message);
                return false;
            }
        }

        // Altera o dia de pagamento dos funcionários
        public static void AlterarDiaFuncionario(int userId, int diaFuncionario)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = "UPDATE UTILIZADORES SET DIA_FUNCIONARIO = @dia WHERE ID = @userid";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", userId);
                        cmd.Parameters.AddWithValue("@dia", diaFuncionario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o histórico dos investimentos: " + ex.Message);
            }
        }


        // Retorna uma datatable com o histórico dos investimentos
        public static DataTable GetHistoricoInvestimentos(int userId)
        {
            DataTable historicoTable = new DataTable();

            try
            {
                using (SqlConnection connection = Connect())
                {
                    string query = @"
            SELECT i.ID AS InvestimentoID, i.DESCRICAO, hi.DATA, hi.VALOR_TOTAL
            FROM INVESTIMENTOS i
            INNER JOIN HISTORICO_INVESTIMENTO hi ON i.ID = hi.INVESTIMENTO_ID
            WHERE i.USER_ID = @userId
            ORDER BY i.ID, hi.DATA";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(historicoTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter o histórico dos investimentos: " + ex.Message);
            }

            return historicoTable;
        }


        // Retorna uma datatable com todos os fornecedores
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


        // Retorna o numéro de categorias
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


        // Retorna o número de cargos presentes na database
        public static int GetNumeroCargos(int userId)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return -1;
                    }

                    string query = "SELECT COUNT(*) FROM CARGOS WHERE USER_ID = @userId";
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


        //Remove um ganho
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


        // Remove uma despesa
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

        // Remove um funcionário da database
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

        // Remove um fornecedor da database
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

        //Remove um utilizador da database
        public static bool RemoverUtilizador(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM UTILIZADORES WHERE ID = @id";
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
                MessageBox.Show("Erro ao remover o utilizador: " + ex.Message);
                return false;
            }
        }

        //Remove um produto da database
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


        // Remove uma categoria da database
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


        // Remove um cargo da database
        public static bool RemoverCargo(int id)
        {
            DialogResult result = MessageBox.Show("Remover este cargo também removerá todos os funcionários que pertencem à este cargo. Deseja continuar?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

                        string query = "DELETE FROM CARGOS WHERE ID = @id";
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
                    MessageBox.Show("Erro ao remover o cargo: " + ex.Message);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        // Remove um investimento da database
        public static bool RemoverInvestimento(int id)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    string query = "DELETE FROM INVESTIMENTOS WHERE ID = @id";
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
                MessageBox.Show("Erro ao remover o investimento: " + ex.Message);
                return false;
            }
        }


        // Edita um ganho da database
        public static bool EditGanho(int id, string desc, string valor)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(desc) || desc.Length > 100)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

            float valorFloat;
            try
            {
                valorFloat = float.Parse(valor);
                if (valorFloat < 0)
                {
                    MessageBox.Show("Valor deve ser maior ou igual a zero.");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Valor inválido. Deve ser um número.");
                return false;
            }

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
                        cmd.Parameters.AddWithValue("@valor", valorFloat); // Ensure parameter type matches database schema
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


        // Edita os investimentos da database
        public static bool EditInvestimento(int id, int tipoInvestimento, string descricao, string valorTotal)
        {
            if (id <= 0 || tipoInvestimento <= 0 || string.IsNullOrWhiteSpace(descricao) || descricao.Length > 100)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

            float valorTotalFloat;
            try
            {
                valorTotalFloat = float.Parse(valorTotal);
                if (valorTotalFloat < 0)
                {
                    MessageBox.Show("Valor total deve ser maior ou igual a zero.");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Valor total inválido. Deve ser um número.");
                return false;
            }

            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "UPDATE INVESTIMENTOS SET TIPO_INVESTIMENTO = @tipoInvestimento, DESCRICAO = @descricao WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@tipoInvestimento", tipoInvestimento);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.ExecuteNonQuery();
                    }

                    string historicoQuery = "INSERT INTO HISTORICO_INVESTIMENTO (INVESTIMENTO_ID, VALOR_TOTAL, DATA) VALUES (@investimentoId, @valorTotal, @data)";
                    using (SqlCommand historicoCmd = new SqlCommand(historicoQuery, connection))
                    {
                        historicoCmd.Parameters.AddWithValue("@investimentoId", id);
                        historicoCmd.Parameters.AddWithValue("@valorTotal", valorTotalFloat); // Ensure parameter type matches database schema
                        historicoCmd.Parameters.AddWithValue("@data", DateTime.Now);
                        historicoCmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o investimento: " + ex.Message);
                return false;
            }
        }

        // Edita um funcionário
        public static bool EditFuncionario(int id, string nome, string salario, string cargo)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(nome) || nome.Length > 100 || string.IsNullOrWhiteSpace(cargo) || cargo.Length > 50)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

            float salarioFloat;
            try
            {
                salarioFloat = float.Parse(salario);
                if (salarioFloat < 0)
                {
                    MessageBox.Show("Salário deve ser maior ou igual a zero.");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Salário inválido. Deve ser um número.");
                return false;
            }

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
                        cmd.Parameters.AddWithValue("@salario", salarioFloat); // Ensure parameter type matches database schema
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

        //Edita um fornecedor
        public static bool EditFornecedor(int id, string nome, string email, string telefone, int categoria)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(nome) || nome.Length > 100 ||
                string.IsNullOrWhiteSpace(email) || !IsValidEmail(email) ||
                string.IsNullOrWhiteSpace(telefone) || telefone.Length > 9 || !IsAllDigits(telefone) || categoria <= 0)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

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

        //Verifica se um email é válido
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }


        //Edita um produto na database
        public static bool EditProduto(int id, string produto, string quantidade, int idCategoria)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(produto) || produto.Length > 100 || idCategoria <= 0)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

            int quantidadeInt;
            try
            {
                quantidadeInt = int.Parse(quantidade);
                if (quantidadeInt < 0)
                {
                    MessageBox.Show("Quantidade deve ser maior ou igual a zero.");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Quantidade inválida. Deve ser um número inteiro.");
                return false;
            }

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
                        cmd.Parameters.AddWithValue("@quantidade", quantidadeInt); 
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


        //Edita uma despesa na database
        public static bool EditDespesa(int id, string desc, string valor)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(desc) || desc.Length > 100)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

            float valorFloat;
            try
            {
                valorFloat = float.Parse(valor);
                if (valorFloat < 0)
                {
                    MessageBox.Show("Valor deve ser maior ou igual a zero.");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Valor inválido. Deve ser um número.");
                return false;
            }

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
                        cmd.Parameters.AddWithValue("@valor", valorFloat); // Ensure parameter type matches database schema
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


        //Edita uma categoria na database
        public static bool EditCategoria(int id, string nome)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
            {
                MessageBox.Show("Dados de entrada inválidos.");
                return false;
            }

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
