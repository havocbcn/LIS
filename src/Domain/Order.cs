using System.Collections.Generic;
using LIS.Domain.Tests;
using LIS.Service;

namespace LIS.Domain {
    public class Order {
        private readonly ITestFactory testFactory;
        private readonly List<Test> tests = new List<Test>();

        public Order(ITestFactory testFactory) {
            this.testFactory = testFactory;
        }

        public Test AddTest(string testName) {
            var test = testFactory.CreateTest(testName);
            tests.Add(test);
            return test;
        }
    }
}