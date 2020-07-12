using System;
using System.Linq;
using FluentAssertions;
using LIS.Domain.Operations;
using LIS.Exceptions;
using LIS.Service;
using Xunit;
using System.Reflection;

namespace UnitTest.LIS.Service {
    public class OperationFactoryShould {
        [Fact]
        public void ObtainTestExample1() {
            var factory = new OperationFactory();

            var expectedOperation = new OperationExample1();
            var actualOperation = factory.CreateOperation("Operationexample1");

            actualOperation.Should().BeOfType(expectedOperation.GetType());
        }

        [Fact]
        public void FailIfTestDoesntExists() {
            var factory = new OperationFactory();

            Action act = () => factory.CreateOperation("XXXXX");
            act.Should().Throw<OperationNotFoundException>();
        }

        [Fact]
        public void FailIfAOperationDoesNotHaveAName() {
            var assembly = typeof(Operation).Assembly;
            assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Operation)))
                .Where(type => !type.IsDefined(typeof(OperationAttribute), false))
                .Should().BeEmpty();    
        }  
        
        [Fact]
        public void FailIfAOperationNameIsRepeated() {
            var assembly = typeof(Operation).Assembly;

            assembly.GetTypes()
                .Where(type => type.IsDefined(typeof(OperationAttribute), false))
                .GroupBy(type => type.GetCustomAttribute<OperationAttribute>(false).Name.ToLower())
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .Should().BeEmpty();           
          
        }
    }
}