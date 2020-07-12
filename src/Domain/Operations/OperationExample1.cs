using LIS.Domain.Tests;

namespace LIS.Domain.Operations {

    [Operation("OperationExample1")]
    public class OperationExample1 : Operation {
        public override void Execute(Test test) {
            switch (test) {
                case BiochemistryTest bioTest:
                    // some magic

                    bioTest.SetCalculation(100);
                    break;
                default:
                    // some magic

                    test.SetCalculation(0);
                    break;
            }            
        }
    }
}