namespace backend.Errors.Exceptions;

public class NotFoundException : Exception
{
    public object? Id { get; }
    
    private NotFoundException(string message, object id) 
        : base(message)
    {
        Id = id;
    }
    
    public static void ThrowFor(object id, Type type)
    {
        throw new NotFoundException($"Entity of type {type.Name} was not found", id);
    }
}