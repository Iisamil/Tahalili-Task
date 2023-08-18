using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
	public class Specification<T> : ISpecification<T> where T : BasEntity
	{
		
		public Expression<Func<T, bool>> Criteria { get; set; }
		
		public Expression<Func<T, object>> OrderBy { get; set; }

		public Expression<Func<T, object>> OrderByDecsending { get; set; }

		public Expression<Func<T, object>> Includes { get; set; }


		public Specification()
        {
            
        }

        public Specification(Expression<Func<T, bool>> criteriaExpression)
		{
			Criteria = criteriaExpression;
		}

		public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		public void AddOrderByDecsending(Expression<Func<T, object>> orderByDecsendingExpression)
		{
			OrderByDecsending = orderByDecsendingExpression;
		}
	}
}
