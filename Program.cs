using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bergs.ProvacSharp.BD;


//obs: Projeto quase finalizado, ainda faltou utlizar alguns valores do banco de dados e manipular os atributos com ele, 
// o método persistir ainda não finalizado por conta do banco de dados mas o resto está ok!


namespace Bergs.ProvacSharp
{
    public class Program 
    {
        static void Main(string[] args)
        {
            //NÃO FINALIZADO!!!
            //Conexão com o banco de dados
            ChaveFavorita chaves = new ChaveFavorita("joao","dsad",TiposChave.CPF);
            Conta c = new Conta();
            string connection = "c:\\soft\\pxc\\data\\Pxcz02da.mdb";

            AcessoBancoDados acesso = new AcessoBancoDados(connection);
            acesso.Abrir();



            string sql = "INSERT INTO Extrato (NumeroConta, NomeTitular) VALUES (@NumeroConta, @NomeTitular)";

            // Passando parâmetros de forma segura
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.Text;

            var numero = new SqlParameter("@NumeroConta", System.Data.DbType.String);
            numero.Value = chaves.NomeTitular;

            var nomeTitular = new SqlParameter("@NomeTitular", System.Data.DbType.String);
            nomeTitular.Value = chaves.TipoChave;

            command.Parameters.Add(numero);
            command.Parameters.Add(nomeTitular);

            acesso.ExecutarInsert(command.GetGeneratedQuery());
            acesso.EfetivarComandos();
            try
            {
                Menu(c);
            }
            catch (ExceptionPersonalizada ex)
            {
                Console.Write(ex.Message);
                Console.WriteLine(", Finalizando o programa!");
                Console.ReadKey();
            }
        }//NÃO FINALIZADO!!!

        //Menu De opções
        static void Menu(Conta c)
        {
            //Opções
            Console.WriteLine($"== Saldo atual: R$ {c.Saldo.ToString("F2")} ==");
            Console.WriteLine("\n1. Efetuar Crédito em conta" +
                "\n2. Adicionar chaves favoritas" +
                "\n3. Listar chaves favoritas" +
                "\n4. Enviar PIX" +
                "\n5. Sair");
            Console.Write("Informe a opção desejada: ");

            int Value = int.Parse(Console.ReadLine());
            if (Value > 5 || Value < 0)
                throw new ExceptionPersonalizada("Erro 10: Valor Inválido.");

            switch (Value)
            {
                //Realiza a operação de Efetuar o crédito na conta
                case 1:
                    Console.Write("Digite o valor a ser adicionado: ");
                    decimal ValorInput = decimal.Parse(Console.ReadLine());
                    c.CreditarConta(ValorInput);
                    Console.WriteLine("Sucesso!\n");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(c);
                    break;
                //Realiza a operação de Efetuar o crédito na conta
                case 2:
                    Console.Write("Digite o nome do titular: ");
                    string InputNomeTitular = Console.ReadLine();
                    Console.Write("\nTipo da chave: ");
                    string InputTipoDaChave = Console.ReadLine().ToLower();
                    Console.Write("\nDigite a chave: ");
                    string InputChave = Console.ReadLine();
                    c.AdicionarChaveFavorita(InputNomeTitular, InputTipoDaChave, InputChave);
                    Console.WriteLine("Chave Adicionada!\n");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(c);
                    break;
                //Realiza a operação Mostrar os registros realizados de acordo com a chave escolhida
                case 3:
                    Console.WriteLine("------  Registros -------");
                    c.ListarChavesFavoritas();
                    Console.WriteLine("------  Registros -------\n");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(c);
                    break;
                //Realiza a operação de PIX
                case 4:
                    Console.Write("Digite a chave: ");
                    string Chave = Console.ReadLine();
                    Console.WriteLine("Digite o valor a enviado:");
                    Decimal ValorPix = decimal.Parse(Console.ReadLine());
                    c.EnviarPix(Chave, ValorPix);
                    Console.WriteLine($"Pix de {ValorPix} Realizado com sucesso!\n");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(c);
                    break;
                case 5:
                    Console.WriteLine("Finalizando o programa tenha um ótimo dia!");
                    break;
            }

        }
    }
}

