using BookReaderAPI.Service;
using BookReaderDataAccess.Context;
using BookReaderDataAccess.Models;
using BookReaderDataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookReaderContext>(options => options
                .UseSqlite(builder.Configuration
                .GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BookReaderAPI")));

//Service IAPIService
builder.Services.AddScoped<IAPIService, APIService>();

//Repository
builder.Services.AddScoped<IGenericRepository<BookDetails>, GenericRepository<BookDetails>>();
builder.Services.AddScoped<IGenericRepository<BookContent>, GenericRepository<BookContent>>();
builder.Services.AddScoped<IGenericRepository<BookPicture>, GenericRepository<BookPicture>>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/book-reader", (IGenericRepository<BookDetails> repository) =>
{
    IEnumerable<BookDetails> response = repository.GetAll(null, x => x.BookPicture, x => x.BookContent);
    return Results.Ok(response);
});

app.MapGet("/book-reader-add", (IAPIService service) =>
{
    return Results.Ok(service.AddBookIfNotExist());
});

app.Run();

