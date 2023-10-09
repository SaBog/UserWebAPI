namespace UserWebAPI.Exceptions
{
    internal class RoleNotFoundException : Exception
    {
        public RoleNotFoundException() { }

        public RoleNotFoundException(string? message) : base(message)
        {
        }
    }
}