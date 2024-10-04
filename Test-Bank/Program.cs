
using System;
using CategorizeTradesConsole.Models;
using CategorizeTradesConsole.Business;
using CategorizeTrades.Repositories;
using CategorizeTradesConsole.Services;
using TradeModels;
using System.Globalization;


class Program
{
    static void Main(string[] args)
    {
        ITradeRepository tradeRepository = new TradeRepository();
        ITradeValidator tradeValidator = new TradeValidator();
        ITradeCategorizer tradeCategorizer = new TradeCategorizer(tradeValidator, tradeRepository);

        // Ler a data de referência
        Console.WriteLine("Digite a data de referência (mm/dd/yyyy):");
        DateTime referenceDate;
        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
        {
            Console.WriteLine("Data inválida. Por favor, insira a data no formato mm/dd/yyyy:");
        }

        // Ler o número de negociações
        Console.WriteLine("Digite o número de negociações:");
        int tradeCount;
        while (!int.TryParse(Console.ReadLine(), out tradeCount) || tradeCount <= 0)
        {
            Console.WriteLine("Número inválido. Digite um número inteiro positivo:");
        }

        // Ler as negociações
        Console.WriteLine($"Digite cada negociação no formato 'valor setor data' em {tradeCount} linhas:");
        for (int i = 0; i < tradeCount; i++)
        {
            string[] tradeInput = Console.ReadLine().Split(' ');

            if (tradeInput.Length == 3 &&
                double.TryParse(tradeInput[0], out double value) &&
                DateTime.TryParseExact(tradeInput[2], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime nextPaymentDate))
            {
                var (isValid, errorMessage) = tradeValidator.IsValidTradeData(value, tradeInput[1], nextPaymentDate);

                if (isValid)
                {
                    tradeRepository.AddTrade(new Trade(value, tradeInput[1], nextPaymentDate));
                }
                else
                {
                    Console.WriteLine($"Erro na negociação {i + 1}: {errorMessage}");
                    i--; 
                }
            }
            else
            {
                Console.WriteLine($"Erro: Formato inválido na negociação {i + 1}. Tente novamente.");
                i--; 
            }
        }

        // Categorização e saída
        foreach (var trade in tradeRepository.GetAllTrades())
        {
            string category = tradeCategorizer.GetCategory(trade, referenceDate);
            Console.WriteLine(category);
        }
    }
}
