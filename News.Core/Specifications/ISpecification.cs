using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
	public interface ISpecification<T> where T : BasEntity
	{
		// Property to WhereCondition
		public Expression<Func<T, bool>> Criteria { get; set; }

		// Property to Sort OrderBy
		public Expression<Func<T, object>> OrderBy { get; set; }

		// Property to Sort OrderByDecsending
		public Expression<Func<T, object>> OrderByDecsending { get; set; }

		// Property to Include()
		public Expression<Func<T, object>> Includes { get; set; }
	}
}
