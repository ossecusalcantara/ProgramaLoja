using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoIntegrador
{
    public partial class Frm_BuscaProduto : Form
    {
        public Frm_BuscaProduto()
        {
            InitializeComponent();
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (txtBuscaid.Text.Trim() != "")
            {
                id = Int32.Parse(txtBuscaid.Text);
            }
            String sql = ProdutoDAO.MontaQueryBuscaProdutos(id, txtNome.Text);
            ProdutoDAO.BuscaProdutos(bindingSource1, dataGridView1, sql);
        }

        private void Frm_BuscaProduto_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            ProdutoDAO.BuscaProdutos(bindingSource1, dataGridView1, "");
        }
    }
}
