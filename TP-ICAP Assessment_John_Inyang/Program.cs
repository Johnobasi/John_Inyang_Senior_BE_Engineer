using TP_ICAP_Domain.Enums;
using TP_ICAP_Domain.Models;

public class OrderBook
{
    public List<Order> Orders { get; } = new List<Order>();

    public void SubmitOrder(Order order)
    {
        Orders.Add(order);
    }

    #region MatchPriceTimePriority
  
    public void MatchPriceTimePriority()
    {
        var buyOrders = Orders.FindAll(order => order.Direction == Direction.BUY);
        var sellOrders = Orders.FindAll(order => order.Direction == Direction.SELL);

        foreach (var buyOrder in buyOrders.OrderBy(o => o))
        {
            foreach (var sellOrder in sellOrders.OrderBy(o => o))
            {
                if (sellOrder.Volume > 0 && buyOrder.Notional >= sellOrder.Notional)
                {
                    var matchedVolume = Math.Min(buyOrder.Volume, sellOrder.Volume);

                    buyOrder.Matches.Add(new Order(sellOrder.CompanyId, sellOrder.OrderId, Direction.SELL,
                                                  sellOrder.Volume, sellOrder.Notional, sellOrder.OrderDatetime));

                    sellOrder.Matches.Add(new Order(buyOrder.CompanyId, buyOrder.OrderId, Direction.BUY,
                                                   sellOrder.Volume, sellOrder.Notional, buyOrder.OrderDatetime));

                    sellOrder.Volume -= matchedVolume;
                    buyOrder.Volume -= matchedVolume;

                    buyOrder.MatchState = buyOrder.Volume == 0 ? MatchState.FULL_MATCH : MatchState.PARTIAL_MATCH;
                    sellOrder.MatchState = sellOrder.Volume == 0 ? MatchState.FULL_MATCH : MatchState.PARTIAL_MATCH;
                }
            }
        }
    }
    #endregion

    #region MatchProRata Methods
    public void MatchProRata()
    {
        var buyOrders = Orders.FindAll(order => order.Direction == Direction.BUY);
        var sellOrders = Orders.FindAll(order => order.Direction == Direction.SELL);

        var totalBuyVolume = buyOrders.Sum(order => order.Volume);

        foreach (var sellOrder in sellOrders)
        {
            foreach (var buyOrder in buyOrders)
            {
                if (sellOrder.Volume > 0)
                {
                    var matchedVolume = (int)(buyOrder.Volume / (double)totalBuyVolume * sellOrder.Volume);

                    buyOrder.Matches.Add(new Order(sellOrder.CompanyId, sellOrder.OrderId, Direction.SELL,
                                                    buyOrder.Volume, matchedVolume, sellOrder.OrderDatetime));

                    sellOrder.Matches.Add(new Order(buyOrder.CompanyId, buyOrder.OrderId, Direction.BUY,
                                                    buyOrder.Volume, matchedVolume, buyOrder.OrderDatetime));

                    sellOrder.Volume -= matchedVolume;
                    buyOrder.Volume -= matchedVolume;

                    
                }
            }
 
            sellOrder.MatchState = sellOrder.Volume == 0 ? MatchState.PARTIAL_MATCH : MatchState.FULL_MATCH;
        }
        foreach (var buyOrder in buyOrders)
        {
            buyOrder.MatchState = buyOrder.Volume == 0 ? MatchState.FULL_MATCH : MatchState.PARTIAL_MATCH;
        }
    }
    #endregion
}

class Program
{
    static void Main()
    {
        OrderBook orderBook = new OrderBook();
     
        orderBook.SubmitOrder(new Order("A", "A1", Direction.BUY, 100, 4.99, "09:27:43"));
        orderBook.SubmitOrder(new Order("B", "B1", Direction.BUY, 200, 5.00, "10:21:46"));
        orderBook.SubmitOrder(new Order("C", "C1", Direction.BUY, 150, 5.00, "10:26:18"));
        orderBook.SubmitOrder(new Order("D", "D1", Direction.SELL, 150, 5.00, "10:32:41"));
        orderBook.SubmitOrder(new Order("E", "E1", Direction.SELL, 100, 5.00, "10:33:07"));

        Console.WriteLine("Running Price-Time-Priority Algorithm:");
        orderBook.MatchPriceTimePriority();

        foreach (Order order in orderBook.Orders)
        {
            Console.WriteLine($"{order.OrderId} - {order.MatchState}");
            foreach (Order match in order.Matches)
            {
                Console.WriteLine($"  {match.OrderId} - {match.Volume}");
            }
        }

        orderBook = new OrderBook();

        orderBook.SubmitOrder(new Order("A", "A1", Direction.BUY, 50, 5.00, "09:27:43"));
        orderBook.SubmitOrder(new Order("B", "B1", Direction.BUY, 200, 5.00, "10:21:46"));
        orderBook.SubmitOrder(new Order("C", "C1", Direction.SELL, 200, 5.00, "10:26:18"));
        
        Console.WriteLine("\nRunning Pro-Rata Algorithm:");
        orderBook.MatchProRata();

        foreach (Order order in orderBook.Orders)
        {
            Console.WriteLine($"{order.OrderId} - {order.MatchState}");
            foreach (Order match in order.Matches)
            {
                Console.WriteLine($"  {match.OrderId} - {match.Notional}");
            }
        }

    }
}
