using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrador
{
    class Funcionarios {
        //Atributos
        public int id;
        public int idfuncao;
        public String nome;
        public String cpf;
        public String telefone;
        public DateTime dtadmissao;
        public DateTime dtnascimento;

        public Funcionarios() { }

        public Funcionarios (int id, int idfuncao, String nome, String cpf, String telefone,
        DateTime dtadmissao, DateTime dtnascimento) {
            this.id = id;
            this.idfuncao = idfuncao;
            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.dtadmissao = dtadmissao;
            this.dtnascimento = dtnascimento;
        }
    }
}
