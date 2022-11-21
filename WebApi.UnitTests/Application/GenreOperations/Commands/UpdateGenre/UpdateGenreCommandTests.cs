using System;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.BookOperations.UpdateBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static WebApi.BookOperations.UpdateBooks.UpdateBookCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = _context.Books.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }

        [Fact]
        public void WhenGivenGenreIsAlreadyExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.Model.Name = "Personel Growth";
            command.Model.IsActive = true;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimle bir kitap türü zaten mevcut.");
        }

        [Fact] //Happy Path
        public void WhenValidInputAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { Name="History" };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(model.IsActive);
        }
    }
}

