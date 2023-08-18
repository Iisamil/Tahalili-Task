using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Data.Config
{
	internal class NewConfigrations : IEntityTypeConfiguration<News.Core.Entity.News>
	{
		public void Configure(EntityTypeBuilder<Core.Entity.News> builder)
		{
			builder.HasOne(P => P.Author).WithMany();
		}
	}
}
