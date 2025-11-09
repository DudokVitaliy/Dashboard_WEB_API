using Dashboard_WEB_API.settings;
using Microsoft.Extensions.FileProviders;

public static class ConfigureStaticFiles
{
    public static void AddStaticFiles(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        string rootPath = environment.ContentRootPath;
        string storagePath = Path.Combine(rootPath, StaticFilesSettings.StorageDirectory );
        string imagesPath = Path.Combine(storagePath, StaticFilesSettings.ImagesDirectory);

        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath = "/images",
            FileProvider = new PhysicalFileProvider(imagesPath)
        });
    }
}