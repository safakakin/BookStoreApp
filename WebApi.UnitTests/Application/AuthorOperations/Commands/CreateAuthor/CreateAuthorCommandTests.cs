using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.BookOperations.CreateBooks;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author { FirstName = "Nazım Hikmet", LastName = "Ran", BirthDate = new DateTime(1902, 01, 15) };

            _context.Authors.Add(author);

            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() { FirstName = author.FirstName, LastName=author.LastName};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar sistemde zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() { FirstName = "Şafak", LastName = "Akın", BirthDate = DateTime.Now.AddYears(-10)};
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();
     
            var author = _context.Authors.SingleOrDefault(author => author.FirstName == model.FirstName && author.LastName==model.LastName);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}
