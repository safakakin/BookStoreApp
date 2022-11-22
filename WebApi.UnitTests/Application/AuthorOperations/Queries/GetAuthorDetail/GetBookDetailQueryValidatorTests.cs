using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQueryValidatorTests
	{
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            query.AuthorId = authorId;
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            query.AuthorId = 1;
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}

