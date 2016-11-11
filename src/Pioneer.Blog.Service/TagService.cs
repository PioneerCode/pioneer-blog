using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ITagService
    {
        List<Tag> GetAll();

        string GetTagNameFromTagUrlInTagCollection(string tagUrl, List<Tag> tags);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>Collection of tags</returns>
        public List<Tag> GetAll()
        {
            return _tagRepository.GetAll().Select(Mapper.Map<TagEntity, Tag>).ToList();
        }

        /// <summary>
        /// Get a tag name, based on tagUrl, from a collection tags
        /// </summary>
        /// <param name="tagUrl">tag url to find match against</param>
        /// <param name="tags">Collection of tags to search for match</param>
        /// <returns>Tag name</returns>
        public string GetTagNameFromTagUrlInTagCollection(string tagUrl, List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                if (tagUrl != tag.Url) continue;
                return tag.Name;
            }
            return "";
        }
    }
}