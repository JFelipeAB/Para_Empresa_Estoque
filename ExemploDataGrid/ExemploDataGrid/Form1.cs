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

        
        private void Form1_Load(object sender, EventArgs e)
        {
           
           
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            gridLista.Rows.Clear();
            int pos = 0;
            bool ExisteItem = false;
            foreach (Item a in lista)
            {
                if (a.Nome.ToUpper().IndexOf(txtBusca.Text.ToString().Trim().ToUpper()) != -1)
                {
                    gridLista.Rows.Add();
                    gridLista.Rows[pos].Cells[0].Value = a.Nome;
                    gridLista.Rows[pos].Cells[1].Value = a.Disponivel;
                    gridLista.Rows[pos].Cells[2].Value = a.Manutencao;
                    gridLista.Rows[pos].Cells[3].Value = a.Local;
                    pos++;
                    ExisteItem = true;
                }
            }
            if (!ExisteItem)
            {
                MessageBox.Show("Nenhum item Encontrado", "`Busca", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AtualizaGrid();
            }
        }
       

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            Item insere = new Item();
            insere.Nome = txtNomeA.Text.Trim();
            insere.Disponivel = Nud1.Text.Trim();
            insere.Manutencao = Nud2.Text.Trim();
            insere.Local = txtLocalA.Text.Trim();
            Item alterado = ExcluiItem(insere);
            if ( alterado.Local == "")
            {
                insere.Local = alterado.Local;                
            }
            lista.Add(insere);
            SalvaLista();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Certeza que deseja exclui o Item?", "`Confirmação", MessageBoxButtons.YesNo);
            Item excluir = new Item();
            excluir.Nome = txtNomeA.Text.Trim();
            excluir.Disponivel = Nud1.Text.Trim();
            excluir.Manutencao = Nud2.Text.Trim();
            excluir.Local = txtLocalA.Text.Trim();
            Item reserva = ExcluiItem(excluir);
        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Item cadastra = new Item();
            cadastra.Nome = txtNomeC.Text.Trim();   
            cadastra.Disponivel = Nud3.Text.Trim();
            cadastra.Manutencao = Nud4.Text.Trim();
            cadastra.Local = txtLocalC.Text.Trim();
            lista.Add(cadastra);
            SalvaLista();
            
        }

        public void LerTexto()
        {
            using (StreamReader sr = new StreamReader("estoque.txt"))
            {
                string linha = string.Empty;
                int contador = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    string[] split = new string[4];
                    Item a = new Item();
                    split = linha.Split('|');
                    a.Nome = split[0];
                    a.Disponivel = split[1];
                    a.Manutencao = split[2];
                    a.Local = split[3];

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
            gridLista.Columns.Clear();
            gridLista.Rows.Clear();
            gridLista.Columns.Add("Item", "Item");
            gridLista.Columns.Add("Disponivel", "Disponivel");
            gridLista.Columns.Add("Manutenção/Descarregado", "Manutenção/Descarregado");
            gridLista.Columns.Add("Local", "Local");

            gridLista.Columns[3].Width = 200;

            foreach (Item a in lista)
            {
                gridLista.Rows.Add();
                gridLista.Rows[pos].Cells[0].Value = a.Nome;
                gridLista.Rows[pos].Cells[1].Value = a.Disponivel;
                gridLista.Rows[pos].Cells[2].Value = a.Manutencao;
                gridLista.Rows[pos].Cells[3].Value = a.Local;
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
                ListaTexo.Add(b.Nome + '|' + b.Disponivel + '|' + b.Manutencao + '|' + b.Local);
            }
            System.IO.File.WriteAllLines(@"estoque.txt", ListaTexo);
            AtualizaGrid();
        }
    }
}