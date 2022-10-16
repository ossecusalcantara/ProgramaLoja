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
    public partial class Frm_ListaVenda : Form
    {
        public Frm_ListaVenda()
        {
            InitializeComponent();
        }

        private void Frm_ListaVenda_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            VendasDAO.BuscaVendas(bindingSource1, dataGridView1, "");
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (txtBuscaid.Text.Trim() != "")
            {
                id = Int32.Parse(txtBuscaid.Text);
            }
            String sql = VendasDAO.MontaQueryBuscaVendas(id);
            VendasDAO.BuscaVendas(bindingSource1, dataGridView1, sql);
        }
    }
}
