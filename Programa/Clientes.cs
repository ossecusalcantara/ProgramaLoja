using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrador
{
    class Clientes
    {
        public int id;
        public String nome;
        public String cpf;
        public DateTime dtnascimento;
        public String telefone;

        public Clientes() { }

        public Clientes(int id, String nome, String cpf, DateTime dtnascimento, String telefone)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.dtnascimento = dtnascimento;
            this.telefone = telefone;

        }
    }
}
