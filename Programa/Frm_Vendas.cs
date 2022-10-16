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
    public partial class Frm_Vendas : Form
    {
        private List<int> arrProdutos;
        public Frm_Vendas()
        {
            InitializeComponent();
            this.arrProdutos = new List<int>();
        }

        private void btnBuscarid_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Campo ID em branco.");
                return;
            }
            String s = ProdutoDAO.GetNomeProdutoById(Int32.Parse(txtBuscaid.Text));
            if (s == null)
            {
                MessageBox.Show("Produto não encontrado.");
                return;
            }
            txtNome.Text = s;
        }

        private void bntAdicionar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Não existe Produto selecionado.");
                return;
            }
            listBox1.Items.Add(txtNome.Text);
            arrProdutos.Add(Int32.Parse(txtBuscaid.Text));
            LimpaPesquisa();
        }

        public void LimpaPesquisa()
        {
            txtBuscaid.Text = "";
            txtNome.Text = "";
        }

        private void LimpaTela()
        {
            txtTotal.Text = "";
            txtBuscaid.Text = "";
            txtNome.Text = "";
            listBox1.Items.Clear();
            arrProdutos.Clear();
        }

        private void LimpaCliente()
        {
            txtIdCliente.Text = "";
            txtNomeCliente.Text = "";
            txtCpf.Text = "";
        }

        private bool ValidaForm()
        {
            if (txtTotal.Text.Trim() == "")
            {
                MessageBox.Show("Total vazio !!!");
                return false;
            }
            if (arrProdutos.Count() == 0)
            {
                MessageBox.Show("Nenhum Produto adicionado !!!");
                return false;
            }
            return true;
        }

        private Vendas GetVendaTela()
        {
            Vendas v = new Vendas();
            if (txtTotal.Text.Trim() == "")
            {
                return v;
            }
            v.total = Decimal.Parse(txtTotal.Text);
            v.idcliente = Int32.Parse(txtIdCliente.Text);
            v.idfuncionario = FuncionarioDAO.GetIdFuncionarioByName(cbFuncionario.Text);
            List<Produtos> temp = new List<Produtos>();
            foreach (int i in arrProdutos)
            {
                v.listavenda.Add(ProdutoDAO.GetProdutoById(i));
            }
            return v;
        }
        private void AtualizaCombo()
        {
            cbFuncionario.Items.Clear();
            cbFuncionario.Text = "";
            
            ArrayList lista = FuncionarioDAO.GetNomesFuncionario();
            foreach (String s in lista)
            {
                cbFuncionario.Items.Add(s);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Vendas v = GetVendaTela();
            if (ValidaForm())
            {
                VendasDAO.salvar(v);
                LimpaTela();
                MessageBox.Show("Venda Registrada com Sucesso !!!");
            }
            else
            {
                MessageBox.Show("Verifique os dados e tente novamente !!!");
            }
        }

        private void btnAdcCliente_Click(object sender, EventArgs e)
        {
            if (txtIdCliente.Text.Trim() == "")
            {
                MessageBox.Show("Campo ID em branco.");
                return;
            }
            String s = ClientesDAO.GetNomeById(Int32.Parse(txtIdCliente.Text));
            String c = ClientesDAO.GetCpfClienteById(Int32.Parse(txtIdCliente.Text));
            if (s == null)
            {
                MessageBox.Show("Cliente não encontrado.");
                return;
            }
            txtNomeCliente.Text = s;
            txtCpf.Text = c;
           
            if (txtIdCliente.Text == "")
            {
                MessageBox.Show("Não existe Cliente selecionado.");
                return;
            }
            
        }

        private void Frm_Vendas_Load(object sender, EventArgs e)
        {
            AtualizaCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpaTela();
            LimpaPesquisa();
            LimpaCliente();
        }
    }
}
