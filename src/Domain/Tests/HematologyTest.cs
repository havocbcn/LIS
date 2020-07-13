using LIS.Service;

namespace LIS.Domain.Tests {
    public abstract class HematologyTest : Test {
        protected HematologyTest(IOperationFactory operationFactory) : base(operationFactory) {

        }

        public float PropD { get; internal set; }
    }
}