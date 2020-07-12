using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LIS.Domain.Operations;
using LIS.Exceptions;

namespace LIS.Service {
    public class OperationFactory : IOperationFactory {
        private readonly Dictionary<string, Type> operations = new Dictionary<string, Type>();
        
        public OperationFactory() {
            var assembly = typeof(Operation).Assembly;
            operations = assembly.GetTypes()
                .Where(type => type.IsDefined(typeof(OperationAttribute), false))
                .ToDictionary( type => type.GetCustomAttribute<OperationAttribute>(false).Name.ToLowerInvariant(), type => type);
        }
        public Operation CreateOperation(string name) 
            => operations.ContainsKey(name.ToLowerInvariant()) ? 
                (Operation)Activator.CreateInstance(operations[name.ToLowerInvariant()]) : 
                throw new OperationNotFoundException(name);
        
    }
}