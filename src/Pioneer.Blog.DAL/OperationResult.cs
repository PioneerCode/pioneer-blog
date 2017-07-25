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
        public T Entity { get; private set; }

        public OperationStatus Status { get; private set; }

        public string Message { get; private set; }

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
