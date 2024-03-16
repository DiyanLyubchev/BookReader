namespace BookReaderAPI.Models.Response
{
    public class BookDetailsResponse
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }

        public byte[] Picture { get; set; }
    }
}
