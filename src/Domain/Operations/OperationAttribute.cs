using System;

namespace LIS.Domain.Operations {
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
  
    public class OperationAttribute : Attribute
    {
        public readonly string Name;

        public OperationAttribute(string name)
        {
            this.Name = name;
        }
    }
}