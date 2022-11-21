using System;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommand
	{
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(int _id)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == _id);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");
            if (_context.Authors.Any(x => x.FirstName.ToLower() == Model.FirstName.ToLower() && x.LastName.ToLower() == Model.LastName.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Yazar sistemde mevcuttur.");

            author.FirstName = Model.FirstName == default ? author.FirstName : Model.FirstName;
         
            author.LastName = Model.LastName == default ? author.LastName : Model.FirstName;

            author.BirthDate = Model.BirthDate == default ? author.BirthDate : Model.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

