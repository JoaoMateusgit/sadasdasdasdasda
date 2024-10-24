using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    class Conta
    {
        public decimal Saldo { get; set; }
        public List<ChaveFavorita> ListaChaves { get; set; } = new List<ChaveFavorita>();

        //Creditar o valor a conta do cliente
        public void CreditarConta(decimal strValor)
        {
            if (strValor < 0)
                throw new ExceptionPersonalizada("Erro 20: Valor menor ou igual a zero.");
            Saldo += strValor;
        }

        //Adicionar a chave do cliente aos favoritos
        public void AdicionarChaveFavorita(string stNome, string strTipo, string strChave)
        {
            //Adiciona e valida os tipos de chave
            TiposChave chave = new TiposChave();
            if (strTipo == "1" || strTipo == "telefone")
                chave = TiposChave.telefone;
            else if (strTipo == "2" || strTipo == "cpf")
                chave = TiposChave.CPF;
            else
                throw new ExceptionPersonalizada("Erro 30: Tipo de chave inválido.");

            // Valida as chaves conforme o tipo
            if (chave == TiposChave.CPF && !System.Text.RegularExpressions.Regex.IsMatch(strChave, "^\\d{11}$"))
                throw new ExceptionPersonalizada("Erro 40: CPF inválido.");
            else if (chave == TiposChave.telefone && !System.Text.RegularExpressions.Regex.IsMatch(strChave, "^\\(?\\d{2}\\)?\\s?\\d{4,5}-?\\d{4}$"))
                throw new ExceptionPersonalizada("Erro 50: Telefone inválido.");

            // Valida chave duplicada
            if (ListaChaves.Where(x => x.Chave == strChave).Any())
                throw new ExceptionPersonalizada("Erro 60: Chave duplicada.");

            //Adiciona a chave se correta
            ListaChaves.Add(new ChaveFavorita(stNome, strChave, chave));
        }

        //Mostra para o cliente as chaves que ele definiu como favoritas
        public void ListarChavesFavoritas()
        {
            foreach (ChaveFavorita chave in ListaChaves)
            {
                Console.WriteLine($"Nome do titular: {chave.NomeTitular},\nChave: {chave.Chave},\nQuantidade de pix realizados: {chave.Quantidade},\nValor total enviado: {chave.ValorTotal} R$");
            }
        }

        //Realiza o envio do pix
        public void EnviarPix(string strChave, decimal strValor)
        {
            //Valida se o valor está dentro do saldo
            if (strValor > Saldo)
                throw new ExceptionPersonalizada("Erro 10: Valor Inválido.");
            //Valida se a chave existe
            else if (!ListaChaves.Where(x => x.Chave.Equals(strChave)).Any())
                throw new ExceptionPersonalizada("Erro 60: Chave inexistente.");
            //Valida se o valor é menor ou igual a zero
            else if (strValor <= 0)
                throw new ExceptionPersonalizada("Erro 20: Valor menor ou igual a zero.");

            //Selecionada a chave correta
            ChaveFavorita chave = ListaChaves.Where(x => x.Chave.Equals(strChave)).FirstOrDefault();

            //Adiciona na quantidade de pixes
            chave.Quantidade++;

            //Adiciona ao valor total
            chave.ValorTotal += strValor;

            //Diminui do Saldo
            Saldo -= strValor;
        }

        //Falta Implementar
        public void Persistir()
        {

        }
    }
}
