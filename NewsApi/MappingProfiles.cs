using AutoMapper;
using NewsApi.Dtos;

namespace NewsApi
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{ 
			CreateMap<News.Core.Entity.News,NewsDto>().ReverseMap();
			
			CreateMap<News.Core.Entity.News,NewsDtoV3>().
				ForMember(D=>D.Author,O=>O.MapFrom(S=>S.Author.Name)).ReverseMap();
		}
	}
}
