var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }); 

builder.Services.AddDbContext<Blog.Data.BlogDataContext>();

var app = builder.Build();
app.MapControllers();
app.Run();