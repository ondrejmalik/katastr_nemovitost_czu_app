using System;
using System.Threading.Tasks;
using App2.Pages;
using App2.Pages.Crud;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
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

    private async void CheckHealth()
    {
        AddressTextBox.Text = AppSettings.Address;
        PortTextBox.Text = AppSettings.Port.ToString();
        try
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var response = await HttpService.GetData("/health");
            sw.Stop();

            if (response.IsSuccessStatusCode)
            {
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
        }
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        CheckHealth();
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
        Frame.Navigate(typeof(CrudMenuPage), null, new SuppressNavigationTransitionInfo());
    }

    private void AddressTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        AppSettings.Address = ((TextBox)sender).Text;
        CheckHealth();
    }

    private void PortTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        AppSettings.Port = Convert.ToInt32(((TextBox)sender).Text);
        CheckHealth();
    }
}