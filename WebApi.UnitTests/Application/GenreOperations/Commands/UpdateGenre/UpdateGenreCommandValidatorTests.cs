using System;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.BookOperations.UpdateBooks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static WebApi.BookOperations.UpdateBooks.UpdateBookCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
		[Theory]
        [InlineData("His")]
        [InlineData("")]
        [InlineData(" ")]


        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {

            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = name,
            };
             
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = "History"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

