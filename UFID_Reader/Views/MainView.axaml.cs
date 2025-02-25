using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UFID_Reader.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }
    
    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}