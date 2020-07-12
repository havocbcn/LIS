using LIS.Domain.Operations;

namespace LIS.Service {
    public interface IOperationFactory {
        Operation CreateOperation(string name);
    }
}