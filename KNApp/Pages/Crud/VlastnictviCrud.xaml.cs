using System.Collections.Generic;
using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class VlastnictviCrud
{
    private List<VlastnictviData>? _data;
    private VlastnictviData _newItem = new();

    public List<VlastnictviData>? Data
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

    public VlastnictviData NewItem
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

    public VlastnictviCrud()
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
        var data = await LoadDataAsync("/vlastnictvi", AppJsonContext.Default.VlastnictviDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/vlastnictvi", NewItem, AppJsonContext.Default.VlastnictviData))
        {
            NewItem = new VlastnictviData();
            LoadData();
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is VlastnictviData item)
        {
            try
            {
                var response = await HttpService.DeleteData($"/vlastnictvi?parcela_id={item.ParcelaId}&majitel_id={item.MajitelId}");
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
