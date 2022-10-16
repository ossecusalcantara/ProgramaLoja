using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrador
{
    class Produtos {
        public int id;
        public String nome;
        public int quantidade;
        public Decimal valor;

        public Produtos() { }

        public Produtos (int id, int quantidade, String nome, Decimal valor) {

            this.id = id;
            this.nome = nome;
            this.quantidade = quantidade;
            this.valor = valor;
        }
    }
}
