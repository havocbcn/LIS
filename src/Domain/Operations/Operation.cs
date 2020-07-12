using LIS.Domain.Tests;

namespace LIS.Domain.Operations {
    public abstract class Operation
    {
        public abstract void Execute(Test test);
    }
}