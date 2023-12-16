namespace Domain.Exceptions;

public class UserExistsException : Exception
{
public UserExistsException() { }

public UserExistsException(string mensagem) : base(mensagem) { }

public UserExistsException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}
