namespace LeadForgeCrm.Api.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }
        public object? Errors { get; init; }
    }
}
