using UFID_Reader.Models;

namespace UFID_Reader.ViewModels;

public partial class BaseFrameViewModel : FrameViewModel
{

    
    public BaseFrameViewModel()
    {
        FrameName = FrameNames.Base;
    }
    
    /*
    [RelayCommand]
    private async Task Authenticate()
    {
        var authResult = await _authService.Authenticate(Input, Input);
    }
    */
}