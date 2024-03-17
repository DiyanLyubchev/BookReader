using BookReaderAPI.Models.Response;
using BookReaderDataAccess.Models;

namespace BookReaderAPI.Service;

public interface IAPIService
{
    string AddBookIfNotExist(string bookContentBase64);
    List<BookDetailsResponse> GetAllBookDetails();
    byte[] GetBookContentById(int id);
    void DeleteById(int id);

    BookDetails GetBookById(int id);
}