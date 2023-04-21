namespace RougeBuilder.Exception;

public class OwnerHasNotDependentComponentException : global::System.Exception
{
    public OwnerHasNotDependentComponentException(string message) : base(message)
    {
    }

    public OwnerHasNotDependentComponentException(string message, global::System.Exception innerException) : base(message, innerException)
    {
    }
}