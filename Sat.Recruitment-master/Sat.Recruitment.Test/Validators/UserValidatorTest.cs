using Sat.Recruitment.Api.Validations;
using Sat.Recruitment.Test.Mocks;
using System;
using FluentAssertions;

namespace Sat.Recruitment.Test.Validators
{
    public class UserValidatorTest
    {
        [Theory]
        [InlineData("John", "123 Main St", "john@example.com", "+1234567890", 101, 1, true)] // Caso válido
        [InlineData("", "123 Main St", "john@example.com", "+1234567890", 101, 1, false)] // Nombre vacío
        [InlineData("John", "", "john@example.com", "+1234567890", 101, 1, false)] // Dirección vacía
        [InlineData("John", "123 Main St", "", "+1234567890", 101, 1, false)] // Email vacío
        [InlineData("John", "123 Main St", "john@example.com", "12345", 101, 1, false)] // Teléfono inválido
        [InlineData("John", "123 Main St", "no-email", "+1234567890", 101, 1, false)] // Email inválido
        [InlineData("John", "123 Main St", "john@example.com", "+1234567890123456", 101, 1, false)] // Teléfono demasiado largo
        public void ValidateMessageTest(string name, string address, string email, string phone, int money, int userType, bool isValid)
        {
            var dto = DtoMockCreator.CreateUserDto(name, address, email, phone, money, userType);

            var validator = new UserValidator();
            validator.Validate(dto).IsValid.Should().Be(isValid);
        }
    }
}