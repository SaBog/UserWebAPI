namespace UserWebAPI.Exceptions
{
    internal class QueryArgumentException : Exception
    {
        public QueryArgumentException() { }

        public QueryArgumentException(string? message) : base(message)
        {
        }
    }
}