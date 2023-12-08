using TP_ICAP_Domain.Enums;
using TP_ICAP_Domain.Models;

namespace TP_ICAP_Test
{
    public class OrderBookTest
    {
        [Fact]
        public void SubmitOrder_AddsOrderToList()
        {
            // Arrange
            var orderBook = new OrderBook();
            var order = new Order("A", "A1", Direction.BUY, 100, 4.99, "09:27:43");

            // Act
            orderBook.SubmitOrder(order);

            // Assert
            Assert.Contains(order, orderBook.Orders);
        }

        [Fact]
        public void MatchPriceTimePriority_MatchesOrdersBasedOnPriceTimePriority()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 100, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 100, 5.00, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchPriceTimePriority();

            // Assert
            Assert.Equal(MatchState.FULL_MATCH, buyOrder.MatchState);
            Assert.Equal(MatchState.FULL_MATCH, sellOrder.MatchState);
            Assert.Single(buyOrder.Matches);
            Assert.Single(sellOrder.Matches);
        }


        [Fact]
        public void MatchProRata_MatchesOrdersBasedOnProRata_FullMatch()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 50, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 50, 5.00, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchProRata();

            // Assert
            Assert.Equal(MatchState.FULL_MATCH, buyOrder.MatchState);
            Assert.Equal(MatchState.PARTIAL_MATCH, sellOrder.MatchState);
            Assert.Single(buyOrder.Matches);
            Assert.Single(sellOrder.Matches);
        }

        [Fact]
        public void MatchProRata_MatchesOrdersBasedOnProRata_PartialMatch()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 50, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 30, 5.00, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchProRata();

            // Assert
            Assert.Equal(MatchState.PARTIAL_MATCH, buyOrder.MatchState);
            Assert.Single(buyOrder.Matches);
            Assert.Single(sellOrder.Matches);
        }

        [Fact]
        public void MatchProRata_DoesNotMatchWhenVolumeIsZero()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 0, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 0, 5.00, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchProRata();

            // Assert
            Assert.Equal(MatchState.FULL_MATCH, buyOrder.MatchState); 
            Assert.Equal(MatchState.PARTIAL_MATCH, sellOrder.MatchState); 
            Assert.Empty(buyOrder.Matches);
            Assert.Empty(sellOrder.Matches);
        }

        [Fact]
        public void MatchPriceTimePriority_MatchesOrdersBasedOnPriceTimePriority_FullMatch()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 100, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 100, 4.99, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchPriceTimePriority();

            // Assert
            Assert.Equal(MatchState.FULL_MATCH, buyOrder.MatchState);
            Assert.Equal(MatchState.FULL_MATCH, sellOrder.MatchState);
            Assert.Single(buyOrder.Matches);
            Assert.Single(sellOrder.Matches);
        }

        [Fact]
        public void MatchPriceTimePriority_MatchesOrdersBasedOnPriceTimePriority_PartialMatch()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 200, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 150, 4.99, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchPriceTimePriority();

            // Assert
            Assert.Equal(MatchState.PARTIAL_MATCH, buyOrder.MatchState);
            Assert.Single(buyOrder.Matches);
            Assert.Single(sellOrder.Matches);
        }

        [Fact]
        public void MatchPriceTimePriority_DoesNotMatchWhenVolumeIsZero()
        {
            // Arrange
            var orderBook = new OrderBook();
            var buyOrder = new Order("A", "A1", Direction.BUY, 0, 5.00, "09:27:43");
            var sellOrder = new Order("B", "B1", Direction.SELL, 0, 4.99, "10:21:46");
            orderBook.SubmitOrder(buyOrder);
            orderBook.SubmitOrder(sellOrder);

            // Act
            orderBook.MatchPriceTimePriority();

            // Assert
            Assert.Equal(MatchState.NO_MATCH, sellOrder.MatchState);
            Assert.Equal(MatchState.NO_MATCH, buyOrder.MatchState); 
            Assert.Empty(buyOrder.Matches);
            Assert.Empty(sellOrder.Matches);
        }
    }
}