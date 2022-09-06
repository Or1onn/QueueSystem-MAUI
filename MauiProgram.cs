using CommunityToolkit.Maui;

namespace QueueSystem;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("texgyrebonum-regular", "TeX");
            fonts.AddFont("texgyrebonum-bold.otf", "TeX_Bold");
        }).UseMauiCommunityToolkit();
        return builder.Build();
    }
}