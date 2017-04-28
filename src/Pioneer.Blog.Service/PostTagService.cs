using AutoMapper;
using Pioneer.Blog.DAL.Entites;
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

        public PostTagService(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        public PostTag Add(PostTag postTag)
        {
            var entity = _postTagRepository.Add(Mapper.Map<PostTag, PostTagEntity>(postTag));
            postTag.PostTagId = entity.PostTagId;
            return postTag;
        }

        public void Delete(PostTag postTag)
        {
            _postTagRepository.RemoveByCompound(Mapper.Map<PostTag, PostTagEntity>(postTag));
        }
    }
}
