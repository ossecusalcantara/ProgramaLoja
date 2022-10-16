using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrador
{
    class Vendas
    {
        public int id;
        public int idfuncionario;
        public int idcliente;
        public DateTime datahora;
        public Decimal total;
        public List<Produtos> listavenda;

        public Vendas() {
            listavenda = new List<Produtos>();
        }


    }
}
