using Models;

namespace ChatPublisher.PostBuilder
{
    public interface IPostBuilder
    {
        public PostModel BuildModel(string message);
    }
}
