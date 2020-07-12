using AutoFixture.Xunit2;
using FluentAssertions;
using LIS.Domain.Operations;
using LIS.Domain.Tests;
using LIS.Service;
using NSubstitute;
using Xunit;

namespace Test.LIS.Domain {
    public class TestShould {
        [Fact]
        public void BeCreatedEmpty() {
            var operationFactory = Substitute.For<IOperationFactory>();

            var test = new TestExample1(operationFactory);

            operationFactory.Received(0).CreateOperation(Arg.Any<string>());
        }

        [Theory, AutoData]
        public void AddAOperationByName(string name) {
            var operationFactory = Substitute.For<IOperationFactory>();
            var operation = new OperationExample1();
            operationFactory.CreateOperation(name).Returns(operation);

            var test = new TestExample1(operationFactory);
            var operationActual = test.AddOperation(name);

            operationFactory.Received(1).CreateOperation(name);
            operationActual.Should().Be(operation);
        }
    }
}