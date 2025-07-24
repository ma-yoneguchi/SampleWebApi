namespace SampleWebApi.Exceptions
{
    public class UniqueConstraintException : BusinessException
    {
        public string[] ConstraintFields { get; }
        public Dictionary<string, object> ConstraintValues { get; }

        public UniqueConstraintException(string tableName, string[] constraintFields, Dictionary<string, object> constraintValues)
            : base($"{tableName}テーブルでユニーク制約違反が発生しました。", "UNIQUE_CONSTRAINT_VIOLATION")
        {
            ConstraintFields = constraintFields;
            ConstraintValues = constraintValues;
        }
    }
}
