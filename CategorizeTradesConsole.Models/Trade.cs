using CategorizeTradesConsole.Models;

namespace TradeModels
{
    
    public class Trade : ITrade
    {
        public double Value { get;  set; }
        public string ClientSector { get;  set; }
        public DateTime NextPaymentDate { get;  set; }

    
        public Trade(double value, string clientSector, DateTime nextPaymentDate)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
        }
    }
}
