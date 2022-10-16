using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.Collections;

namespace ProjetoIntegrador
{
    class FuncionarioDAO
    {
        public static object DataTime { get; private set; }

        private FuncionarioDAO() { }
        public static void salvar(Funcionarios f) {
            SqlConnection conn = Conexao.conecta();

            String s = " Insert into funcionarios (nome, cpf, telefone, dtnascimento, dtadmissao, idfuncao ) values ( @nome, @cpf ,@telefone , @dtadmissao , @dtnascimento, @idfuncao )";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = f.nome;
            cmd.Parameters.Add("@cpf", SqlDbType.VarChar, 50).Value = f.cpf;
            cmd.Parameters.Add("@telefone", SqlDbType.VarChar, 50).Value = f.telefone;
            cmd.Parameters.Add("@dtadmissao", SqlDbType.DateTime).Value = f.dtadmissao;
            cmd.Parameters.Add("@dtnascimento", SqlDbType.DateTime).Value = f.dtnascimento;
            cmd.Parameters.Add("@idfuncao", SqlDbType.Int).Value = f.idfuncao;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static void editar(Funcionarios f) {
            SqlConnection conn = Conexao.conecta();

            String s = " UPDATE funcionarios SET nome = @nome,idfuncao = @idfuncao ,cpf = @cpf ,telefone = @telefone , dtadmissao = @dtadmissao ,dtnascimento = @dtnascimento WHERE id = @id ";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = f.nome;
            cmd.Parameters.Add("@idfuncao", SqlDbType.Int).Value = f.idfuncao;
            cmd.Parameters.Add("@cpf", SqlDbType.VarChar, 50).Value = f.cpf;
            cmd.Parameters.Add("@telefone", SqlDbType.VarChar, 50).Value = f.telefone;
            cmd.Parameters.Add("@dtadmissao", SqlDbType.DateTime).Value = f.dtadmissao;
            cmd.Parameters.Add("@dtnascimento", SqlDbType.DateTime).Value = f.dtnascimento;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = f.id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public static void excluir(Funcionarios f) {
            excluir(f.id);
        }
        public static void excluir(int id) {
            SqlConnection conn = Conexao.conecta();

            String s = " DELETE  FROM funcionarios WHERE id = @id";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public static String MontaQueryBuscaFuncionario(int id, String nome) {
            if (id <= 0 && nome.Trim() == "")
            {
                return "SELECT * FROM funcionarios";
            }
            else
            {
                if (id > 0 && nome.Trim() != "")
                {
                    return "SELECT * FROM funcionarios WHERE id = " + id + " AND nome LIKE '%" + nome + "%'";
                }
                else
                {
                    if (id > 0)
                    {
                        return "SELECT * FROM funcionarios  WHERE id = " + id;
                    }
                    return "SELECT * FROM funcionarios WHERE nome LIKE '% " + nome + "%'";
                }
            }
        }
        public static void BuscaFuncionario(BindingSource bindingSource1, DataGridView dataGridView1, string sql) {
            SqlConnection conn = Conexao.conecta();
            //String sql = "select * from jogos";
            if (sql.Trim() == "")
            {
                sql = "select f.id, f.nome, g.nome as genero,  f.cindicativa, f.ano," +
                    " j.status  from jogos j INNER JOIN generos g ON f.genero = g.id ";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

        }

        public static ArrayList GetNomesFuncionario() {
            ArrayList arr = new ArrayList();
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT nome FROM funcionarios";
            SqlCommand command = new SqlCommand(sql, conn);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                arr.Add(reader[0]);
            }
            reader.Close();
            command.Dispose();
            return arr;
        }

        public static Funcionarios GetFuncionarioByNome(String nome)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM funcionarios WHERE nome = @nome";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = nome;
            command.Prepare();


            SqlDataReader reader = command.ExecuteReader();
            Funcionarios f = new Funcionarios();
            if (reader.Read())
            {
                f.id = Int32.Parse(reader[0].ToString());
                f.nome = reader[1].ToString();
                f.cpf = reader[2].ToString();
                f.telefone = reader[3].ToString();
                f.dtadmissao = DateTime.Parse(reader[4].ToString());
                f.dtnascimento = DateTime.Parse(reader[5].ToString());
                f.idfuncao = Int32.Parse(reader[6].ToString());
            }
            reader.Close();
            command.Dispose();
            return f;
        }
        public static Funcionarios GetFuncionarioById(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM funcionarios WHERE id = @id";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Prepare();


            SqlDataReader reader = command.ExecuteReader();
            Funcionarios f = new Funcionarios();
            if (reader.Read())
            {
                f.id = Int32.Parse(reader[0].ToString());
                f.nome = reader[1].ToString();
                f.cpf = reader[2].ToString();
                f.telefone = reader[3].ToString();
                f.dtadmissao = DateTime.Parse(reader[4].ToString());
                f.dtnascimento = DateTime.Parse(reader[5].ToString());
                f.idfuncao = Int32.Parse(reader[6].ToString());
            }
            reader.Close();
            command.Dispose();
            return f;
        }

        public static String GetNomeFuncionarioById(int id)
        {
            Funcionarios f = FuncionarioDAO.GetFuncionarioById(id);
            return f.nome;
        }

        public static int GetIdFuncionarioByName(String s)
        {
            Funcionarios f = FuncionarioDAO.GetFuncionarioByNome(s);
            return f.id;
        }
    }
}
