

using System.Runtime.Serialization;
using News.Core.Entity;

namespace NewsApi.Dtos
{
	public class NewsDto
	{
		public int Id { get; set; }
        public string Title { get; set; }
		public string Image { get; set; }
	}

	public class NewsDtoV2
	{
		public string Title { get; set; }
		public string Image { get; set; }
		
		[DataMember]
		public DateTime PublicationDate { get; set; }
		public int AuthorId { get; set; }
		public string? Author { get; set; }

	}

	public class NewsDtoV3
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		[DataMember]
		public DateTime CreationDate { get; set; }
		[DataMember]
		public DateTime PublicationDate { get; set; }
		public int AuthorId { get; set; }
		public string Author { get; set; }

	}
    public class NewsDtoV4
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
        

    }
}
