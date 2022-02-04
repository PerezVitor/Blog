using Blog.Extensions;

WebApplication
    .CreateBuilder(args)
    .InitializeConfiguration()
    .BuildApp()
    .Run();