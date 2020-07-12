using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                .ToDictionary( type => type.GetCustomAttribute<TestAttribute>(false).Name.ToLowerInvariant(), type => type);

            this.operationFactory = operationFactory;
        }

        public Test CreateTest(string name) 
            => tests.ContainsKey(name.ToLowerInvariant()) ? 
                (Test)Activator.CreateInstance(tests[name.ToLowerInvariant()], new object[] { operationFactory }) :
                throw new TestNotFoundException(name);
        
    }
}