using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Service
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Article, ArticleEntity>().ReverseMap();
            CreateMap<Category, CategoryEntity>().ReverseMap();
            CreateMap<Contact, ContactEntity>().ReverseMap();
            CreateMap<Excerpt, ExcerptEntity>().ReverseMap();
            CreateMap<PostTag, PostTagEntity>().ReverseMap();
            CreateMap<Tag, TagEntity>().ReverseMap();
            CreateMap<PostEntity, Post>()
                .ForMember(dto => dto.Tags, opt => opt.MapFrom(x => x.PostTags.Select(t => t.Tag))).ReverseMap();
            CreateMap<PostTagEntity, Tag>().ReverseMap();
            CreateMap<List<PostEntity>, List<Post>>().ReverseMap();
        }
    }
}
