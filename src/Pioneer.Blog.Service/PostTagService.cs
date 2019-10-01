using AutoMapper;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface IPostTagService
    {
        PostTag Add(PostTag postTag);
        void Delete(PostTag postTag);
    }

    public class PostTagService : IPostTagService
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly IMapper _mapper;

        public PostTagService(IPostTagRepository postTagRepository, IMapper mapper)
        {
            _postTagRepository = postTagRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a new PostTag record
        /// </summary>
        /// <param name="postTag">Compound Key</param>
        /// <returns>Qualified PostTag</returns>
        public PostTag Add(PostTag postTag)
        {
            var entity = _postTagRepository.Add(_mapper.Map<PostTag, PostTagEntity>(postTag));
            postTag.PostTagId = entity.PostTagId;
            return postTag;
        }

        /// <summary>
        /// Delete a PostTag by compound key
        /// </summary>
        /// <param name="postTag">Compound Key</param>
        public void Delete(PostTag postTag)
        {
            _postTagRepository.RemoveByCompound(_mapper.Map<PostTag, PostTagEntity>(postTag));
        }
    }
}
