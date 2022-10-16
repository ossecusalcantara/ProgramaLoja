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
    class FuncaoDAO
    {
        public static void salvar(Funcao f)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " Insert into funcao values ( @funcao, @salario )";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@funcao", SqlDbType.VarChar, 50).Value = f.funcao;
            SqlParameter parameter = new SqlParameter("@salario", SqlDbType.Decimal);
            parameter.Precision = 8;
            parameter.Scale = 2;
            cmd.Parameters.Add(parameter).Value = f.salario;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void editar(Funcao f)
        {
            SqlConnection conn = Conexao.conecta();

            String s = " UPDATE funcao SET cargo = @cargo, salario = @salario WHERE id = @id ";
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.Parameters.Add("@cargo", SqlDbType.VarChar, 50).Value = f.funcao;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = f.id;
            SqlParameter parameter = new SqlParameter("@salario", SqlDbType.Decimal);
            parameter.Precision = 8;
            parameter.Scale = 2;
            cmd.Parameters.Add(parameter).Value = f.salario;
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
            String sql = "DELETE FROM funcao WHERE id = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public static ArrayList GetNomesFuncao()
        {
            ArrayList arr = new ArrayList();
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT cargo FROM funcao";
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

        public static Funcao GetFuncaoByNome(String nome)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM funcao WHERE cargo = @cargo";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@cargo", SqlDbType.VarChar, 50).Value = nome;
            command.Prepare();


            SqlDataReader reader = command.ExecuteReader();
            Funcao f = new Funcao();
            if (reader.Read())
            {
                f.id = Int32.Parse(reader[0].ToString());
                f.funcao = reader[1].ToString();
                f.salario = Decimal.Parse(reader[2].ToString());
            }
            reader.Close();
            command.Dispose();
            return f;
        }
        public static int GetIdByName(String s)
        {
            Funcao f = FuncaoDAO.GetFuncaoByNome(s);
            return f.id;
        }

        public static Funcao GetFuncaoById(int id)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "SELECT * FROM funcao WHERE id = @id";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Prepare();

            SqlDataReader reader = command.ExecuteReader();
            Funcao f = new Funcao();
            if (reader.Read())
            {
                f.id = Int32.Parse(reader[0].ToString());
                f.funcao = reader[1].ToString();
                f.salario = Decimal.Parse(reader[2].ToString());
            }
            reader.Close();
            command.Dispose();
            return f;
        }

        public static String GetNomeById(int i)
        {
            Funcao f =FuncaoDAO.GetFuncaoById(i);
            return f.funcao;
        }
    }
}
