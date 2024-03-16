using BookReaderAPI.Models.Request;
using BookReaderAPI.Service;
using BookReaderDataAccess.Context;
using BookReaderDataAccess.Models;
using BookReaderDataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookReaderContext>(options => options
                .UseSqlite(builder.Configuration
                .GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BookReaderAPI")));

//Service
builder.Services.AddScoped<IAPIService, APIService>();

//Repository
builder.Services.AddScoped<IGenericRepository<BookDetails>, GenericRepository<BookDetails>>();
builder.Services.AddScoped<IGenericRepository<BookContent>, GenericRepository<BookContent>>();
builder.Services.AddScoped<IGenericRepository<BookPicture>, GenericRepository<BookPicture>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapGet("/all-details", (IAPIService service) =>
{
    return Results.Ok(service.GetAllBookDetails());
});

app.MapGet("/content/{id}", (int id, IAPIService service) =>
{
    return Results.Ok(service.GetBookContentById(id));
});

app.MapPost("/add-book", ([FromBody] BookContentRequest request, IAPIService service) =>
{
    return Results.Ok(service.AddBookIfNotExist(request.Base64Content));
});

app.MapDelete("/delete/{id}", (int id, IAPIService service) =>
{
    service.DeleteById(id);
    return Results.NoContent();
});

app.Run();