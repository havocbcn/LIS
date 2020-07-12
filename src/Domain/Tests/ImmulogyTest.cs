using LIS.Service;

namespace LIS.Domain.Tests {
    public abstract class ImmulogyTest : Test {
        protected ImmulogyTest(IOperationFactory operationFactory) : base(operationFactory) {

        }

        public float ImmulogyProperty { get; internal set; }
    }
}