namespace Blog.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication InitializeConfiguration(this WebApplication webApplication)
    {
        webApplication.ConfigureAuth();
        webApplication.ConfigureMvc();

        return webApplication;
    }

    private static void ConfigureAuth(this WebApplication webApplication)
    {
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
    }

    private static void ConfigureMvc(this WebApplication webApplication)
        => webApplication.MapControllers();
}