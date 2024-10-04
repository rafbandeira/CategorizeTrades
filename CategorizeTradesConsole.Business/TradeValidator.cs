using CategorizeTradesConsole.Business;
using CategorizeTradesConsole.Models;

namespace CategorizeTradesConsole.Business
{
    public class TradeValidator : ITradeValidator
    {
        public bool IsValid(ITrade trade)
        {
            return trade.Value > 0 &&
                   (trade.ClientSector == "Public" || trade.ClientSector == "Private") &&
                   trade.NextPaymentDate > DateTime.MinValue;
        }

        public (bool IsValid, string ErrorMessage) IsValidTradeData(double value, string clientSector, DateTime nextPaymentDate)
        {
            if (value <= 0)
            {
                return (false, "Valor deve ser maior que zero.");
            }
            if (clientSector != "Public" && clientSector != "Private")
            {
                return (false, "Setor do cliente deve ser 'Public' ou 'Private'.");
            }
            if (nextPaymentDate <= DateTime.MinValue)
            {
                return (false, "Data de pagamento inválida.");
            }
            return (true, string.Empty);
        }

        public (bool IsValid, string ErrorMessage) ValidateTradeCount(List<string> trades, int expectedCount)
        {
            if (trades.Count != expectedCount)
            {
                return (false, "A quantidade de linhas inseridas não corresponde ao número especificado de negociações.");
            }
            return (true, string.Empty);
        }

        public DateTime ReadValidDate()
        {
            while (true)
            {
               
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }

        public int ReadValidTradeCount()
        {
            while (true)
            {
               
                if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                    return count;
                Console.WriteLine("Número inválido. Tente novamente.");
            }
        }
    }
}
