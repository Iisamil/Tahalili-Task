using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Data
{
	public class NewsWebsiteDbContext : IdentityDbContext<WebUser>
	{
        public NewsWebsiteDbContext(DbContextOptions<NewsWebsiteDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		//public DbSet<WebUser> users { get; set; }
		//public DbSet<WebUser> WebUsers { get; set; }
		public DbSet<Author> authors { get; set; }
		public DbSet<News.Core.Entity.News> news { get; set; }


	}
}
