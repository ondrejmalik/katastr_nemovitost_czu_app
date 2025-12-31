using System;
using System.Threading.Tasks;
using App2.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App2;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        Loaded += MainPage_Loaded;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var response = await HttpService.GetData("/health");
            if (response.IsSuccessStatusCode)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                await HttpService.GetData("/health");
                sw.Stop();

                HealthInfoBar.Severity = InfoBarSeverity.Success;
                HealthInfoBar.Title = "Health Check";
                HealthInfoBar.Message = $"Service is healthy. Latency: {sw.ElapsedMilliseconds}ms";
                HealthInfoBar.IsOpen = true;

                await Task.Delay(5000);
                HealthInfoBar.IsOpen = false;
            }
        }
        catch (Exception)
        {
            // Silently fail or handle error if needed
        }
    }

    private void GoLv(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ListVlastnictviSearch), null, new SuppressNavigationTransitionInfo());
    }

    private void GoParcela(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ParcelaSearch), null, new SuppressNavigationTransitionInfo());
    }

    private void GoSpravniRizeni(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SpravniRizeniSearch), null, new SuppressNavigationTransitionInfo());
    }

    private void GoCrud(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Crud), null, new SuppressNavigationTransitionInfo());
    }
}