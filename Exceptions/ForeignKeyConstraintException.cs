namespace SampleWebApi.Exceptions
{
    public class ForeignKeyConstraintException : BusinessException
    {
        public string ReferencedTable { get; }
        public object ReferencedId { get; }

        public ForeignKeyConstraintException(string message, string referencedTable, object referencedId)
            : base(message, "FOREIGN_KEY_CONSTRAINT_VIOLATION")
        {
            ReferencedTable = referencedTable;
            ReferencedId = referencedId;
        }
    }
}
