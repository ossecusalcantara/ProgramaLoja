using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace ProjetoIntegrador
{
    class VendasDAO
    {
        public static void salvar(Vendas v)
        {
            SqlConnection conn = Conexao.conecta();
            String sql = "INSERT INTO vendas (datahora, total, idcliente, idfuncionario) output INSERTED.ID " +
                "VALUES (@dt, @total, @idcliente, @idfuncionario)";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.Now;
            command.Parameters.Add("@idcliente", SqlDbType.Int).Value = v.idcliente;
            command.Parameters.Add("@idfuncionario", SqlDbType.Int).Value = v.idfuncionario;
            SqlParameter parameter = new SqlParameter("@total", SqlDbType.Decimal);
            parameter.Precision = 8;
            parameter.Scale = 2;
            command.Parameters.Add(parameter).Value = v.total;
            command.Prepare();

            int idVenda = (int)command.ExecuteScalar();
            command.Dispose();

            //ITEMS
            sql = "INSERT INTO itensvenda (idvenda, idproduto) VALUES (@vendaid ,@produtoid)";
            foreach (Produtos p in v.listavenda)
            {
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@vendaid", SqlDbType.Int).Value = idVenda;
                command.Parameters.Add("@produtoid", SqlDbType.Int).Value = p.id;
                command.Prepare();
                command.ExecuteNonQuery();
                command.Dispose();
            }
            return;
        }
        public static void BuscaVendas(BindingSource bindingSource1,
       DataGridView dataGridView1, String sql)
        {
            SqlConnection conn = Conexao.conecta();
            if (sql.Trim() == "")
            {
                sql = "select v.ID , v.datahora as Data, v.total as Total, c.nome as Cliente, f.nome as Funcionario" +
                        " from vendas v" +
                        " INNER JOIN clientes c ON v.idcliente = c.id" +
                        " INNER JOIN funcionarios f ON v.idfuncionario = f.id";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;
        }

        public static String MontaQueryBuscaVendas(int id)
        {
            if (id == 0)
            {
                return "select id as Id, datahora as Data, total as Total,  from vendas";
            }
            else
            {
                return "select id as Id, datahora as Data, total as Total from vendas" +
                    " WHERE id = " + id;
            }
        }
    }
}
