using System;
using System.Collections.Generic;
using System.Text.Json;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App2.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ParcelaDisplay
{
    public List<ParcelaData>? Data { get; set; }

    public ParcelaDisplay()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is List<ParcelaData> data)
        {
            Data = data;
        }
    }

    private void GoLv(object sender, RoutedEventArgs e)
    {
        var katastralniUzemi = Data[0].KatastralniUzemi;
        var lv = Data[0].CisloLv;

        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            using var client = new System.Net.Http.HttpClient();
            var url = $"http://localhost:3000/lv?katastralni_uzemi={katastralniUzemi}&cislo_lv={lv}";
            try
            {
                var response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize(response, AppJsonContext.Default.LVData);


                DispatcherQueue.TryEnqueue(() =>
                {
                    Frame.Navigate(typeof(ListVlastnictviDisplay), data,
                        new SuppressNavigationTransitionInfo());
                });
            }
            catch (Exception)
            {
            }
        });
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ParcelaSearch), null, new SuppressNavigationTransitionInfo());
    }
}