using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.Collections;

namespace ProjetoIntegrador
{
    class ProdutoDAO {
        public static void salvar(Produtos p)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " Insert into produtos values ( @nome, @valor, @quantidade )";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = p.nome;
            cmd.Parameters.Add("@quantidade", SqlDbType.Int).Value = p.quantidade;
            SqlParameter parameter = new SqlParameter("@valor", SqlDbType.Decimal);
            parameter.Precision = 8;
            parameter.Scale = 2;
            cmd.Parameters.Add(parameter).Value = p.valor;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void editar(Produtos p)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " UPDATE produtos SET nome = @nome, valor = @valor, quantidade = @quantidade WHERE id = @id ";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = p.nome;
            cmd.Parameters.Add("@quantidade", SqlDbType.Int).Value = p.quantidade;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = p.id;
            SqlParameter parameter = new SqlParameter("@valor", SqlDbType.Decimal);
            parameter.Precision = 8;
            parameter.Scale = 2;
            cmd.Parameters.Add(parameter).Value = p.valor;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public static void excluir(Produtos p)
        {
            excluir(p.id);
        }

        public static void excluir(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "DELETE FROM produtos WHERE id = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public static ArrayList GetNomesProduto()
        {
            ArrayList arr = new ArrayList();
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT nome FROM produtos";
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

        public static Produtos GetProdutoByNome(String nome)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM produtos WHERE nome = @nome";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = nome;
            command.Prepare();


            SqlDataReader reader = command.ExecuteReader();
            Produtos p = new Produtos();
            if (reader.Read())
            {
                p.id = Int32.Parse(reader[0].ToString());
                p.nome = reader[1].ToString();
                p.valor = Decimal.Parse(reader[2].ToString());
                p.quantidade = Int32.Parse(reader[3].ToString());
            }
            reader.Close();
            command.Dispose();
            return p;
        }
        public static int GetIdByName(String s)
        {
            Produtos p = ProdutoDAO.GetProdutoByNome(s);
            return p.id;
        }

        public static Produtos GetProdutoById(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM produtos WHERE id = @id";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Prepare();

            SqlDataReader reader = command.ExecuteReader();
            Produtos p = new Produtos();
            if (reader.Read())
            {
                p.id = Int32.Parse(reader[0].ToString());
                p.nome = reader[1].ToString();
                p.valor = Decimal.Parse(reader[2].ToString());
                p.quantidade = Int32.Parse(reader[3].ToString());
            }
            reader.Close();
            command.Dispose();
            return p;
        }

        public static String GetNomeProdutoById(int i)
        {
            Produtos p =  ProdutoDAO.GetProdutoById(i);
            return p.nome;
        }

        public static String MontaQueryBuscaProdutos(int id, String nome)
        {
            //id e nome vazios
            if (id <= 0 && nome.Trim() == "")
            {
                return "SELECT * FROM produtos";
            }
            else
            {
                //id e nome preenchido
                if (id > 0 && nome.Trim() != "")
                {
                    return "SELECT * FROM produtos WHERE id = " + id +
                        " AND nome LIKE '%" + nome + "%'";
                }
                else
                {
                    //apenas ID
                    if (id > 0)
                    {
                        return "SELECT * FROM produtos WHERE id = " + id;
                    }
                    //apenas Nome
                    return "SELECT * FROM produtos WHERE nome LIKE '%" + nome + "%'";
                }
            }
        }

        public static void BuscaProdutos(BindingSource bindingSource1,
        DataGridView dataGridView1, String sql)
        {
            SqlConnection conn = Conexao.conecta();
            if (sql.Trim() == "")
            {
                sql = "SELECT ID, Nome, Quantidade , Valor from produtos";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;
        }
        public static List<Produtos> GetProdutosByVenda(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT idproduto FROM itensvendas WHERE idvenda = @id";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            List<int> temp = new List<int>();
            while (reader.Read())
            {
                temp.Add(Int32.Parse(reader[0].ToString()));
            }
            reader.Close();
            command.Dispose();

            List<Produtos> lista = new List<Produtos>();
            foreach (int i in temp)
            {
                Produtos p = ProdutoDAO.GetProdutoById(i);
                lista.Add(p);
            }
            return lista;

        }
    }
}
