
using CategorizeTradesConsole.Models;
using System.Collections.Generic;

namespace CategorizeTrades.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly List<ITrade> _trades = new List<ITrade>();

        public IEnumerable<ITrade> GetAllTrades() => _trades;

        public void AddTrade(ITrade trade) => _trades.Add(trade);
    }
}
