using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace ProjetoIntegrador
{
    public partial class Frm_Funcoes : Form
    {
        public Frm_Funcoes()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Funcao f = GetFuncaotela();
            if (validaform())
            {
                FuncaoDAO.salvar(f);
                MessageBox.Show("Função cadastrada com sucesso!");
                LimpaTela();
                AtualizaCombo();
            }
            else
            {
                MessageBox.Show(" Nescessário peencher todos os campos !!!  ");
            }

        }

        private Funcao GetFuncaotela()
        {
            Funcao f = new Funcao();
            f.funcao = txtFuncao.Text;
            f.salario = Decimal.Parse(txtSalario.Text);
            return f;
        }

        private bool validaform()
        {
            if (txtFuncao.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        private void LimpaTela()
        {
            txtFuncao.Text = "";
            txtSalario.Text = "";
        }

        private void AtualizaCombo()
        {
            cbBuscaFuncao.Items.Clear();
            cbBuscaFuncao.Text = "";
            txtBuscaid.Text = "";
            ArrayList lista = FuncaoDAO.GetNomesFuncao();
            foreach (String s in lista)
            {
                cbBuscaFuncao.Items.Add(s);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Nenhuma Funcao foi selecionada !!!");
            }
            else
            {
                Funcao f = GetFuncaotela();
                f.id = Int32.Parse(txtBuscaid.Text);
                if (validaform())
                {
                    FuncaoDAO.editar(f);
                    LimpaTela();
                    AtualizaCombo();
                    MessageBox.Show("Função editado com sucesso !!!");
                }
                else
                {
                    MessageBox.Show("Campo ID em branco !!!");
                }
            }
        }

        private void Frm_Funcoes_Load(object sender, EventArgs e)
        {
            AtualizaCombo();
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            if (cbBuscaFuncao.Text.Trim() == "")
            {
                return;
            }
            Funcao f = FuncaoDAO.GetFuncaoByNome(cbBuscaFuncao.Text);
            txtBuscaid.Text = f.id.ToString();
            txtFuncao.Text = f.funcao;
            txtSalario.Text = f.salario.ToString();
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
                FuncaoDAO.excluir(id);
                LimpaTela();
                AtualizaCombo();
            }
        }

        private void btnlimpatela_Click(object sender, EventArgs e)
        {
            LimpaTela();
        }
    }
}
