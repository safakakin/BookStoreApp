using System;
using FluentValidation;
using WebApi.BookOperations.UpdateBooks;

namespace WebApi.BookOperations.GetBooksById
{
	public class GetBookDetailQueryValidator: AbstractValidator<GetBookDetailQuery>
    {
		public GetBookDetailQueryValidator()
		{
			RuleFor(query => query.BookId).GreaterThan(0);
		}
	}
}

