using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoIntegrador
{
    class Conexao
    {
        private const String host = "CRE107D10";
        private const String banco = "loja";
        private const String user = "sa";
        private const String pass = "Senac@12345";
        private static SqlConnection conn = null;

        private Conexao() { }

        public static SqlConnection conecta()
        {
            if (conn == null)
            {
                String connectionString = "Data Source=" + host + ";Initial Catalog=" + banco + ";User ID=" + user + ";Password=" + pass + ";";

                conn = new SqlConnection(connectionString);
                conn.Open();
            }
            return conn;
        }
        public static void fechaConexao()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
    }
}
