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

        public void LerTexto()
        {
            using (StreamReader sr = new StreamReader("estoque.txt"))
            {
                string linha = string.Empty;
                int contador = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    Item a = new Item();
                    a.Nome = linha.Substring(0, linha.IndexOf('|'));
                    linha.Remove(0, linha.IndexOf('|'));
                    a.Disponivel = linha.Substring(0, linha.IndexOf('|') + 1);
                    linha.Remove(0, linha.IndexOf('|'));
                    a.Manutencao = linha.Substring(0, linha.IndexOf('|'));
                    linha.Remove(0, linha.IndexOf('|'));
                    a.Local = linha.Substring(0, linha.IndexOf('|') + 1);
                    linha.Remove(0, linha.IndexOf('|'));
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
            gridLista.Columns.Add("Item", "Item");
            gridLista.Columns.Add("Disponivel", "Disponivel");
            gridLista.Columns.Add("Manutenção/Descarregado", "Manutenção/Descarregado");
            gridLista.Columns.Add("Local", "Local");

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

        private void Form1_Load(object sender, EventArgs e)
        {
           
           
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            foreach (Item a in lista)
            {
                if (a.Nome == txtBusca.Text.ToString().Trim())
                {
                    gridLista.Rows.Clear();
                    gridLista.Rows[0].Cells[0].Value = a.Nome;
                    gridLista.Rows[0].Cells[1].Value = a.Disponivel;
                    gridLista.Rows[0].Cells[2].Value = a.Manutencao;
                    gridLista.Rows[0].Cells[3].Value = a.Local;
                    break;
                }
                else
                { }
                
            }
           
            
        }
    }
}