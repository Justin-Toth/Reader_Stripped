using System;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UFID_Reader.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentFrame;

    [ObservableProperty] 
    private string _currentTime;
    
    [ObservableProperty]
    private string _currentDate;

    private readonly ScanFrameBaseViewModel _scanFrameBase = new ();
    private readonly ScanFrameFailureViewModel _scanFrameFailure = new ();
    private readonly ScanFrameSuccessViewModel _scanFrameSuccess = new ();

    public MainViewModel()
    {
        CurrentFrame = _scanFrameBase;

        CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        CurrentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentTime = DateTime.Now.ToString("hh:mm:ss tt"));
    }

    [RelayCommand]
    private void ToggleFrame()
    {
        if (CurrentFrame == _scanFrameBase)
        {
            GoToFailure();
        }
        else if (CurrentFrame == _scanFrameFailure)
        {
            GoToSuccess();
        }
        else
        {
            GoToBase();
        }
    }
    
    [RelayCommand]
    private void GoToBase() => CurrentFrame = _scanFrameBase;    
    
    [RelayCommand] 
    private void GoToFailure() => CurrentFrame = _scanFrameFailure;    
    
    [RelayCommand]
    private void GoToSuccess() => CurrentFrame = _scanFrameSuccess;    
}