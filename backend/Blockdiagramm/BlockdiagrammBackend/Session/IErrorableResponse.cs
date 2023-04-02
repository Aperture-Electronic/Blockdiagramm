namespace BlockdiagrammBackend.Session
{
    public interface IErrorableResponse
    {
        public bool Success { get; set; }

        public string ErrorReason { get; set; }
    }
}
