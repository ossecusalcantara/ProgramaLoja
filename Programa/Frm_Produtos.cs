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
    public partial class Frm_Produtos : Form
    {
        public Frm_Produtos()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Produtos p = GetProdutotela();
            if (validaform())
            {
                ProdutoDAO.salvar(p);
                MessageBox.Show("Produto cadastrado com sucesso!");
                LimpaTela();
                AtualizaCombo();
            }
            else
            {
                MessageBox.Show(" Nescessário peencher todos os campos !!!  ");
            }
        }
        private Produtos GetProdutotela()
        {
            Produtos p = new Produtos();
            p.nome = txtNome.Text;
            p.valor = Decimal.Parse(txtValor.Text);
            p.quantidade = Int32.Parse(txtQtde.Text);
            return p;
        }
        private void LimpaTela()
        {
            txtNome.Text = "";
            cbBuscaNome.Text = "";
            txtQtde.Text = "";
            txtValor.Text = "";


        }
        private bool validaform()
        {
            if (txtNome.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }
        private void AtualizaCombo()
        {
            cbBuscaNome.Items.Clear();
            cbBuscaNome.Text = "";
            txtBuscaid.Text = "";
            ArrayList lista = ProdutoDAO.GetNomesProduto();
            foreach (String s in lista)
            {
                cbBuscaNome.Items.Add(s);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Nenhum Produto foi selecionada !!!");
            }
            else
            {
                Produtos p = GetProdutotela();
                p.id = Int32.Parse(txtBuscaid.Text);
                if (validaform())
                {
                    ProdutoDAO.editar(p);
                    LimpaTela();
                    AtualizaCombo();
                    MessageBox.Show("Produto editado com sucesso !!!");
                }
                else
                {
                    MessageBox.Show("Campo ID em branco !!!");
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Nenhuma Produto Selecionado.");
                return;
            }
            var result = MessageBox.Show("Confirmar Exlcusão?", "legenda",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //////////////
                int id = Int32.Parse(txtBuscaid.Text);
                ProdutoDAO.excluir(id);
                LimpaTela();
                AtualizaCombo();
            }
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            if (cbBuscaNome.Text.Trim() == "")
            {
                return;
            }
            Produtos p = ProdutoDAO.GetProdutoByNome(cbBuscaNome.Text);
            txtBuscaid.Text = p.id.ToString();
            txtNome.Text = p.nome.ToString();
            txtValor.Text = p.valor.ToString();
            txtQtde.Text = p.quantidade.ToString();
           
        }

        private void Frm_Produtos_Load(object sender, EventArgs e)
        {
            AtualizaCombo();
        }

        private void btnlimpatela_Click(object sender, EventArgs e)
        {
            LimpaTela();
        }
    }
}
