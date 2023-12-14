namespace Domain.Exceptions;


public class InvalidEnumValueException : Exception
{
    public InvalidEnumValueException() { }

    public InvalidEnumValueException(string mensagem) : base(mensagem) { }

    public InvalidEnumValueException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}
