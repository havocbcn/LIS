using LIS.Service;

namespace LIS.Domain.Tests {
    public abstract class BiochemistryTest : Test {
        protected BiochemistryTest(IOperationFactory operationFactory) : base(operationFactory) {

        }

        public float PropA { get; internal set; }
    }
}