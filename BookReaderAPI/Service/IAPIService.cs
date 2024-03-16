using BookReaderAPI.Models.Response;

namespace BookReaderAPI.Service
{
    public interface IAPIService
    {
        string AddBookIfNotExist(byte[] bookContent);
        List<BookDetailsResponse> GetAllBookDetails();
        byte[] GetBookContentById(int id);
        void DeleteById(int id);
    }
}