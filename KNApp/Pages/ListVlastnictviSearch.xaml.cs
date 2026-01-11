using System;
using System.Text.Json;
using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace KNApp.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ListVlastnictviSearch
{
    public ListVlastnictviSearch()
    {
        InitializeComponent();
    }


    private void SearchLv(object sender, RoutedEventArgs e)
    {
        var katastralniUzemi = Uri.EscapeDataString(KatastralniUzemiTextBox.Text);
        var lv = Uri.EscapeDataString(CisloLvTextBox.Text);
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            var uri = $"/lv?katastralni_uzemi={katastralniUzemi}&cislo_lv={lv}";
            try
            {
                var response = await HttpService.GetData(uri);
                response.EnsureSuccessStatusCode();


                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize(json, AppJsonContext.Default.LvData);


                DispatcherQueue.TryEnqueue(() =>
                {
                    Frame.Navigate(typeof(ListVlastnictviDisplay), data, new SuppressNavigationTransitionInfo());
                });
            }
            catch (Exception)
            {
                // log or ignore
            }
        });
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage), new SuppressNavigationTransitionInfo());
    }
}