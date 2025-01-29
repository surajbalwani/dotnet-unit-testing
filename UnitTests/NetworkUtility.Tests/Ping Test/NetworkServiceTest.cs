using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests.Ping_Test
{
    public class NetworkServiceTest
    {
        private readonly IDNS _dns;
        private readonly NetworkService _pingService;
        public NetworkServiceTest()
        {
            //Dependencies
            _dns = A.Fake<IDNS>();
            // SUT - System Under Test
            _pingService = new NetworkService(_dns);
        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            // Arrange - variables, classes, mocks
            //var pingService = new NetworkService();
            A.CallTo(() => _dns.sendDNS()).Returns(true);

            // Act
            var result = _pingService.SendPing();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            //Arrange
            //var pingService = new NetworkService();

            //Act
            var result = _pingService.PingTimeout(a, b);

            //Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-10000, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            // Arrange - variables, classes, mocks
            //var pingService = new NetworkService();

            // Act
            var result = _pingService.LastPingDate();

            // Assert
            result.Should().BeBefore(1.January(2030));
            result.Should().BeAfter(1.January(2010));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.GetPingOptions();

            //Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_RetunrsObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.MostRecentPings();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<PingOptions>>();
            result.Should().BeOfType<PingOptions[]>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
