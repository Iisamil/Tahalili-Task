using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Entity
{
	public class News :BasEntity
	{
		public string Title { get; set; }
		public string Image { get; set; }
		//public DateTime PublicationDate { get; set; }

		private DateTime PublicationDate;
		public DateTime publicationDate
		{
			get { return PublicationDate; }
			set { PublicationDate = value >= CreationDate && value < CreationDate.AddDays(7) ? value : CreationDate.AddDays(7); }
		}

		public DateTime CreationDate { get; set; } = DateTime.Now;
		public int AuthorId { get; set; }
		public Author Author { get; set; }
	}


}
