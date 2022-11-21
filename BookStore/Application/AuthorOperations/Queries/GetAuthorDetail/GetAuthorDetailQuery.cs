﻿using System;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQuery
	{
        public int AuthorId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
          
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");
            return _mapper.Map<AuthorDetailViewModel>(author);

        }

        public class AuthorDetailViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}

