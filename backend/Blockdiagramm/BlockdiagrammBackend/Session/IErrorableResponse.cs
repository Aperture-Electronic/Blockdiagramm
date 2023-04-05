namespace BlockdiagrammBackend.Session
{
    public interface IErrorableResponse
    {
        public bool Success { get; }

        public string ErrorReason { get; }
    }
}
