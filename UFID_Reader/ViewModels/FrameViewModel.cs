using CommunityToolkit.Mvvm.ComponentModel;
using UFID_Reader.Models;

namespace UFID_Reader.ViewModels;

public partial class FrameViewModel : ViewModelBase
{
    [ObservableProperty]
    private FrameNames _frameName;
}