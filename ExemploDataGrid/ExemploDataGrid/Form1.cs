using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExemploDataGrid
{

    public partial class Form1 : Form
    {
        public DataGridView gridLista;

        public List<Item> lista = new List<Item>();

        public Form1()
        {
            InitializeComponent();
            LerTexto();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            gridLista.Rows.Clear();
            CbAlterar.Items.Clear();
            int pos = 0;
            bool ExisteItem = false;
            foreach (Item a in lista)
            {
                if (a.Nome.ToUpper().IndexOf(txtBusca.Text.ToString().Trim().ToUpper()) != -1) //busca qualuqer parte do Nome
                {
                    gridLista.Rows.Add();
                    gridLista.Rows[pos].Cells[0].Value = a.Nome;
                    gridLista.Rows[pos].Cells[1].Value = a.Disponivel;
                    gridLista.Rows[pos].Cells[2].Value = a.Manutencao;
                    gridLista.Rows[pos].Cells[3].Value = a.Local;
                    gridLista.Rows[pos].Cells[4].Value = a.Fornecedor;
                    pos++;
                    CbAlterar.Items.Add(a.Nome);
                    ExisteItem = true;
                }
            }
            if (!ExisteItem)
            {
                MessageBox.Show("Nenhum item Encontrado", "`Busca", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AtualizaGrid();
            }
            txtBusca.Clear();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            LerTexto();
            LimpaCampos();
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            Item insere = new Item();
            insere.Nome = CbAlterar.Text.Trim();
            insere.Disponivel = Nud1.Text.Trim();
            insere.Manutencao = Nud2.Text.Trim();
            insere.Local = txtLocalA.Text.Trim();
            insere.Fornecedor = txtFornecedor.Text.Trim();
            Item alterado = ExcluiItem(insere); // O Item retornado pelo metodo serve para atribuir o lacal do item automaticamenta, mais explicação a seguir

            if (alterado == null) //Se o item a ser excluido não for encontrado, ele retorna nulo
            {
                return;
            }
            //if (String.IsNullOrEmpty(insere.Local)) //Se o usuario deixar o campo Local em branco, o programa matem o antigo local
            //{
            //    insere.Local = alterado.Local;
            //}
            //if (String.IsNullOrEmpty(insere.Fornecedor)) //Se o usuario deixar o campo fornecedor em branco, o programa matem o antigo local
            //{
            //    insere.Fornecedor = alterado.Fornecedor;
            //}
            lista.Add(insere);
            SalvaLista();
            LimpaCampos();
            MessageBox.Show("Alterado com sucesso", "Ação concluida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExclui_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Certeza que deseja exclui o Item?", "Confirmação", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                Item excluir = new Item();
                excluir.Nome = CbAlterar.Text.Trim();                
                excluir = ExcluiItem(excluir); // O metodo tem retorno para outras funções
                if(excluir != null)
                    {
                    LimpaCampos();
                    MessageBox.Show("Excluido com sucesso", "Ação concluida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Item cadastra = new Item();
            cadastra.Nome = CbAlterar.Text.Trim();
            cadastra.Disponivel = Nud1.Text.Trim();
            cadastra.Manutencao = Nud2.Text.Trim();
            cadastra.Local = txtLocalA.Text.Trim();
            cadastra.Fornecedor = txtFornecedor.Text.Trim();
            lista.Add(cadastra);
            SalvaLista();
            LimpaCampos();
            MessageBox.Show("Cadastrado com sucesso", "Ação concluida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LerTexto()
        {
            lista.Clear();
            using (StreamReader sr = new StreamReader("estoque.txt"))
            {
                string linha = string.Empty;
                int contador = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    string[] split = new string[5];
                    Item a = new Item();
                    split = linha.Split('|');
                    a.Nome = split[0];
                    a.Disponivel = split[1];
                    a.Manutencao = split[2];
                    a.Local = split[3];
                    a.Fornecedor = split[4];

                    lista.Add(a);
                    contador++;
                }
            }
            AtualizaGrid();
        }

        public void AtualizaGrid()
        {
            int pos = 0;
            
            gridLista = dgvLista;// Atribui o elemento da tela
            gridLista.Rows.Clear();
            gridLista.Columns.Clear();            
            gridLista.Columns.Add("Item", "Item"); //nome das colunas
            gridLista.Columns.Add("Disponivel", "Disponivel");
            gridLista.Columns.Add("Manutenção", "Manutenção");
            gridLista.Columns.Add("Local", "Local");
            gridLista.Columns.Add("Observação","Observação");
                       
            gridLista.Columns[0].Width = 160; //tamanho das colunas
            gridLista.Columns[1].Width = 60;
            gridLista.Columns[2].Width = 70;
            gridLista.Columns[3].Width = 170;

            foreach (Item a in lista)
            {
                gridLista.Rows.Add();
                gridLista.Rows[pos].Cells[0].Value = a.Nome;
                gridLista.Rows[pos].Cells[1].Value = a.Disponivel;
                gridLista.Rows[pos].Cells[2].Value = a.Manutencao;
                gridLista.Rows[pos].Cells[3].Value = a.Local;
                gridLista.Rows[pos].Cells[4].Value = a.Fornecedor;
                pos++;
            }
        }

        public Item ExcluiItem(Item a)
        {
            Item Removido = a;
            bool ExisteItem = false;
            foreach (Item b in lista)
            {
                if (a.Nome.ToUpper() == b.Nome.ToUpper())
                {
                    lista.Remove(b);
                    ExisteItem = true;
                    Removido = b;
                    break;
                }
            }
            if (!ExisteItem)
            {
                MessageBox.Show("item Não Encontrado", "`Busca", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AtualizaGrid();
                return null;
            }
            else
            {
                SalvaLista();
            }
            return Removido;
        }

        public void SalvaLista()
        {
            List<string> ListaTexo = new List<string>();
            foreach (Item b in lista)
            {
                ListaTexo.Add(b.Nome + '|' + b.Disponivel + '|' + b.Manutencao + '|' + b.Local + '|' + b.Fornecedor);
            }
            System.IO.File.WriteAllLines(@"estoque.txt", ListaTexo);
            AtualizaGrid();
        }

        public void LimpaCampos()
        {
            CbAlterar.Text= "";
            txtBusca.Clear();
            txtLocalA.Clear();
            txtFornecedor.Clear();
            Nud1.Text = "0";
            Nud2.Text = "0";
            CbAlterar.Items.Clear();
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvLista.CurrentRow.Selected = true;
            CbAlterar.Text = dgvLista.CurrentRow.Cells[0].Value.ToString();            
            Nud1.Text = dgvLista.CurrentRow.Cells[1].Value.ToString();
            Nud2.Text = dgvLista.CurrentRow.Cells[2].Value.ToString();
            txtLocalA.Text = dgvLista.CurrentRow.Cells[3].Value.ToString();
            txtFornecedor.Text = dgvLista.CurrentRow.Cells[4].Value.ToString();
        }
    }
}