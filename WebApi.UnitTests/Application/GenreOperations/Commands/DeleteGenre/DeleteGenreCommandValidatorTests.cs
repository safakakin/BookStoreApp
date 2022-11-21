using System;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.BookOperations.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            command.GenreId = genreId;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            command.GenreId = 1;

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

