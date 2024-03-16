using BookReaderDataAccess.Models;
using BookReaderDataAccess.Repository;
using iTextSharp.text.pdf;

namespace BookReaderAPI.Service
{
    public class APIService(IGenericRepository<BookDetails> repository) : IAPIService
    {
        private readonly IGenericRepository<BookDetails> _repository = repository;

        public string GetPictureFromPDF()
        {
            string pathToBook = @"D:\Common-Sense-Investing.pdf";

            if (File.Exists(pathToBook))
            {
                PdfReader reader = new(File.ReadAllBytes(pathToBook));
                string title = reader.Info["Title"];
                //  _repository.GetFirstOrDefault(x => x.Title == reader)
                for (var pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
                {
                    byte[] contentBytes = reader.GetPageContent(pageNum);
                    PrTokeniser tokenizer = new(new RandomAccessFileOrArray(contentBytes));

                    List<string> stringsList = new();
                    while (tokenizer.NextToken())
                    {
                        if (tokenizer.TokenType == PrTokeniser.TK_STRING)
                        {
                            stringsList.Add(tokenizer.StringValue);
                        }
                    }

                    // Print the set of string tokens, one on each line.
                    Console.WriteLine(string.Join("\r\n", stringsList));
                }
            }
            return null;
        }
    }
}
