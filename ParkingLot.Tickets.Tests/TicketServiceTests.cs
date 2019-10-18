using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Data;
using ParkingLot.Data.Models;
using Xunit;

namespace ParkingLot.Tickets.Tests
{
    public class TicketServiceTests
    {
        private readonly VehiklParkingDbContext _context;
        private readonly TicketService _ticketService;
        private readonly ParkingLotConfig _config = new ParkingLotConfig {MaxParkingSpaces = 3};

        public TicketServiceTests()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(TicketServiceTests))
                .Options;

            _context = new VehiklParkingDbContext(options);
            _ticketService = new TicketService(_context, _config);
        }

        [Fact]
        public async Task ItRefusesEntryIfTheLotIsFull()
        {
            // arrange
            var rateLevel = new RateLevel
            {
                Name = "Test Rate",
                RateValue = 1.25M
            };
            
            var tickets = new[]
            {
                new Ticket {Customer = "Test Customer 1", RateLevel = rateLevel},
                new Ticket {Customer = "Test Customer 2", RateLevel = rateLevel},
                new Ticket {Customer = "Test Customer 3", RateLevel = rateLevel}
            };

            await _context.Tickets.AddRangeAsync(tickets);
            await _context.SaveChangesAsync();
            
            // act/assert
            await Assert.ThrowsAsync<LotFullException>(async () => await _ticketService.IssueNewTicket("Test Customer 4", 1));
        }
        
        [Theory]
        [MemberData(nameof(ItCalculatesTheCorrectOwingAmount_Data))]
        public async Task ItCalculatesTheCorrectOwingAmount(DateTimeOffset issuedOn, TimeSpan rateDuration, decimal rateValue, decimal expectedTotal)
        {
            // arrange
            var rateLevel = new RateLevel
            {
                Name = rateDuration.ToString(),
                Duration = rateDuration,
                RateValue = rateValue
            };

            var ticket = new Ticket
            {
                Customer = "Test Customer",
                RateLevel = rateLevel,
                IssuedOn = issuedOn
            };

            await _context.AddAsync(ticket);
            await _context.SaveChangesAsync();
            
            // act
            var amountOwing = _ticketService.GetAmountOwed(ticket);
            
            // assert
            Assert.Equal(expectedTotal, amountOwing);
        }

        public static IEnumerable<object[]> ItCalculatesTheCorrectOwingAmount_Data => new[]
        {
            new object[] {DateTimeOffset.UtcNow.AddHours(-6), TimeSpan.FromHours(3), 1.50M, 3M},
            new object[] {DateTimeOffset.UtcNow.AddHours(-4), TimeSpan.FromHours(12), 1.25M, 0.42M}
        };
    }
}