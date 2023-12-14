using System;
using System.ComponentModel.DataAnnotations;
using Domain.Exceptions;
namespace Domain.DTOs.Validations;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ValidEnumValueAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public ValidEnumValueAttribute(Type enumType)
    {
        if (enumType == null || !enumType.IsEnum)
        {
            throw new ArgumentException("O tipo fornecido deve ser uma enumeração.");
        }

        _enumType = enumType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null && Enum.IsDefined(_enumType, value))
        {
            return ValidationResult.Success;
        }

        var displayName = validationContext.DisplayName;
        var errorMessage = $"{displayName} inválido para {_enumType.Name}.";

        throw new InvalidEnumValueException(errorMessage);
    }
}