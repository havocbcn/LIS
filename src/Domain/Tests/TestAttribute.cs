using System;

namespace LIS.Domain.Tests {
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
  
    public class TestAttribute : Attribute
    {
        public string Name { get; }

        public TestAttribute(string name)
        {
            this.Name = name;
        }
    }
}