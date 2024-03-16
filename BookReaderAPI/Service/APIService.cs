using BookReaderAPI.Models.Response;
using BookReaderDataAccess.Models;
using BookReaderDataAccess.Repository;
using iTextSharp.text.pdf;

namespace BookReaderAPI.Service
{
    public class APIService(IGenericRepository<BookDetails> repository) : IAPIService
    {
        private readonly IGenericRepository<BookDetails> _repository = repository;

        public List<BookDetailsResponse> GetAllBookDetails()
        {

            List<BookDetailsResponse> booksDetails = _repository.GetAll(filter: null, includeProperties: x => x.BookPicture)
                                                                .Select(book => new BookDetailsResponse
                                                                {
                                                                    Author = book.Author,
                                                                    Pages = book.Pages,
                                                                    Title = book.Title,
                                                                    Picture = book.BookPicture.Picture
                                                                })
                                                                .ToList();

            return booksDetails;
        }

        public string AddBookIfNotExist(byte[] bookContent)
        {
            string message = string.Empty;
            //TODO: This will be removed when the client upload feature is implemented
            string pathToBook = @"D:\The-Simple-Path-to-Wealth.pdf";
            try
            {
                bookContent = File.ReadAllBytes(pathToBook);
            }
            catch (Exception)
            {
            }

            if (bookContent != null && bookContent.Length > 0) 
            {
                PdfReader reader = new(bookContent);
                string title = reader.Info["Title"];
                string authour = reader.Info["Author"];
                BookDetails bookDetails = _repository.GetFirstOrDefault(filter: x => x.Title == title && x.Author == authour);

                if (bookDetails == null)
                {
                    message = $"Book {title} from authour: {authour} was added successfully!";
                    byte[] pictureBytes = reader.GetPageContent(1);
                    bookDetails = new()
                    {
                        Title = title,
                        Pages = reader.NumberOfPages,
                        Author = authour,
                        BookPicture = new() { Picture = pictureBytes },
                        BookContent = new() { Content = bookContent }
                    };

                    _repository.InsertWithSave(bookDetails);
                }
                else
                    message = $"Book with title {title} and from authour: {authour} already exists!";
            }

            return string.IsNullOrEmpty(message) ? "The Book cannot be added!" : message;
        }

        public byte[] GetBookContentById(int id)
        {
            return _repository.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: x => x.BookContent).BookContent.Content;
        }

        public void DeleteById(int id)
        {
            _repository.DeleteWithSave(id);
        }
    }
}
