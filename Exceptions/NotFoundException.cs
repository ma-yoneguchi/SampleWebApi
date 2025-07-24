namespace SampleWebApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public string? ResourceType { get; }
        public object? ResourceId { get; }
        public string ErrorCode { get; }

        public NotFoundException(string message) : base(message)
        {
            ErrorCode = "NOT_FOUND";
        }

        public NotFoundException(string resourceType, object resourceId)
            : base($"{resourceType} (ID: {resourceId}) が見つかりません。")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
            ErrorCode = "RESOURCE_NOT_FOUND";
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = "NOT_FOUND";
        }
    }
}
