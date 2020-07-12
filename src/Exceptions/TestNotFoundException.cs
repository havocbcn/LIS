
namespace LIS.Exceptions {
    [System.Serializable]
    public class TestNotFoundException : System.Exception
    {
        public TestNotFoundException() { }
        public TestNotFoundException(string message) : base(message) { }
        public TestNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected TestNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}