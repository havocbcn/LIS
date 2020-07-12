using LIS.Domain.Tests;

namespace LIS.Domain.Operations {

    [Operation("OperationExample2")]
    public class OperationExample2 : Operation {
        public override void Execute(Test test) {
            // some magic

            test.SetCalculation(0);
        }
    }
}