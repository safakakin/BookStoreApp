using System;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using WebApi.BookOperations.CreateBooks;
using Xunit;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using FluentAssertions;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommandTestsValidator
	{
        [Theory]
        [InlineData("Los", "Lo")]
        [InlineData("Los", "")]
        [InlineData("Los", " ")]
        [InlineData("Lo", "Los")]
        [InlineData("", "Los")]
        [InlineData(" ", "Los")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName )
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = DateTime.Now.Date.AddYears(-10),
                
            };
            //act 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBirthDateEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Şafak",
                LastName = "Akın",
                BirthDate = DateTime.Now.Date,
                
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Şafak",
                LastName = "Akın",
                BirthDate = DateTime.Now.Date.AddYears(-10),
                
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

