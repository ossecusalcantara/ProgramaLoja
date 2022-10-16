using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrador
{
    class Funcao
    {
        public int id;
        public String funcao;
        public Decimal salario;


        public Funcao() { }

        public Funcao(int id, String funcao, Decimal salario) {
            this.id = id;
            this.funcao = funcao;
            this.salario = salario;
        }
    }
}
