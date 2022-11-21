using System;
using FluentValidation;
using WebApi.BookOperations.GetBooksById;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {
	
		public GetGenreDetailQueryValidator()
		{
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
	}
}

