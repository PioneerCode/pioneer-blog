using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
        IEnumerable<Tag> GetAllPaged(int count, int page = 1);
        string GetTagNameFromTagUrlInTagCollection(string tagUrl, List<Tag> tags);
        Tag GetById(int id);
        Tag Add(Tag tag);
        void Update(Tag tag);
        void Remove(int id);
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
        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll()
                .Select(Mapper.Map<TagEntity, Tag>)
                .OrderBy(x => x.Name)
                .ToList();
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

        /// <summary>
        /// Get Tag by id
        /// </summary>
        /// <param name="id">Tag id</param>
        /// <returns>Tag Object</returns>
        public Tag GetById(int id)
        {
            return Mapper.Map<TagEntity, Tag>(_tagRepository.GetById(id));
        }

        /// <summary>
        /// Create Tag record
        /// If a Name is not provided, set Name and URL to a GUID
        /// </summary>
        /// <param name="tag">Tag</param>
        public Tag Add(Tag tag)
        {
            if (tag.Name == null)
            {
                tag.Name = Guid.NewGuid().ToString();
                tag.Url = tag.Name;
            }

            var response = _tagRepository.Add(Mapper.Map<Tag, TagEntity>(tag));
            tag.TagId = response.TagId;
            return tag;
        }

        /// <summary>
        /// Update Tag record
        /// </summary>
        /// <param name="tag">Updated Tag</param>
        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }

        /// <summary>
        /// Delete Tag record based on id
        /// </summary>
        /// <param name="id">Tag Id</param>
        public void Remove(int id)
        {
            _tagRepository.Remove(id);
        }

        /// <summary>
        /// Get paged collection of tags
        /// </summary>
        /// <param name="count">Number of tags in page</param>
        /// <param name="page">Page of tags</param>
        /// <returns>Count of tags starting at page</returns>
        public IEnumerable<Tag> GetAllPaged(int count = 10, int page = 1)
        {
            return _tagRepository.GetAllPaged(count, page).Select(Mapper.Map<TagEntity, Tag>);
        }
    }
}