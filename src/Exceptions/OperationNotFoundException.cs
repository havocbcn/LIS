namespace LIS.Exceptions {
    [System.Serializable]
    public class OperationNotFoundException : System.Exception
    {
        public OperationNotFoundException() { }
        public OperationNotFoundException(string message) : base(message) { }
        public OperationNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected OperationNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}