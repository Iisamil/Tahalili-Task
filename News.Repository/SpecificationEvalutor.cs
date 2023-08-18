using Microsoft.EntityFrameworkCore;
using News.Core.Entity;
using News.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
	public static class SpecificationEvalutor <T> where T : BasEntity
	{
		public static IQueryable<T> QuaryBuilder(IQueryable<T> inputQuary, ISpecification<T> spec)
		{
			var query = inputQuary;  // Here we Get The Context And Table That We Want To Make The Query For
									 // Context.TableName
			
			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria); // Here We Make Filtertion 
													// Context.TableName.where(C=>C.Name.First().Lower == a)

			if (spec.OrderBy is not null) 
				query = query.OrderBy(spec.OrderBy); // Here We Order By DESC Or ASC 
													 // Context.TableName.where(C=>C.Name.First().Lower == a).OrderBy(O=>O.Name);

			if (spec.OrderByDecsending is not null)
				query = query.OrderByDescending(spec.OrderByDecsending);

			if (spec.Includes is not null)
				query = query.Include(spec.Includes); // Here We Include Or We Load Table If We Want To
													

			return query; // Finally We Return The Query 
		}
	}
}