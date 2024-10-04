
using CategorizeTradesConsole.Models;

namespace CategorizeTradesConsole.Business
{
    public interface ITradeValidator
    {

        bool IsValid(ITrade trade);
        (bool IsValid, string ErrorMessage) IsValidTradeData(double value, string clientSector, DateTime nextPaymentDate);
        DateTime ReadValidDate();
        int ReadValidTradeCount();

    }
}
