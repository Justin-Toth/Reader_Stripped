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
    private readonly IRpiService _rpiService;
    private readonly IKioskService _kioskService;
    
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
    public MainViewModel(FrameFactory frameFactory, IAuthService authService, IRpiService rpiService, IKioskService kioskService)
    {
        _frameFactory = frameFactory;
        _authService = authService;
        _rpiService = rpiService;
        _kioskService = kioskService;
        
        // Initialize the kiosk
        InitializeKioskAsync().ConfigureAwait(false);
        Console.WriteLine("Constructor called");
        
        GoToBase();
        
        CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        CurrentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentTime = DateTime.Now.ToString("hh:mm:ss tt"));
    }

    // Design Time Constructor (Can be deleted/removed if needed, allows us to see a design time view of the frame)
    // This is what is throwing the two warnings for this file
    public MainViewModel()
    {
        CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        CurrentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentTime = DateTime.Now.ToString("hh:mm:ss tt"));
    }
    
    private async Task InitializeKioskAsync()
    {
        var serialNumber = _rpiService.GetRpiSerial();
        Console.WriteLine($"Serial number: {serialNumber}");
        var kiosk = await _kioskService.GetKioskBySerialNumberAsync(serialNumber);
        
        if (kiosk == null)
        {
            await _kioskService.RegisterKioskAsync(serialNumber);
            Console.WriteLine("Kiosk registered successfully.");
        }
        else
        {
            Console.WriteLine("Kiosk is already registered.");
        }
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
        // Add timesheet to log swipes
        // Testing serial number if you want "10000000d340eb60"

        var serialNumber = _rpiService.GetRpiSerial();
        Console.WriteLine($"Serial number: {serialNumber}");
        var result = await _authService.AuthenticateSwipeAsync(ScannerInput, Mode, serialNumber);
        ScannerInput = "";
        
        Console.WriteLine(result.Message);
        
        CurrentFrame = result.IsSuccess
            ? _frameFactory.GetFrameViewModel(FrameNames.Success)
            : _frameFactory.GetFrameViewModel(FrameNames.Failure);
        
        await Task.Delay(TimeSpan.FromSeconds(3));
        CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Base);
    }
}