using System;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UFID_Reader.Factory;
using UFID_Reader.Models;

namespace UFID_Reader.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly FrameFactory _frameFactory;
    
    [ObservableProperty]
    private FrameViewModel _currentFrame = null!;
    
    [ObservableProperty] 
    private string _currentTime;
    
    [ObservableProperty]
    private string _currentDate;

    public MainViewModel(FrameFactory frameFactory)
    {
        _frameFactory = frameFactory;
        
        GoToBase();
        
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
    
    
    // Temp Function to swap between frames
    [RelayCommand]
    private void ToggleFrame()
    {
       switch (CurrentFrame.FrameName)
        {
            case FrameNames.Base:
                GoToFailure();
                break;
            case FrameNames.Failure:
                GoToSuccess();
                break;
            default:
                GoToBase();
                break;
        }    
    }
}