using System;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = _context.Genres.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek tür bulunamadı.");
        }

        public void WhenValidIdIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = _context.Books.OrderByDescending(x => x.Id).FirstOrDefault().Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Books.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().BeNull();

        }

    }
}

