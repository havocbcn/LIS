using LIS.Domain;

namespace LIS.Service {
    public interface ITestFactory {
        Test CreateTest(string name);
    }
}