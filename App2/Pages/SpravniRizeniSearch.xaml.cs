using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App2.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SpravniRizeniSearch
{
    public SpravniRizeniSearch()
    {
        InitializeComponent();
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
    }

    private void SearchRizeni(object sender, RoutedEventArgs e)
    {
        var typ = (TypComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
        var cislo = CisloTextBox.Text;
        var rok = RokTextBox.Text;
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            var uri = $"/spravni_rizeni?typ={typ}&cislo={cislo}&rok={rok}";
            try
            {
                var response = await HttpService.GetData(uri);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<SpravniRizeniData>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                DispatcherQueue.TryEnqueue(() =>
                {
                    Frame.Navigate(typeof(SpravniRizeniDisplay), data, new SuppressNavigationTransitionInfo());
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception();
            }
        });
    }
}