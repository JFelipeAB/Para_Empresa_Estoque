using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploDataGrid
{
    public class Item
    {
        private string nome;
        private string disponivel;
        private string manutencao;
        private string local;
        private string fornecedor;
        private string data;
        public string Nome { get; set;}
        public string Fornecedor { get; set;}
        public string Disponivel { get; set;}
        public string Manutencao { get; set; }
        public string Local { get; set; }
        public string Data { get; set;}
    }
}
