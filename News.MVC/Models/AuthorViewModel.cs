using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace News.MVC.Models
{
	public class AuthorViewModel
	{
		public int? Id { get; set; }
		
		[MaxLength(20)]
		[MinLength(3)]
		public string Name { get; set; }

	}
public class AuthorViewModelv2
	{
		public int? Id { get; set; }
		public string Name { get; set; }

        public string? Order { get; set; }
        public string? filterCharacter { get; set; }

    }
}
