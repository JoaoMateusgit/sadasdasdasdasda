using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    class RetornoListaChave : Retorno
    {
        public RetornoListaChave(bool sucesso, int codRetorno, string mensagem) : base(sucesso, codRetorno, mensagem)
        {
        }
    }
}
