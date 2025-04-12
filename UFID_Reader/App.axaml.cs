using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UFID_Reader.Factory;
using UFID_Reader.Models;
using UFID_Reader.Services;
using UFID_Reader.ViewModels;
using UFID_Reader.Views;

namespace UFID_Reader;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<BaseFrameViewModel>();
        collection.AddTransient<SuccessFrameViewModel>();
        collection.AddTransient<FailureFrameViewModel>();
        
        // Configure Services needed for Authentication
        const string connectionString = "Server=127.0.0.1;Port=3306;Database=ufid_database;User=myuser;Password=mypass;";
        // const string connectionString = "Server=100.64.1.86;Port=3306;Database=ufid_database;User=myuser;Password=mypass;";
        collection.AddSingleton<IDbService, DbService>(x => new DbService(connectionString));
        collection.AddTransient<IAuthService, AuthService>();
        collection.AddTransient<IStudentService, StudentService>();
        collection.AddTransient<IKioskService, KioskService>();
        collection.AddTransient<IScheduleService, ScheduleService>();
        collection.AddTransient<IDateTimeService, DateTimeService>();
        collection.AddTransient<IRpiService, RpiService>();
        
        
        collection.AddSingleton<Func<FrameNames, FrameViewModel>>(x => name => name switch
        {
            FrameNames.Base => x.GetRequiredService<BaseFrameViewModel>(),
            FrameNames.Success => x.GetRequiredService<SuccessFrameViewModel>(),
            FrameNames.Failure => x.GetRequiredService<FailureFrameViewModel>(),
            _ => throw new InvalidOperationException()
        });
        collection.AddSingleton<FrameFactory>();
        
        var services = collection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainView
            {
                DataContext = services.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}