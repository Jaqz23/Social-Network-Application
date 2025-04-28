namespace RS.Core.Application.Wrappers
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public OperationResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public static OperationResult Ok(string? message = null) => new OperationResult(true, message);
        public static OperationResult Fail(string message) => new OperationResult(false, message);
    }
}
