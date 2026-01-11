using System.Collections.Generic;
  using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class RizeniCrud
{
    private List<RizeniData>? _data;
    private RizeniData _newItem = new();

    public List<RizeniData>? Data
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

    public RizeniData NewItem
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

    public RizeniCrud()
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
        var data = await LoadDataAsync("/rizeni", AppJsonContext.Default.RizeniDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/rizeni", NewItem, AppJsonContext.Default.RizeniData))
        {
            NewItem = new RizeniData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is RizeniData item)
        {
            if (await UpdateItemAsync("/rizeni", item, AppJsonContext.Default.RizeniData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is RizeniData item)
        {
            if (await DeleteItemAsync("/rizeni", item.Id))
            {
                LoadData();
            }
        }
    }
}

