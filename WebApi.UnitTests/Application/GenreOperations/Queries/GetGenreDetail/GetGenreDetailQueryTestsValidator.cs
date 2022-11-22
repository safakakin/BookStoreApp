using System;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.BookOperations.GetBooksById;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQueryTestsValidator
	{
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            query.GenreId = genreId;
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            query.GenreId = 1;
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}

