
using System;
using CategorizeTradesConsole.Models;

namespace CategorizeTradesConsole.Services
{
    public interface ITradeCategorizer
    {
        string GetCategory(ITrade trade, DateTime referenceDate);
    }
}
