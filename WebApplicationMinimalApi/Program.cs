using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fruit API",
        Description = "API for managing a list of fruit and their stock status.",
        TermsOfService = new Uri("http://example.com/terms")
    });
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users/{userID}/books/{bookId}",
    (int userId, int bookId) => $"The user id is {userId} and book id is {bookId}");

app.MapPost("/fruits", async (Fruit fruit, FruitDb db) =>
{
    db.Fruits.Add(fruit);
    await db.SaveChangesAsync();

    return Results.Created($"/{fruit.Id}", fruit);
})
    .Produces<Fruit>(201)
    .WithTags("post", "fruits")
    .WithSummary("Create a new fruit");

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.Run();
