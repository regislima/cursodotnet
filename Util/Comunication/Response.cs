namespace api.Util.Comunication
{
    public class Response<T> : BaseResponse
    {
        public T Answer { get; private set; }

        public Response(bool success, string message, T entity) : base(success, message)
        {
            Answer = entity;
        }

        public Response(T entity) : this(true, string.Empty, entity) { }

        public Response(string message) : this(false, message, default(T)) { }
    }
}