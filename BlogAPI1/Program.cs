var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// Our Datastore


var blogs = new List<Blog>()

{ 
    new  Blog() {Id = 1, Title = "Intro to Mimimal API" , Content ="We are going Implement minimal API" },
    new  Blog() {Id = 2, Title = "Intro to  API" , Content ="We are going Implement  API white Controllers" },
   
};

//Consulta todos blogs

app.MapGet("api/blog", () => Results.Ok(blogs));


//Consulta o blog por id
app.MapGet("api/blog/{id}", (int id) =>
{
    Blog BlogPost = blogs.FirstOrDefault(b => b.Id == id);

    if (BlogPost == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(BlogPost);
    
});


//Cria um novo blog

app.MapPost("api/blog", (Blog blog) =>
{
    blogs.Add(blog);

    return Results.Ok();
               


});

//atualiza o blog 

app.MapPut("api/blog", (Blog blog) =>
{
    var BlogPost = blogs.FirstOrDefault(p => p.Id == blog.Id);
    if (BlogPost == null)
   
    {
        return Results.NotFound();
    }

        BlogPost.Title = blog.Title;
        BlogPost.Content = blog.Content;

        return Results.Ok();
    

});


//Deleta o blog

app.MapDelete("api/blog", (int id) =>
{
    var blogPost = blogs.FirstOrDefault(p => p.Id == id);
    if (blogPost == null)
    {
        return Results.NotFound();
    }

    blogs.Remove(blogPost);

    return Results.Ok();


});



app.Run();
class Blog 
{
    
    public int Id { get; set; }
    public string Title { get; set; }

    public string Content { get; set; }

}
    