using LIS.Domain.Tests;

namespace LIS.Service {
    public interface ITestFactory {
        Test CreateTest(string name);
    }
}