using Microsoft.EntityFrameworkCore;
using Test.Data;
using TestWebApi.Repositories;
using TestWebApi.Repositories.Interfaces;
using TestWebApi.Services;
using TestWebApi.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7123",
                                              "http://localhost:3000");
                      });
});
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnString")));

builder.Services.AddControllers();

builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
