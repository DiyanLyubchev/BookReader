namespace BookReaderDataAccess.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public int BookContentId { get; set; }
        public BookContent BookContent { get; set; }
        public int BookPictureId { get; set; }
        public BookPicture BookPicture { get; set; }
    }
}
