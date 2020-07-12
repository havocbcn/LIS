using LIS.Service;

namespace LIS.Domain.Tests {
  
    [Test("TestExample2")]
    public class TestExample2 : ImmulogyTest {
        public TestExample2(IOperationFactory operationFactory) : base(operationFactory) {
            
        }

        public override void SetCalculation(int calculation) {
            if (calculation > 50) {
                this.Confidence = 0.95f;
                this.Result = Result.Positive;
            } else {
                this.Confidence = 1f;
                this.Result = Result.Negative;
            }
        }
    }
}