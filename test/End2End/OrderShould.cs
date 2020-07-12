using FluentAssertions;
using LIS.Domain;
using LIS.Domain.Tests;
using LIS.Service;
using Xbehave;

namespace End2End.LIS.Domain {
    public class OrderShould {
        [Scenario]
        public void ExecuteAOperationInATest() {
            var operationFactory = new OperationFactory();
            var testFactory = new TestFactory(operationFactory);
            Test testExample1 = null;
            Order order = null;

            "Given an order with test TestExample1"
                .x(() =>  order = new Order(testFactory));

            "that is added a test" 
                .x(() => testExample1 = order.AddTest("testExample1"));

            "And operation OperationExample1 is added to testExample1" 
                .x(() => testExample1.AddOperation("operationExample1"));

            "And the order is executed"
                .x(()=> order.ExecuteOperations());

            "Then the test must be positive with a value of cofidence 0.95 (both common test properties) and a biochemistryProperty of 0.32"
                .x( ()=> { 
                     testExample1.Result.Should().Be(Result.Positive);
                     testExample1.Confidence.Should().Be(0.95f);
                     (testExample1 as BiochemistryTest).BiochemistryProperty.Should().Be(0.32f);
                });
        }
    }
}