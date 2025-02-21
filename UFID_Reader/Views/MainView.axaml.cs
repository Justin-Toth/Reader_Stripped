using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UFID_Reader;

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

    private IObservable<string> getTime()
    {
        var currentTime = Observable.Create<string>(
            observer =>
            {
                var timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += (_, _) => observer.OnNext($"{DateTime.Now:hh:mm:ss tt}");
                timer.Start();
                return Disposable.Create(() => timer.Dispose());
            });

        return currentTime;
    }
}