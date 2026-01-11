using System.Collections.Generic;
using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class UcastCrud
{
    private List<UcastData>? _data;
    private UcastData _newItem = new();

    public List<UcastData>? Data
    {
        get => _data;
        set
        {
            if (_data != value)
            {
                _data = value;
                OnPropertyChanged();
            }
        }
    }

    public UcastData NewItem
    {
        get => _newItem;
        set
        {
            if (_newItem != value)
            {
                _newItem = value;
                OnPropertyChanged();
            }
        }
    }

    public UcastCrud()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        LoadData();
    }

    private async void LoadData()
    {
        var data = await LoadDataAsync("/ucast", AppJsonContext.Default.UcastDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/ucast", NewItem, AppJsonContext.Default.UcastData))
        {
            NewItem = new UcastData();
            LoadData();
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UcastData item)
        {
            try
            {
                var response = await HttpService.DeleteData($"/ucast?rizeni_id={item.RizeniId}&ucastnik_rizeni_id={item.UcastnikRizeniId}&typ_ucastnika_id={item.TypUcastnikaId}");
                if (response.IsSuccessStatusCode)
                {
                    LoadData();
                    ShowMessage("Success", "Item deleted successfully.", InfoBarSeverity.Success);
                }
                else
                {
                    ShowMessage("Error", "Failed to delete item.", InfoBarSeverity.Error);
                }
            }
            catch (System.Exception ex)
            {
                ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            }
        }
    }
}
