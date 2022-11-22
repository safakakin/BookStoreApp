using System;
using FluentAssertions;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooksById;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries
{
	public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            query.BookId = bookId;
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            query.BookId = 1;

            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}

