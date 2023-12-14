using Domain.Entities;
using Domain.Services;
using Domain.Enumerables;
using System.ComponentModel.DataAnnotations;
namespace PixGuard.Tests.Domain;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void Pix_MustHavePropertyPixIdWithAttributeKey()
    {

        var propertyInfo = typeof(Pix).GetProperty("Id");


        var keyAttribute = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true);


        Assert.IsNotNull(keyAttribute);
        Assert.IsTrue(keyAttribute.Length > 0);
    }

    [TestCase("KeyType")]
    [TestCase("KeyValue")]
    [Test]
    public void Pix_RequiredProperities(string propertyName)
    {

        var propertyInfo = typeof(Pix).GetProperty(propertyName);


        var requiredAttributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true);


        Assert.IsNotNull(requiredAttributes);
        Assert.IsTrue(requiredAttributes.Length > 0);
    }

    [Test]
    public void ValidatePix_NullPix_ThrowsArgumentNullException()
    {
        
        var pixService = new PixService();

   
        Assert.Throws<ArgumentNullException>(() => pixService.ValidatePix(null));
    }

    [TestCase(KeyType.CPF, "123.456.789-09")]
    [TestCase(KeyType.Email, "test@example.com")]
    [TestCase(KeyType.PhoneNumber, "(31) 98765-4321")]
    [TestCase(KeyType.Random, "randomValue")]
    [Test]
    public void ValidatePix_ValidPix_ReturnsTrue(KeyType keyType, string keyValue)
    {
       
        var pixService = new PixService();
        var pix = new Pix
        {
            KeyType = keyType,
            KeyValue = keyValue,
            UserId = null,

        };
        
        bool result = pixService.ValidatePix(pix);
        
        Assert.IsTrue(result);
    }

    [TestCase(KeyType.CPF, "123.456.789-01")]
    [TestCase(KeyType.Email, "invalid_email")]
    [TestCase(KeyType.PhoneNumber, "(31) 12345-6789")]
    [Test]
    public void ValidatePix_InvalidPix_ReturnsFalse(KeyType keyType, string keyValue)
    {
    
        var pixService = new PixService();
        var pix = new Pix
        {
            KeyType = keyType,
            KeyValue = keyValue,
            UserId = null
        };
        
        bool result = pixService.ValidatePix(pix);

       
        Assert.IsFalse(result);
    }
    
    
    [Test]
    public void Pix_WhenAllPropertiesAreValid_ShouldPassValidation()
    {
        // Arrange
        var pix = new Pix
        {
            KeyType = KeyType.PhoneNumber,
            KeyValue = "(31) 99344-0434",
            UserId = Guid.NewGuid(),
           
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(pix, new ValidationContext(pix), validationResults, true);

        // Assert
        Assert.IsTrue(isValid, "Pix object should pass validation");
        Assert.IsEmpty(validationResults, "Validation results should be empty");
    }
    [Test]
    public void Pix_WhenUserNotProvidedButUserIdIs_ShouldFailValidation()
    {
       
        var pix = new Pix
        {
            KeyType = KeyType.PhoneNumber,
            UserId = Guid.NewGuid()
        };
        
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(pix, new ValidationContext(pix), validationResults, true);
        Assert.IsFalse(isValid, "Pix object should fail validation when User is not provided but KeyValue is");
        Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains(nameof(Pix.KeyValue))), "Validation result should contain error for KeyValue");
    }
}