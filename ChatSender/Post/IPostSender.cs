using Models;

namespace ChatSender.Post
{
    public interface IPostPublisher
    {
        public void Publish(PostModel postModel);
    }
}
