using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.BookOperations.UpdateBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;


namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = _context.Authors.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            FluentActions
                .Invoking(() => command.Handle(command.AuthorId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        [Fact]
        public void WhenGivenAuthorIsAlreadyExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId= 2;
            command.Model.FirstName = "Nazım Hikmet";
            command.Model.LastName = "Ran";
            command.Model.BirthDate = new DateTime(1902, 01, 15);


            FluentActions
                .Invoking(() => command.Handle(command.AuthorId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar sistemde mevcuttur.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeUpdated()
        {
            
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel() { FirstName = "Şafak", LastName="Akın", BirthDate= new DateTime(1922, 01, 15) };
            command.Model = model;
            command.AuthorId = 1;

            FluentActions.Invoking(() => command.Handle(command.AuthorId)).Invoke();
           
            var author = _context.Authors.SingleOrDefault(author => author.FirstName == model.FirstName && author.FirstName == model.FirstName);
            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.BirthDate.Should().Be(model.BirthDate);

        }
    }
}

