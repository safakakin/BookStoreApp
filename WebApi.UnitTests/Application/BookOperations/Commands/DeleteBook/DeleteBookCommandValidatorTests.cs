using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            command.BookId = bookId;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            command.BookId = 1;
            
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
