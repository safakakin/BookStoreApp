using System;
using BookStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
	public class DataGenerator
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			{
				if (context.Books.Any())
				{
					return;
				}

				context.Genres.AddRange(
					new Genre { Name = "Personel Growth" },
					new Genre { Name = "Science Fiction" },
					new Genre { Name = "Romance" });


				context.Books.AddRange(
				 new Book { /* Id = 1,*/ Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
				 new Book { /*Id = 2,*/ Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
				 new Book { /*Id = 3,*/ Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2008, 12, 21) });

				context.Authors.AddRange(
					new Author { FirstName="Nazım Hikmet",LastName="Ran",BirthDate=new DateTime(1902,01,15)},
                    new Author { FirstName = "Necip Fazıl", LastName = "Kısakürek", BirthDate = new DateTime(1904, 05, 26)},
                    new Author { FirstName = "Kemal Sadık", LastName = "Gökçeli", BirthDate = new DateTime(1923, 10, 06) });
				context.SaveChanges();
			}
		}
	}
}

