using App2;
using App2.Pages.Crud;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace App2.Pages;

public sealed partial class CrudMenuPage
{
    public CrudMenuPage()
    {
        InitializeComponent();
        MenuContent.IsEnabled = false;
        Loaded += CrudMenuPage_Loaded;
    }

    private async void CrudMenuPage_Loaded(object sender, RoutedEventArgs e)
    {
        Loaded -= CrudMenuPage_Loaded;
        string password = AppSettings.AuthPassword;
        bool authorized = false;
        if (!string.IsNullOrEmpty(password))
        {
            try
            {
                var response = await HttpService.GetData($"/auth?password={password}");
                if (response.IsSuccessStatusCode)
                {
                    authorized = true;
                }
            }
            catch
            {
                // Request failed
            }
        }

        if (!authorized)
        {
            while (!authorized)
            {
                var passwordBox = new PasswordBox { PlaceholderText = "Password" };
                var dialog = new ContentDialog
                {
                    Title = "Enter Password",
                    Content = passwordBox,
                    PrimaryButtonText = "Submit",
                    CloseButtonText = "Cancel",
                    XamlRoot = XamlRoot,
                    DefaultButton = ContentDialogButton.Primary
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    try
                    {
                        var response = await HttpService.GetData($"/auth?password={passwordBox.Password}");
                        if (response.IsSuccessStatusCode)
                        {
                            authorized = true;
                            AppSettings.AuthPassword = passwordBox.Password;
                        }
                    }
                    catch
                    {
                        // Request failed
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if (authorized)
        {
            MenuContent.IsEnabled = true;
            if (MenuContent.MenuItems.Count > 0)
            {
                MenuContent.SelectedItem = MenuContent.MenuItems[0];
            }
        }
        else
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }
    }

    private void MenuContent_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            // Handle settings if needed
        }
        else
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem?.Tag == null) return;
            string? tag = selectedItem.Tag.ToString();
            Type? pageType = null;

            switch (tag)
            {
                case "Majitel": pageType = typeof(MajitelCrud); break;
                case "Kraj": pageType = typeof(KrajCrud); break;
                case "Okres": pageType = typeof(OkresCrud); break;
                case "Obec": pageType = typeof(ObecCrud); break;
                case "KatastralniUzemi": pageType = typeof(KatastralniUzemiCrud); break;
                case "Bpej": pageType = typeof(BpejCrud); break;
                case "TypRizeni": pageType = typeof(TypRizeniCrud); break;
                case "TypOperace": pageType = typeof(TypOperaceCrud); break;
                case "TypUcastnika": pageType = typeof(TypUcastnikaCrud); break;
                case "UcastnikRizeni": pageType = typeof(UcastnikRizeniCrud); break;
                case "ListVlastnictvi": pageType = typeof(ListVlastnictviCrud); break;
                case "ParcelaRow": pageType = typeof(ParcelaRowCrud); break;
                case "Rizeni": pageType = typeof(RizeniCrud); break;
                case "Vlastnictvi": pageType = typeof(VlastnictviCrud); break;
                case "BremenoParcelaParcela": pageType = typeof(BremenoParcelaParcelaCrud); break;
                case "BremenoParcelaMajitel": pageType = typeof(BremenoParcelaMajitelCrud); break;
                case "RizeniOperaceRow": pageType = typeof(RizeniOperaceRowCrud); break;
                case "Plomba": pageType = typeof(PlombaCrud); break;
                case "Ucast": pageType = typeof(UcastCrud); break;
                case "Exit":
                    Frame.Navigate(typeof(MainPage));
                    return;
            }

            if (pageType != null)
            {
                ContentFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo());
            }
        }
    }

    private void MenuContent_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        Frame.Navigate(typeof(MainPage));
    }
}