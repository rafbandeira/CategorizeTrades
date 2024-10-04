using CategorizeTrades.Repositories;
using CategorizeTradesConsole.Business;
using CategorizeTradesConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategorizeTradesConsole.Services
{
    public class TradeCategorizer : ITradeCategorizer
    {
        private readonly ITradeValidator _validator;
        private readonly ITradeRepository _repository;
        private readonly List<Func<ITrade, DateTime, string>> _categories;

        public TradeCategorizer(ITradeValidator validator, ITradeRepository repository)
        {
            _validator = validator;
            _repository = repository;
            _categories = new List<Func<ITrade, DateTime, string>>
            {
                ExpiredCategory,
                HighRiskCategory,
                MediumRiskCategory
            };
        }

        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            if (!_validator.IsValid(trade))
            {
                throw new ArgumentException("Trade data is not valid.");
            }

            foreach (var categorize in _categories)
            {
                var result = categorize(trade, referenceDate);
                if (result != null)
                {
                    return result;
                }
            }
            return "UNCATEGORIZED";
        }

        private string ExpiredCategory(ITrade trade, DateTime referenceDate) =>
            (referenceDate - trade.NextPaymentDate).Days > 30 ? "EXPIRED" : null;

        private string HighRiskCategory(ITrade trade, DateTime referenceDate) =>
            (trade.Value > 1000000 && trade.ClientSector == "Private") ? "HIGHRISK" : null;

        private string MediumRiskCategory(ITrade trade, DateTime referenceDate) =>
            (trade.Value > 1000000 && trade.ClientSector == "Public") ? "MEDIUMRISK" : null;
    }
}
