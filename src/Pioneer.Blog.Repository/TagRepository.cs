using System.Collections.Generic;
using System.Linq;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Repository
{
    public interface ITagRepository
    {
        IEnumerable<TagEntity> GetAll();
        IEnumerable<TagEntity> GetAllPaged(int count, int page);
        TagEntity GetById(int id);
        TagEntity Add(TagEntity tagEntity);
        void Update(Tag tag);
        void Remove(int id);
    }

    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _blogContext;

        public TagRepository(BlogDbContext blogContext)
        {
            _blogContext = blogContext;
        }

        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <returns>Tag Collection</returns>
        public IEnumerable<TagEntity> GetAll()
        {
            return _blogContext
                      .Tags
                      .ToList();
        }

        /// <summary>
        /// Get Tag By Id
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <returns>Tag Entity</returns>
        public TagEntity GetById(int id)
        {
            return _blogContext
                    .Tags
                    .FirstOrDefault(x => x.TagId == id);
        }

        /// <summary>
        /// Create Tag
        /// </summary>
        /// <param name="tagEntity">Tag Entity to save</param>
        /// <returns>Tag Entity with Id</returns>
        public TagEntity Add(TagEntity tagEntity)
        {
            _blogContext
                .Tags
                .Add(tagEntity);
            _blogContext.SaveChanges();

            return tagEntity;
        }

        /// <summary>
        /// Update Entity based on Model
        /// </summary>
        /// <param name="tag">Tag Object</param>
        public void Update(Tag tag)
        {
            var entity = _blogContext
                .Tags
                .FirstOrDefault(x => x.TagId == tag.TagId);

            if (entity == null) return;

            entity.Url = tag.Url;
            entity.Name = tag.Name;
            _blogContext.SaveChanges();
        }

        /// <summary>
        /// Remove Tag record based on Id
        /// </summary>
        /// <param name="id">Tag Id</param>
        public void Remove(int id)
        {
            var entity = _blogContext
                .Tags
                .FirstOrDefault(x => x.TagId == id);

            if (entity == null) return;

            _blogContext.Tags.Remove(entity);
            _blogContext.SaveChanges();
        }

        /// <summary>
        /// Get a collection of tags by skipping x and taking y
        /// </summary>
        /// <param name="count">The total number of tags you want to Take</param>
        /// <param name="page">The denomination of tags you want to skip. (page - 1) * count </param>
        /// <returns>Collections of Tags</returns>
        public IEnumerable<TagEntity> GetAllPaged(int count, int page)
        {
            return _blogContext
                    .Tags
                    .Skip((page - 1) * count)
                    .Take(count)
                    .ToList();
        }
    }
}
