using CommunityToolkit.Maui;
using MauiAiClassifierSample.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiAiClassifierSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp(Action<MauiAppBuilder> registationForPlatformSpecificServices)
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitCamera()
                .UseMauiCommunityToolkitMediaElement()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            registationForPlatformSpecificServices.Invoke(builder);
            
            builder.RegisterServices()
            .RegisterViewModels()
            .RegisterViews();

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<CameraDetectViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<CameraDetectView>();

            return mauiAppBuilder;
        }
    }
}
