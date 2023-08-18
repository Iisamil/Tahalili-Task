using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
	public class AuthorWithSpecifications : Specification<Author>
	{
		public AuthorWithSpecifications(string? Order)
		{
			if (Order == null) AddOrderBy(p => p.Name);
			else
			{
				switch (Order.ToLower())
				{
					case "nameasc":
						AddOrderBy(p => p.Name);
						break;
					case "namedesc":
						AddOrderByDecsending(p => p.Name);
						break;
				}

			}
		}
		public AuthorWithSpecifications(string? Order, char? filterCharacter) : base(p => p.Name.FirstOrDefault() == filterCharacter)
		{
			if (Order == null) AddOrderBy(p => p.Name);
			else
			{
				switch (Order.ToLower())
				{
					case "nameasc":
						AddOrderBy(p => p.Name);
						break;
					case "namedesc":
						AddOrderByDecsending(p => p.Name);
						break;
				}

			}
		}

		
	} 
}

