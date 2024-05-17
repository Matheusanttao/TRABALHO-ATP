using System;

class Program
{
    static int tamanhoExtrato = 2;
    static int[] dia = new int[tamanhoExtrato];
    static int[] mes = new int[tamanhoExtrato];
    static string[] descricao = new string[tamanhoExtrato];
    static double[] valor = new double[tamanhoExtrato];
    static string[] tipo = new string[tamanhoExtrato];
    static double saldoInicial = 0;
    static double saldoFinal = 0;
    static int numeroLancamentos = 0;

    static void Main(string[] args)
    {
        bool encerrarPrograma = false;

        while (!encerrarPrograma)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Incluir lançamento");
            Console.WriteLine("2. Exibir extrato");
            Console.WriteLine("3. Encerrar");
            Console.Write("Digite a opção desejada: ");

            string opcao = Console.ReadLine();
            Console.Clear(); // Limpa a tela 
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    IncluirLancamento();
                    break;
                case "2":
                    ExibirExtrato();
                    break;
                case "3":
                    encerrarPrograma = true;
                    Console.WriteLine("Programa encerrado. Obrigado por utilizar o sistema.");
                    Console.WriteLine("Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void IncluirLancamento()
    {
        if (numeroLancamentos >= tamanhoExtrato)
        {
            // Remover o último lançamento, deslocando os lançamentos anteriores para frente
            for (int i = 0; i < tamanhoExtrato - 1; i++)
            {
                dia[i] = dia[i + 1];
                mes[i] = mes[i + 1];
                descricao[i] = descricao[i + 1];
                valor[i] = valor[i + 1];
                tipo[i] = tipo[i + 1];
            }

            // Limpar o último elemento do vetor
            dia[tamanhoExtrato - 1] = 0;
            mes[tamanhoExtrato - 1] = 0;
            descricao[tamanhoExtrato - 1] = null;
            valor[tamanhoExtrato - 1] = 0;
            tipo[tamanhoExtrato - 1] = null;

            // Decrementar o número de lançamentos
            numeroLancamentos--;
        }

        int novoDia;
        Console.Write("Digite o dia do lançamento: ");
        while (!int.TryParse(Console.ReadLine(), out novoDia))
        {
            Console.WriteLine("Entrada inválida. Digite apenas números para o dia.");
            Console.Write("Digite o dia do lançamento: ");
        }

        int novoMes;
        Console.Write("Digite o mês do lançamento: ");
        while (!int.TryParse(Console.ReadLine(), out novoMes))
        {
            Console.WriteLine("Entrada inválida. Digite apenas números para o mês.");
            Console.Write("Digite o mês do lançamento: ");
        }

        Console.Write("Digite a descrição do lançamento: ");
        string novaDescricao = Console.ReadLine();

        Console.Write("Digite o valor do lançamento: R$ ");
        double novoValor;
        bool valorValido = double.TryParse(Console.ReadLine(), out novoValor);

        while (!valorValido)
        {
            Console.WriteLine("Valor inválido. Digite somente números.");
            Console.Write("Digite o valor do lançamento: ");
            valorValido = double.TryParse(Console.ReadLine(), out novoValor);
        }

        Console.Write("Digite o tipo do lançamento (D para débito ou C para crédito): ");
        string novoTipo = Console.ReadLine().ToUpper();

        while (novoTipo != "D" && novoTipo != "C")
        {
            Console.WriteLine("Tipo de lançamento inválido. Digite apenas 'D' ou 'C'.");
            Console.Write("Digite o tipo do lançamento (D para débito ou C para crédito): ");
            novoTipo = Console.ReadLine().ToUpper();
        }

        dia[numeroLancamentos] = novoDia;
        mes[numeroLancamentos] = novoMes;
        descricao[numeroLancamentos] = novaDescricao;
        valor[numeroLancamentos] = novoValor;
        tipo[numeroLancamentos] = novoTipo;

        numeroLancamentos++;
        AtualizarSaldos();
        Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
        Console.Clear(); // Limpa a tela 
    }

    static void ExibirExtrato()
    {
        Console.WriteLine("Extrato:");

        for (int i = 0; i < numeroLancamentos; i++)
        {
            Console.WriteLine("{0}/{1} - {2}: {3} (R${4})", dia[i], mes[i], descricao[i], tipo[i], valor[i]);
        }

        Console.WriteLine();
        Console.WriteLine("Saldo anterior: R$ {0}", saldoInicial);
        Console.WriteLine("Saldo final: R$ {0}", saldoFinal);
        Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
        Console.Clear(); // Limpa a tela 
    }

    static void AtualizarSaldos()
    {
        saldoInicial = 0;
        saldoFinal = 0;

        for (int i = 0; i < numeroLancamentos; i++)
        {
            if (tipo[i] == "D")
            {
                saldoFinal -= valor[i];
            }
            else if (tipo[i] == "C")
            {
                saldoFinal += valor[i];
            }
        }
    }
}

