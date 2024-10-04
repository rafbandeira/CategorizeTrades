
using System;

namespace CategorizeTradesConsole.Models
{
   
    public interface ITrade
    {
        double Value { get; set; } // Valor da transação
        string ClientSector { get; set; } // Setor do cliente, podendo ser "Public" ou "Private"
        DateTime NextPaymentDate { get; set; } // Data do próximo pagamento 
    }
}
