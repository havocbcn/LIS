using LIS.Service;

namespace LIS.Domain.Tests {
  
    [Test("TestExample1")]
    public class TestExample1 : BiochemistryTest {
        public TestExample1(IOperationFactory operationFactory) : base(operationFactory) {
            
        }

        public override void SetCalculation(int calculation) {
            if (calculation > 50) {
                this.BiochemistryProperty = 0.32f;
                this.Confidence = 0.95f;
                this.Result = Result.Positive;
            } else {
                this.BiochemistryProperty = 0;
                this.Confidence = 1f;
                this.Result = Result.Negative;
            }
        }
    }
}