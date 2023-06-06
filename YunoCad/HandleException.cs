using System.Runtime.Serialization;

namespace YunaComputer.YunoCad;

public abstract class HandleException : Exception
{
    protected HandleException()
    {
    }

    protected HandleException(string? message) : base(message)
    {
    }

    protected HandleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected HandleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

public class DocumentHandleException : HandleException
{
    public DocumentHandleException()
    {
    }

    public DocumentHandleException(string? message) : base(message)
    {
    }

    public DocumentHandleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DocumentHandleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

public class DrawingWindowHandleException : HandleException
{
    public DrawingWindowHandleException()
    {
    }

    public DrawingWindowHandleException(string? message) : base(message)
    {
    }

    public DrawingWindowHandleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DrawingWindowHandleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
