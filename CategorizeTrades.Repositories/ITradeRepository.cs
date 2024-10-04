using CategorizeTradesConsole.Models;
using System.Collections.Generic;

namespace CategorizeTrades.Repositories
{
    public interface ITradeRepository
    {
        IEnumerable<ITrade> GetAllTrades();
        void AddTrade(ITrade trade);
    }
}
