using System.Runtime.Serialization;

namespace News.MVC.Models
{
	public class NewsViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		public DateTime PublicationDate { get; set; }
		public DateTime CreationDate { get; set; } 
		public int AuthorId { get; set; }
		public string Author { get; set; }
	}
	
	public class NewsViewModelV2
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		
	}
	public class NewsViewModelV3
	{
		public string Title { get; set; }
		public string Image { get; set; }
		public IFormFile ImageName { get; set; }

		[DataMember]
		public DateTime PublicationDate { get; set; }
		public int AuthorId { get; set; }
		public string? Author { get; set; }
	}
    public class NewsViewModelV4
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public IFormFile ImageName { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
    }

}
