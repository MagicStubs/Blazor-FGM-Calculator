using System.Runtime.Serialization;

namespace FGM.Data.Exceptions;

public class InvalidFileTypeException : Exception
{
    public InvalidFileTypeException() 
        : base("The file type provided is invalid.")
    {
    }

    public InvalidFileTypeException(string message) 
        : base(message)
    {
    }

    public InvalidFileTypeException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }

    protected InvalidFileTypeException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}