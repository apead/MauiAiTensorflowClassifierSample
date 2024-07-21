using Android.App;
using Android.Runtime;
using MauiAiClassifierSample.Droid;
using MauiAiClassifierSample.Services;

namespace MauiAiClassifierSample
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            return MauiProgram.CreateMauiApp(RegisterServices);
        }

        private static void RegisterServices(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IClassifier, TensorflowClassifier>();
        }
    }
}
