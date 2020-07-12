using System.Collections.Generic;
using LIS.Domain.Operations;
using LIS.Service;

namespace LIS.Domain.Tests {
    public abstract class Test {
        private readonly IOperationFactory operationFactory;
        private List<Operation> operations = new List<Operation>(); 

        public Test(IOperationFactory operationFactory) {
            this.operationFactory = operationFactory;
        }

        public Operation AddOperation(string name) {
            var operation = operationFactory.CreateOperation(name);
            operations.Add(operation);
            return operation;
        }

        internal void ExecuteOperations() 
            => operations.ForEach(operation => operation.Execute(this));

        public abstract void SetCalculation(int calculation);

        public Result Result { get; internal set; }        

        public float Confidence { get; internal set; }
    }
}