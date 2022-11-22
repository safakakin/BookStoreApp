using System;
using BookStore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
	public static class Authors
	{
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author { FirstName = "Nazım Hikmet", LastName = "Ran", BirthDate = new DateTime(1902, 01, 15) },
                    new Author { FirstName = "Necip Fazıl", LastName = "Kısakürek", BirthDate = new DateTime(1904, 05, 26) },
                    new Author { FirstName = "Kemal Sadık", LastName = "Gökçeli", BirthDate = new DateTime(1923, 10, 06) });
        }
    }
}

