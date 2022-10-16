using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime;

namespace ProjetoIntegrador
{
    public partial class Frm_funcionarios : Form
    {
        public Frm_funcionarios()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Funcionarios f = GetFuncionariotela();
            if (validaform())
            {
                FuncionarioDAO.salvar(f);
                MessageBox.Show("Funcionário cadastrado com sucesso!");
                LimpaTela();
                AtualizaCombo();
                AtualizaComboGenero();
            }
            else
            {
                MessageBox.Show(" em branco!");
            }
        }
        private Funcionarios GetFuncionariotela()
        {
            Funcionarios f = new Funcionarios();
            f.nome = txtNome.Text;
            f.idfuncao = FuncaoDAO.GetIdByName(cbFuncao.Text);
            f.cpf = txtcpf.Text;
            f.telefone = txtTelefone.Text;
            f.dtadmissao = dtimeAdmissao.Value.Date;
            f.dtnascimento = dtimeNascimento.Value.Date;
            return f;
        }
        private void LimpaTela()
        {
            txtNome.Text = "";
            cbFuncao.Text = "";
            txtcpf.Text = "";
            txtTelefone.Text = "";
            dtimeAdmissao.Value = DateTime.Now;
            dtimeNascimento.Value = DateTime.Now;

        }
        private bool validaform()
        {
            if (txtNome.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            if (cbBuscaNome.Text.Trim() == "")
            {
                return;
            }
            Funcionarios f = FuncionarioDAO.GetFuncionarioByNome(cbBuscaNome.Text);
            txtBuscaid.Text = f.id.ToString();
            txtNome.Text = f.nome;
            cbFuncao.SelectedItem = FuncaoDAO.GetNomeById(f.idfuncao);
            txtcpf.Text = f.cpf;
            txtTelefone.Text = f.telefone;
            dtimeAdmissao.Value = f.dtadmissao;
            dtimeNascimento.Value = f.dtnascimento;

        }
        private void AtualizaCombo()
        {
            cbBuscaNome.Items.Clear();
            cbBuscaNome.Text = "";
            txtBuscaid.Text = "";
            ArrayList lista = FuncionarioDAO.GetNomesFuncionario();
            foreach (String s in lista)
            {
                cbBuscaNome.Items.Add(s);
            }
        }
        private void AtualizaComboGenero()
        {
            cbFuncao.Items.Clear();
            cbFuncao.Text = "";
            ArrayList lista = FuncaoDAO.GetNomesFuncao();
            foreach (String s in lista)
            {
                cbFuncao.Items.Add(s);
            }
        }

        private void Frm_funcionarios_Load(object sender, EventArgs e)
        {
            AtualizaCombo();
            AtualizaComboGenero();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Nenhum Funcionário foi selecionado !!!");
            }
            else
            {
                Funcionarios f = GetFuncionariotela();
                f.id = Int32.Parse(txtBuscaid.Text);
                if (validaform())
                {
                    FuncionarioDAO.editar(f);
                    LimpaTela();
                    AtualizaCombo();
                    AtualizaComboGenero();
                    MessageBox.Show("Perfil do funcionário editado com sucesso !!!");
                }
                else
                {
                    MessageBox.Show("Perfil do funcionário em branco !!!");
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Não existe Nenhuma Função Selecionado.");
                return;
            }
            var result = MessageBox.Show("Confirmar Exlcusão?", "legenda",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //////////////
                int id = Int32.Parse(txtBuscaid.Text);
                FuncionarioDAO.excluir(id);
                LimpaTela();
                AtualizaCombo();
                AtualizaComboGenero();
            }
        }

        private void btnlimpatela_Click(object sender, EventArgs e)
        {
            LimpaTela();
        }
    }
}
