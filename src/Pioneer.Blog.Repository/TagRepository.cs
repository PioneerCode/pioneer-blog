using System.Collections.Generic;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using System.Linq;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Repository
{
    public interface ITagRepository
    {
        List<TagEntity> GetAll();
        TagEntity GetById(int id);
        TagEntity Add(TagEntity tagEntity);
        void Update(Tag tag);
        void Remove(int id);
    }

    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _blogContext;

        public TagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <returns>Tag Collection</returns>
        public List<TagEntity> GetAll()
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

            _blogContext.Tags.Remove(entity);
            _blogContext.SaveChanges();
        }
    }
}
