using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
	public class NewsWithSpecifications : Specification<News.Core.Entity.News>
	{
		public NewsWithSpecifications(string? Order, string? Author, string? title) : base 
			(P=>
			(string.IsNullOrEmpty(Author) || P.Author.Name.ToLower()==Author.ToLower()) &&
			(string.IsNullOrEmpty(title) || P.Title.ToLower() == title.ToLower())
			)
		{
			Includes = p => p.Author;
			if (!string.IsNullOrEmpty(Order))
			{
				switch (Order.ToLower())
				{
					case "titleasc":
						AddOrderBy(P => P.Title);
						break;
					case "titledesc":
						AddOrderByDecsending(P => P.Title);
						break;
					case "authorasc":
						AddOrderBy(P => P.Author.Name);
						break;
					case "authordesc":
						AddOrderByDecsending(P => P.Author.Name);
						break;
					default:
						AddOrderBy(P => P.Title);
						break;
				}
			}
			else
				AddOrderBy(P => P.Title);
		}
        public NewsWithSpecifications(int id) : base(p=>p.Id==id)
		{
            Includes = p => p.Author;
        }


    }
}
