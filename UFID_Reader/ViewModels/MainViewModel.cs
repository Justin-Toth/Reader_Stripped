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
    private readonly IValidationService _validationService;
    
    [ObservableProperty]
    private FrameViewModel _currentFrame = null!;
    
    [ObservableProperty] 
    private string _currentTime;
    
    [ObservableProperty]
    private string _currentDate;
    
    [ObservableProperty]
    private string _scannerInput = "";

    // Default Constructor
    public MainViewModel(FrameFactory frameFactory, IValidationService validationService)
    {
        _frameFactory = frameFactory;
        _validationService = validationService;
        
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
    private async Task Validate()
    {
        const int mode = 1;
        const string serialNum = "10000000d340eb60";
        
        // Run the validation service
        // {Mode = 1 (exam mode), SerialNum = 10000000d340eb60 (valid kiosk in db), ScannerInput = the input from the scanner}
        var result = await _validationService.Validate(mode, serialNum, ScannerInput);

        // Clear the scanner input for a new scan
        ScannerInput = "";
        
        // Debug: print the result to console
        Console.WriteLine(result.IsValid ? "Success" : "Failure");

        // Set the current frame to the appropriate frame based on the result
        CurrentFrame = result.IsValid
            ? _frameFactory.GetFrameViewModel(FrameNames.Success) 
            : _frameFactory.GetFrameViewModel(FrameNames.Failure);
        
        // Delay for 1 second before returning to the base frame to allow result to display
        await Task.Delay(TimeSpan.FromSeconds(1));
        CurrentFrame = _frameFactory.GetFrameViewModel(FrameNames.Base);
    }
}