namespace CleanArchitecture.Infrastructure.MicrosoftSql.Exceptions
{
    public class ConcurrencyException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
