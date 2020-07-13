using LIS.Service;

namespace LIS.Domain.Tests {
    public abstract class MicrobiologyTest : Test {
        protected MicrobiologyTest(IOperationFactory operationFactory) : base(operationFactory) {

        }

        public float PropC { get; internal set; }
    }
}