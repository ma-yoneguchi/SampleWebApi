namespace SampleWebApi.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }
        public string ErrorCode { get; }

        public ValidationException(Dictionary<string, string[]> errors) : base("バリデーションエラーが発生しました。")
        {
            Errors = errors;
            ErrorCode = "VALIDATION_ERROR";
        }

        public ValidationException(string field, string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { message } }
            };
            ErrorCode = "VALIDATION_ERROR";
        }

        public ValidationException(string message, string field, string errorCode) : base(message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { message } }
            };
            ErrorCode = errorCode;
        }
    }
}
