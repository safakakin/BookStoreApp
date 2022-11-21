using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
	public class Genre
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//Idendity üzerinden oto increment yapar. 

        public int Id { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;
		//default değeri true olarak atanır.

	}
}

