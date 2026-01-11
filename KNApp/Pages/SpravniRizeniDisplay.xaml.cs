using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using NavigationEventArgs = Microsoft.UI.Xaml.Navigation.NavigationEventArgs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace KNApp.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SpravniRizeniDisplay
{
    public SpravniRizeniData? Data { get; set; }

    public SpravniRizeniDisplay()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is SpravniRizeniData data)
        {
            Data = data;
        }

        base.OnNavigatedTo(e);
    }


    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
    }
}