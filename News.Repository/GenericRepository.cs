using Microsoft.EntityFrameworkCore;
using News.Core.Entity;
using News.Core.Repos;
using News.Core.Specifications;
using News.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
	public class GenericRepository<T> : IGenericRepository<T>,IDisposable where T : BasEntity
	{
		private readonly NewsWebsiteDbContext _dbContext;

		public GenericRepository(NewsWebsiteDbContext dbContext) // Ask The CLR For Create Object From NewsWebsiteDbContext
		{
			_dbContext = dbContext;
		}
		public async Task Add(T entity)
			=> await _dbContext.Set<T>().AddAsync(entity);	
		

		public void Delete(T entity)
			=> _dbContext.Set<T>().Remove(entity);

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			var query =SpecificationEvalutor<T>.QuaryBuilder(_dbContext.Set<T>(), spec);
			return await query.ToListAsync();
		}

		public async Task<T> FindEntityWithSpec(ISpecification<T> spec)
		{
			var query = SpecificationEvalutor<T>.QuaryBuilder(_dbContext.Set<T>(), spec);

			return await query.FirstOrDefaultAsync();
		}

		public async Task<T> FindEntityById(int Id)
		{
			return await _dbContext.Set<T>().FindAsync(Id);
		}
		public void Update(T entity)
			=> _dbContext.Set<T>().Update(entity);

		public async Task UpdateDB()
			=> await _dbContext.SaveChangesAsync();
		public void Dispose()
			=> _dbContext.Dispose();

	

	}
}
