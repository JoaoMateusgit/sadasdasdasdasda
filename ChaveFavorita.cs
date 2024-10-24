using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    class ChaveFavorita
    {
        public string NomeTitular { get; set; }
        public string Chave { get; set; }
        public TiposChave TipoChave { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }

        //  Construtor para agilizar o trabalho
        public ChaveFavorita(string nome, string chave, TiposChave tipo)
        {
            NomeTitular = nome;
            Chave = chave;
            TipoChave = tipo;
        }


    }
}
