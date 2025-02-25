using System;
using UFID_Reader.Models;
using UFID_Reader.ViewModels;

namespace UFID_Reader.Factory;

public class FrameFactory(Func<FrameNames, FrameViewModel> factory)
{
    public FrameViewModel GetFrameViewModel(FrameNames frameName) => factory.Invoke(frameName);
}