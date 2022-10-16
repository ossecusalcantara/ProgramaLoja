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


namespace ProjetoIntegrador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void funcionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_funcionarios funcionarios = new Frm_funcionarios();
            funcionarios.MdiParent = this;
            funcionarios.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void funçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Funcoes f = new Frm_Funcoes();
            f.MdiParent = this;
            f.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Clientes c = new Frm_Clientes();
            c.MdiParent = this;
            c.Show();
        }


        private void cadastroDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Produtos p = new Frm_Produtos();
            p.MdiParent = this;
            p.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_BuscaProduto b = new Frm_BuscaProduto();
            b.MdiParent = this;
            b.Show();
        }

        private void caixaLivreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Vendas v = new Frm_Vendas();
            v.MdiParent = this;
            v.Show();
        }
    }
}
