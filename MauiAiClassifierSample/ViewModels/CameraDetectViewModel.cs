using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAiClassifierSample.Enums;
using MauiAiClassifierSample.Services;
using CameraInfo = CommunityToolkit.Maui.Core.CameraInfo;
using ICameraProvider = CommunityToolkit.Maui.Core.ICameraProvider;

namespace MauiAiClassifierSample.ViewModels;

public partial class CameraDetectViewModel : BaseViewModel
{
	private IClassifier _classifierService;
    private ICameraProvider _cameraProvider;
    
    [ObservableProperty]
    CameraActionEnum currentCameraAction;

[ObservableProperty]
    CameraView currentCameraView;

    [ObservableProperty]
	CameraInfo? selectedCamera;

	[ObservableProperty]
	Size selectedResolution;

    [ObservableProperty]
    ImageSource capturedImage;

    [ObservableProperty]
    string tagName;


	public IReadOnlyList<CameraInfo> Cameras => _cameraProvider?.AvailableCameras ?? [];

	public CancellationToken Token => CancellationToken.None;

	public ICollection<CameraFlashMode> FlashModes { get; } = Enum.GetValues<CameraFlashMode>();

	public CameraDetectViewModel(ICameraProvider cameraProvider, IClassifier classifierService)
	{
		_cameraProvider = cameraProvider;
		_classifierService = classifierService;

        SelectedResolution = new Size(1024, 768);

		TagName = "Detected Tag";

    }


	[RelayCommand]
	async Task RefreshCameras(CancellationToken token) => await _cameraProvider.RefreshAvailableCameras(token);


    [RelayCommand]
    async Task TakePhoto(Stream imageStream)
    {
		if (CurrentCameraAction == CameraActionEnum.Object)
		{
			string? encodedImage = string.Empty;
			using (MemoryStream ms = new MemoryStream())
			{
				imageStream.CopyTo(ms);

				  var classifications = await _classifierService.Classify(ms.ToArray());

				if (classifications != null)
				{
                    var sortedList = classifications.OrderByDescending(x => x.Probability);

                    var top = sortedList.First();

					if (top.Probability >= 0.9)
						TagName = top.TagName;
					else TagName = "Not sure";

                }
            }
		}
    }


    [RelayCommand]
    async Task DoClassify()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            currentCameraAction = CameraActionEnum.Object;
            CurrentCameraView.CaptureImageCommand.Execute(Token);
        });
    }
}