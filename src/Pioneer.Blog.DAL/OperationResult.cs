namespace Pioneer.Blog.DAL
{
    public enum OperationStatus
    {
        Ok,
        Created,
        Updated,
        NotFound,
        Deleted,
        NothingModified,
        Error
    }

    public class OperationResult<T> where T : class
    {
        public T Entity { get; }

        public OperationStatus Status { get; }

        public string Message { get; }

        public OperationResult(T entity, OperationStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public OperationResult(T entity, OperationStatus status, string message) : this(entity, status)
        {
            Message = message;
        }
    }
}
