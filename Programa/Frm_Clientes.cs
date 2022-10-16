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
    public partial class Frm_Clientes : Form
    {
        public Frm_Clientes()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Clientes c = GetClientetela();
            if (validaform())
            {
                ClientesDAO.salvar(c);
                MessageBox.Show("Cliente cadastrada com sucesso!");
                LimpaTela();
                AtualizaCombo();
            }
            else
            {
                MessageBox.Show(" Nome do cliente em branco!");
            }
        }
        private Clientes GetClientetela()
        {
            Clientes c = new Clientes();
            c.nome = txtNome.Text;
            c.cpf = txtcpf.Text;
            c.dtnascimento = dtimeNascimento.Value.Date;
            c.telefone = txtTelefone.Text;
            return c;
        }

        private bool validaform()
        {
            if (txtNome.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        private void LimpaTela()
        {
            txtNome.Text = "";
            txtcpf.Text = "";
            txtTelefone.Text = "";
            dtimeNascimento.Value = DateTime.Now;
        }

        private void AtualizaCombo()
        {
            cbBuscaNome.Items.Clear();
            cbBuscaNome.Text = "";
            txtBuscaid.Text = "";
            ArrayList lista = ClientesDAO.GetNomesClientes();
            foreach (String s in lista)
            {
                cbBuscaNome.Items.Add(s);
            }
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            if (cbBuscaNome.Text.Trim() == "")
            {
                return;
            }
            Clientes c = ClientesDAO.GetClientesByNome(cbBuscaNome.Text);
            txtBuscaid.Text = c.id.ToString();
            txtNome.Text = c.nome;
            txtcpf.Text = c.cpf;
            dtimeNascimento.Value = c.dtnascimento;
            txtTelefone.Text = c.telefone;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show("Nenhum Cliente foi selecionado !!!");
            }
            else
            {
                Clientes c = GetClientetela();
                c.id = Int32.Parse(txtBuscaid.Text);
                if (validaform())
                {
                    ClientesDAO.editar(c);
                    LimpaTela();
                    AtualizaCombo();
                    MessageBox.Show("Perfil do Cliente editado com sucesso !!!");
                }
                else
                {
                    MessageBox.Show("Perfil do Cliente em branco !!!");
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtBuscaid.Text.Trim() == "")
            {
                MessageBox.Show(" Selecione o Perfil de um Cliente !!!");
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
            }
        }

        private void Frm_Clientes_Load(object sender, EventArgs e)
        {
            AtualizaCombo();
        }

        private void btnlimpatela_Click(object sender, EventArgs e)
        {
            LimpaTela();
        }
    }
}
