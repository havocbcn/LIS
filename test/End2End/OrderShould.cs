using FluentAssertions;
using LIS.Domain;
using LIS.Domain.Tests;
using LIS.Service;
using Xbehave;

namespace End2End.LIS.Domain {
    public class OrderShould {
        [Scenario]
        public void ExecuteOperation1InTest1() {
            var operationFactory = new OperationFactory();
            var testFactory = new TestFactory(operationFactory);
            Test testExample1 = null;
            Order order = null;

            "Given an order"
                .x(() =>  order = new Order(testFactory));

            "that is added a biochemistry test" 
                .x(() => testExample1 = order.AddTest("testExample1"));

            "And operation OperationExample1 is added to testExample1" 
                .x(() => testExample1.AddOperation("operationExample1"));

            "And the order is executed"
                .x(()=> order.ExecuteOperations());

            "Then the test must be positive with a value of cofidence 0.95 (both common test properties) and a biochemistryProperty of 0.32"
                .x( ()=> { 
                     testExample1.Result.Should().Be(Result.Positive);
                     testExample1.Confidence.Should().Be(0.95f);
                     (testExample1 as BiochemistryTest).PropA.Should().Be(0.32f);
                });
        }

        [Scenario]
        public void ExecuteOperation1And2InTest1() {
            var operationFactory = new OperationFactory();
            var testFactory = new TestFactory(operationFactory);
            Test testExample1 = null;
            Order order = null;

            "Given an order"
                .x(() =>  order = new Order(testFactory));

            "that is added a biochemistry test" 
                .x(() => testExample1 = order.AddTest("testExample1"));

            "And operation OperationExample1 and OperationExample2 is added to testExample1" 
                .x(() => {
                    testExample1.AddOperation("operationExample1");
                    testExample1.AddOperation("operationExample2");
                });

            "And the order is executed"
                .x(()=> order.ExecuteOperations());

            "Then the test must be negative"
                .x( ()=> { 
                     testExample1.Result.Should().Be(Result.Negative);
                     testExample1.Confidence.Should().Be(1f);
                     (testExample1 as BiochemistryTest).PropA.Should().Be(0.0f);
                });
        }

        [Scenario]
        public void ExecuteOperation1InImmunologyTest() {
            var operationFactory = new OperationFactory();
            var testFactory = new TestFactory(operationFactory);
            Test testExample2 = null;
            Order order = null;

            "Given an order"
                .x(() =>  order = new Order(testFactory));

            "that is added a immunology test" 
                .x(() => testExample2 = order.AddTest("testExample2"));

            "And operation OperationExample1 is added" 
                .x(() => testExample2.AddOperation("operationExample1"));

            "And the order is executed"
                .x(()=> order.ExecuteOperations());

            "Then the test must be negative"
                .x( ()=> { 
                     testExample2.Result.Should().Be(Result.Negative);
                     testExample2.Confidence.Should().Be(1.0f);
                });
        }
    }
}