using LIS.Domain.Tests;
using LIS.Service;
using NSubstitute;
using Xunit;

namespace Test.LIS.Domain {
    public class TestShould {
        [Fact]
        public void BeCreatedEmpty() {
            var operationFactory = Substitute.For<IOperationFactory>();
            var order = new TestExample1(operationFactory);

            operationFactory.Received(0).CreateOperation(Arg.Any<string>());
        }
    }
}