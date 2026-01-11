using System.Collections.Generic;
   using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class OkresCrud
{
    private List<OkresData>? _data;
    private OkresData _newItem = new();

    public List<OkresData>? Data
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

    public OkresData NewItem
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

    public OkresCrud()
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
        var data = await LoadDataAsync("/okres", AppJsonContext.Default.OkresDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/okres", NewItem, AppJsonContext.Default.OkresData))
        {
            NewItem = new OkresData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is OkresData item)
        {
            if (await UpdateItemAsync("/okres", item, AppJsonContext.Default.OkresData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is OkresData item)
        {
            if (await DeleteItemAsync("/okres", item.Id))
            {
                LoadData();
            }
        }
    }
}

