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
    class ClientesDAO {
        public static void salvar(Clientes c)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " Insert into clientes values ( @nome, @cpf, @dtnascimento, @telefone )";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = c.nome;
            cmd.Parameters.Add("@cpf", SqlDbType.VarChar, 50).Value = c.cpf;
            cmd.Parameters.Add("@dtnascimento", SqlDbType.DateTime).Value = c.dtnascimento;
            cmd.Parameters.Add("@telefone", SqlDbType.VarChar, 50).Value = c.telefone;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void editar(Clientes c)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " UPDATE clientes SET nome = @nome, cpf = @cpf, dtnascimento = @dtnascimento, telefone = @telefone WHERE id = @id ";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = c.nome;
            cmd.Parameters.Add("@cpf", SqlDbType.VarChar, 50).Value = c.cpf;
            cmd.Parameters.Add("@dtnascimento", SqlDbType.DateTime).Value = c.dtnascimento;
            cmd.Parameters.Add("@telefone", SqlDbType.VarChar, 50).Value = c.telefone;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = c.id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public static void excluir(Funcao f)
        {
            excluir(f.id);
        }

        public static void excluir(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "DELETE FROM clientes WHERE id = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public static ArrayList GetNomesClientes()
        {
            ArrayList arr = new ArrayList();
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT nome FROM clientes";
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


        public static Clientes GetClientesByNome(String nome)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM clientes WHERE nome = @nome";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = nome;
            command.Prepare();


            SqlDataReader reader = command.ExecuteReader();
            Clientes c = new Clientes();
            if (reader.Read())
            {
                c.id = Int32.Parse(reader[0].ToString());
                c.nome = reader[1].ToString();
                c.cpf = reader[2].ToString();
                c.dtnascimento = DateTime.Parse(reader[3].ToString());
                c.telefone = reader[4].ToString();

            }
            reader.Close();
            command.Dispose();
            return c;
        }
        public static int GetIdByName(String s)
        {
            Clientes c = ClientesDAO.GetClientesByNome(s);
            return c.id;
        }

        public static Clientes GetClientesById(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM clientes WHERE id = @id";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Prepare();

            SqlDataReader reader = command.ExecuteReader();
            Clientes c = new Clientes();
            if (reader.Read())
            {
                c.id = Int32.Parse(reader[0].ToString());
                c.nome = reader[1].ToString();
                c.cpf = reader[2].ToString();
                c.dtnascimento = DateTime.Parse(reader[3].ToString());
                c.telefone = reader[4].ToString();
            }
            reader.Close();
            command.Dispose();
            return c;
        }

        public static String GetNomeById(int i)
        {
            Clientes c = ClientesDAO.GetClientesById(i);
            return c.nome;
        }

        public static String GetCpfClienteById(int i)
        {
            Clientes c = ClientesDAO.GetClientesById(i);
            return c.cpf;
        }
    }
}
