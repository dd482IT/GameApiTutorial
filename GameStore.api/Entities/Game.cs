using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Entities
{
	public class Game
	{
		public int id { get; set; }
        // Annotations require NuGet Packages
        // Package Management for .NET 
		[Required]
		[StringLength(20)]
        public required string Name { get; set; }
        [Required]
        [StringLength(20)]
        public required string Genre { get; set; }
        [Required]
        [Range(1,1000)]
        public required decimal Price { get; set; }
		public DateTime ReleaseDate { get; set; }
        [Url]
        [StringLength(100)]
        public required string ImageURI { get; set; }

        //Constructor
        public Game()
		{
		}
	}
}

