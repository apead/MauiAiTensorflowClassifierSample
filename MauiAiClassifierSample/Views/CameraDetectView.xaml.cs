using CommunityToolkit.Maui.Views;
using MauiAiClassifierSample.ViewModels;

namespace MauiAiClassifierSample
{
    public partial class CameraDetectView : ContentPage
    {
        public CameraDetectView(CameraDetectViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;

            vm.CurrentCameraView = Camera;

            Camera.MediaCaptured += OnMediaCaptured;
          
        }

        async void  OnMediaCaptured(object? sender, MediaCapturedEventArgs e)
        {
            var vm = BindingContext as CameraDetectViewModel;
            await vm.TakePhotoCommand.ExecuteAsync(e.Media);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            var vm = BindingContext as CameraDetectViewModel;

            await vm.RefreshCamerasCommand.ExecuteAsync(cancellationTokenSource.Token);
        }
    }

}
