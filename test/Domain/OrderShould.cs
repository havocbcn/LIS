using LIS.Service;
using NSubstitute;
using Xunit;

namespace LIS.Domain.Test {
    public class OrderShould {
        [Fact]
        public void BeCreatedEmpty() {
            var testFactory = Substitute.For<ITestFactory>();
            var order = new Order(testFactory);

            testFactory.Received(0).CreateTest(Arg.Any<string>());
        }
    }
}