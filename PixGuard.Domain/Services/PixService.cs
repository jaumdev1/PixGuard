using Domain.Entities;
using Domain.Enumerables;

namespace Domain.Services;

public class PixService
{
    public bool ValidatePix(Pix pix)
    {
        if (pix == null)
        {
            throw new ArgumentNullException(nameof(pix));
        }


        switch (pix.KeyType)
        {
            case KeyType.CPF:
                return ValidateCPF(pix.KeyValue);
            case KeyType.Email:
                return ValidateEmail(pix.KeyValue);
            case KeyType.PhoneNumber:
                return ValidatePhoneNumber(pix.KeyValue);
            case KeyType.Random:
                return true;
            default:
                return false;
        }
    }

    private bool ValidateCPF(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            return false;
        }

 
        cpf = new string(cpf.Where(char.IsDigit).ToArray());


        if (cpf.Length != 11)
        {
            return false;
        }


        if (cpf.Distinct().Count() == 1)
        {
            return false;
        }

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];
        }

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];
        }

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }


    private bool ValidateEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool ValidatePhoneNumber(string number)
    {
        number = new string(number.Where(char.IsDigit).ToArray());
        
        if (number.Length != 11)
        {
            return false;
        }
        if (number[2] != '9')
        {
            return false;
        }
        return true;
    }
}