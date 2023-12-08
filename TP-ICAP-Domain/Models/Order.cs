using TP_ICAP_Domain.Enums;

namespace TP_ICAP_Domain.Models
{
    public record Order : IComparable<Order>
    {
        public string CompanyId { get; set; }
        public string OrderId { get; set; }
        public Direction Direction { get; set; }
        public int Volume { get; set; }
        public double Notional { get; set; }
        public string OrderDatetime { get; set; }
        public MatchState MatchState { get; set; }
        public List<Order> Matches { get; } = new List<Order>();

        public Order(string companyId, string orderId, Direction direction, int volume, double notional, string orderDatetime)
        {
            CompanyId = companyId;
            OrderId = orderId;
            Direction = direction;
            Volume = volume;
            Notional = notional;
            OrderDatetime = orderDatetime;
            MatchState = MatchState.NO_MATCH;
        }


        public int CompareTo(Order other)
        {
            if (other == null)
            {
                return 1; // null is considered greater
            }

            // Compare based on notional and then order datetime
            int notionalComparison = Notional.CompareTo(other.Notional);
            if (notionalComparison != 0)
            {
                return notionalComparison;
            }

            return string.Compare(OrderDatetime, other.OrderDatetime, StringComparison.Ordinal);
        }
    }
}
