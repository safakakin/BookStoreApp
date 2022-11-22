using System;
using AutoMapper;
using BookStore;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommand
	{
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName==Model.LastName);

            if (author is not null)
                throw new InvalidOperationException("Yazar sistemde zaten mevcut.");
            author = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

        }

        public class CreateAuthorModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }

    }
}

