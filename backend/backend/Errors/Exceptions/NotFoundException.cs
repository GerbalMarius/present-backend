namespace backend.Errors.Exceptions;

public class NotFoundException : Exception
{
    public object? Id { get; }
    
    private NotFoundException(string message, object id) 
        : base(message)
    {
        Id = id;
    }
    
    public static NotFoundException NotFound(object id, Type type)
    {
        return new NotFoundException($"Entity of type {type.Name} was not found", id);
    }
}