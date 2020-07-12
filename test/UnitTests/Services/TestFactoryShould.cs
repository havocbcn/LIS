using System;
using System.Linq;
using FluentAssertions;
using LIS.Domain.Tests;
using LIS.Exceptions;
using LIS.Service;
using NSubstitute;
using Xunit;
using System.Reflection;

namespace UnitTest.LIS.Service {
    public class TestFactoryShould {
        [Fact]
        public void ObtainTestExample1() {
            var operationFactory = Substitute.For<IOperationFactory>();

            var factory = new TestFactory(operationFactory);

            var expectedTest = new TestExample1(operationFactory);
            var actualTest = factory.CreateTest("testexample1");

            actualTest.Should().BeEquivalentTo(expectedTest);
        }

        [Fact]
        public void FailIfTestDoesntExists() {
            var operationFactory = Substitute.For<IOperationFactory>();

            var factory = new TestFactory(operationFactory);

            Action act = () => factory.CreateTest("XXXXX");
            act.Should().Throw<TestNotFoundException>();
        }

        [Fact]
        public void FailIfATestDoesNotHaveAName() {
            var assembly = typeof(Test).Assembly;
            assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Test)))
                .Where(type => !type.IsDefined(typeof(TestAttribute), false))
                .Should().BeEmpty();                
        }

        [Fact]
        public void FailIfATestNameIsRepeated() {
            var assembly = typeof(Test).Assembly;

            assembly.GetTypes()
                .Where(type => type.IsDefined(typeof(TestAttribute), false))
                .GroupBy(type => type.GetCustomAttribute<TestAttribute>(false).Name.ToLower())
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .Should().BeEmpty();           
        }
    }
}