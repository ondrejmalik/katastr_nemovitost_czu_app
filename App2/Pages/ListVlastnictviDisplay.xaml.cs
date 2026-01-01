using System;
using System.Text.Json;
using ABI.Microsoft.UI.Xaml.Media.Animation;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


namespace App2.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ListVlastnictviDisplay : Page
{
    public LVData? Data { get; set; }

    public ListVlastnictviDisplay()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is LVData data)
        {
            Data = data;
        }
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ListVlastnictviSearch), new SuppressNavigationTransitionInfo());
    }

    private void GoSpravniRizeni(object sender, RoutedEventArgs e)
    {
        if (sender is HyperlinkButton { DataContext: PartD partD })
        {
            _ = System.Threading.Tasks.Task.Run((Func<System.Threading.Tasks.Task>)(async () =>
            {
                var uri = $"/spravni_rizeni?id={partD.CisloRizeni}";
                try
                {
                    var response = await HttpService.GetData(uri);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<SpravniRizeniData>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    DispatcherQueue.TryEnqueue(() => { Frame.Navigate(typeof(SpravniRizeniDisplay), data); });
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }));
        }
    }
}