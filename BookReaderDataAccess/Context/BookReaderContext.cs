using BookReaderDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReaderDataAccess.Context;

public class BookReaderContext(DbContextOptions<BookReaderContext> options) : DbContext(options)

{
    public DbSet<BookDetails> BookDetails { get; set; }
    public DbSet<BookContent> BookContents { get; set; }

    public DbSet<BookPicture> BookPictures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
