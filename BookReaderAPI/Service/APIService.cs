using BookReaderDataAccess.Models;
using BookReaderDataAccess.Repository;
using iTextSharp.text.pdf;

namespace BookReaderAPI.Service
{
    public class APIService(IGenericRepository<BookDetails> repository) : IAPIService
    {
        private readonly IGenericRepository<BookDetails> _repository = repository;

        public string AddBookIfNotExist()
        {
            string message = string.Empty;
            //TODO: This will be removed when the client upload feature is implemented
            string pathToBook = @"D:\The-Simple-Path-to-Wealth.pdf";

            if (File.Exists(pathToBook))
            {
                byte[] bookContent = File.ReadAllBytes(pathToBook);
                PdfReader reader = new(bookContent);
                string title = reader.Info["Title"];
                string authour = reader.Info["Author"];
                BookDetails bookDetails = _repository.GetFirstOrDefault(x => x.Title == title && x.Author == authour);

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

            return string.IsNullOrEmpty(message) ? "Book cannot be add!" : message;
        }
    }
}
