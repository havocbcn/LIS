using System;
using System.Collections.Generic;
using System.Linq;
using LIS.Domain.Tests;
using LIS.Exceptions;

namespace LIS.Service {
    public class TestFactory : ITestFactory {
        private readonly IOperationFactory operationFactory;
        private readonly Dictionary<string, Type> tests = new Dictionary<string, Type>();
        
        public TestFactory(IOperationFactory operationFactory) {
            var assembly = typeof(Test).Assembly;
            tests = assembly.GetTypes()
                .Where(type => type.IsDefined(typeof(TestAttribute), false))
                .ToDictionary( type => type.Name.ToLower(), type => type);

            this.operationFactory = operationFactory;
        }

        public Test CreateTest(string name) 
            => tests.ContainsKey(name.ToLower()) ? 
                (Test)Activator.CreateInstance(tests[name.ToLower()], new object[] { operationFactory }) :
                throw new TestNotFoundException(name);
        
    }
}