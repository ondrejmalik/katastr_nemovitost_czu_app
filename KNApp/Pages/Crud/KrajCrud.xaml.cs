using System.Collections.Generic;
   using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class KrajCrud 
{
    private List<KrajData>? _data;
    private KrajData _newItem = new() { Nazev = string.Empty };

    public List<KrajData>? Data
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

    public KrajData NewItem
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

    public KrajCrud()
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
        var data = await LoadDataAsync("/kraj", AppJsonContext.Default.KrajDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/kraj", NewItem, AppJsonContext.Default.KrajData))
        {
            NewItem = new KrajData() { Nazev = string.Empty };
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KrajData item)
        {
            if (await UpdateItemAsync("/kraj", item, AppJsonContext.Default.KrajData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KrajData item)
        {
            if (await DeleteItemAsync("/kraj", item.Id))
            {
                LoadData();
            }
        }
    }
}

