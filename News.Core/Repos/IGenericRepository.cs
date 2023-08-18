using News.Core.Entity;
using News.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repos
{
	public interface IGenericRepository <T> where T : BasEntity
	{
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
		Task Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		public Task UpdateDB();
		public void Dispose();
		public Task<T> FindEntityWithSpec(ISpecification<T> spec);
		public Task<T> FindEntityById(int Id);
	}
}
