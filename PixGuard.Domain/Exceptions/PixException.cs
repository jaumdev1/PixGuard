namespace Domain.Exceptions;

public class PixException : Exception
{
public PixException() { }

public PixException(string mensagem) : base(mensagem) { }

public PixException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}
