namespace UserWebAPI.Exceptions
{
    public class UserValidationModelException : Exception
    {
        public UserValidationModelException() { }

        public UserValidationModelException(string? message) : base(message)
        {
        }
    }
}
