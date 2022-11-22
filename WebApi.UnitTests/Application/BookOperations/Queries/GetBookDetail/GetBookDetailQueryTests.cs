using System;
using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.GetBooksById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using static WebApi.BookOperations.GetBooksById.GetBookDetailQuery;

namespace WebApi.UnitTests.Application.BookOperations.Queries
{
	public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId= _context.Books.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            FluentActions
                .Invoking(() => query.Handle(query.BookId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Belirtilen Id'ye sahip kitap mevcut değildir.");
        }

        [Fact] 
        public void WhenValidInputAreGiven_Book_ShouldBeReturn()
        {
          
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId= _context.Books.OrderByDescending(x => x.Id).FirstOrDefault().Id;

            FluentActions.Invoking(() => query.Handle(query.BookId)).Invoke();
           
            var book = _context.Books.SingleOrDefault(book => book.Id == query.BookId);
            book.Should().NotBeNull();

        }
    }
}

