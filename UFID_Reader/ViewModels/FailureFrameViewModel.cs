using UFID_Reader.Models;

namespace UFID_Reader.ViewModels;

public partial class FailureFrameViewModel : FrameViewModel
{
    public FailureFrameViewModel()
    {
        FrameName = FrameNames.Failure;
    }
}