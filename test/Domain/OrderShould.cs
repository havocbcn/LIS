using AutoFixture.Xunit2;
using FluentAssertions;
using LIS.Domain;
using LIS.Domain.Tests;
using LIS.Service;
using NSubstitute;
using Xunit;

namespace Test.LIS.Domain {
    public class OrderShould {
        [Fact]
        public void BeCreatedEmpty() {
            var testFactory = Substitute.For<ITestFactory>();
            var order = new Order(testFactory);

            testFactory.Received(0).CreateTest(Arg.Any<string>());
        }

        [Theory, AutoData]
        public void AddATestByName(string testName) {
            var testFactory = Substitute.For<ITestFactory>();
            var operationFactory = Substitute.For<IOperationFactory>();
            var test = new TestExample1(operationFactory);
            testFactory.CreateTest(testName).Returns(test);

            var order = new Order(testFactory);

            var actualTest = order.AddTest(testName);

            actualTest.Should().Be(test);
            testFactory.Received(1).CreateTest(testName);
        }
    }
}