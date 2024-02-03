var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
app.MapFallback(HandleFallback);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();


async Task HandleFallback(HttpContext context)
{
    var apiPathSegment = new PathString("/api"); // Find out from the request URL if this is a request to the API or just a web page on the Blazor WASM app.

    bool isApiRequest = context.Request.Path.StartsWithSegments(apiPathSegment);

    if (!isApiRequest)
    {
        context.Response.Redirect("index.html"); // This is a request for a web page so just do the normal out-of-the-box behaviour.
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound; // This request had nothing to do with the Blazor app. This is just an API call that went wrong.
    }
}