using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UFID_Reader.Factory;
using UFID_Reader.Models;
using UFID_Reader.Services;

namespace UFID_Reader.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly FrameFactory _frameFactory;
    private readonly IAuthService _authService;
    
    [ObservableProperty]
    private FrameViewModel _currentFrame = null!;
    
    [ObservableProperty] 
    private string _currentTime;
    
    [ObservableProperty]
    private string _currentDate;
    
    [ObservableProperty]
    private string _scannerInput = "";

    [ObservableProperty] 
    private string _mode = "class";

    // Default Constructor
    public MainViewModel(FrameFactory frameFactory, IAuthService authService)
    {
        _frameFactory = frameFactory;
        _authService = authService;
        
        GoToBase();
        
        CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        CurrentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentTime = DateTime.Now.ToString("hh:mm:ss tt"));
    }

    // Design Time Constructor
    public MainViewModel()
    {
        CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        CurrentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentTime = DateTime.Now.ToString("hh:mm:ss tt"));
    }
    
    
    [RelayCommand]
    private void GoToBase() => CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Base);

    [RelayCommand]
    private void GoToFailure() => CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Failure);
    
    [RelayCommand]
    private void GoToSuccess() => CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Success);
    
    [RelayCommand]
    private void ChangeMode() => Mode = Mode == "class" ? "exam" : "class";
    
    [RelayCommand]
    private async Task Authenticate()
    {
        // Todo:
        // Add 15 min grace period to start_times (swiping before class/exam starts)
        // Grab serial number from kiosk (raspberry pi)
        // Figure something out for mode selection physically
        
        var result = await _authService.AuthenticateSwipeAsync(ScannerInput, Mode, "10000000d340eb60");
        ScannerInput = "";
        
        Console.WriteLine(result.Message);
        
        CurrentFrame = result.IsSuccess
            ? _frameFactory.GetFrameViewModel(FrameNames.Success)
            : _frameFactory.GetFrameViewModel(FrameNames.Failure);
        
        await Task.Delay(TimeSpan.FromSeconds(3));
        CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Base);
    }
}