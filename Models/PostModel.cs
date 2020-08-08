namespace Models
{
    public class PostModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return Title + '%' + Text;
        }
    }
}
