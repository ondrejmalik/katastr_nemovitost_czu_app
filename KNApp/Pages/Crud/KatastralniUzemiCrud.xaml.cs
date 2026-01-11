using System.Collections.Generic;
  using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class KatastralniUzemiCrud
{
    private List<KatastralniUzemiData>? _data;
    private KatastralniUzemiData _newItem = new();

    public List<KatastralniUzemiData>? Data
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

    public KatastralniUzemiData NewItem
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

    public KatastralniUzemiCrud()
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
        var data = await LoadDataAsync("/katastralni_uzemi", AppJsonContext.Default.KatastralniUzemiDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/katastralni_uzemi", NewItem, AppJsonContext.Default.KatastralniUzemiData))
        {
            NewItem = new KatastralniUzemiData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KatastralniUzemiData item)
        {
            if (await UpdateItemAsync("/katastralni_uzemi", item, AppJsonContext.Default.KatastralniUzemiData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KatastralniUzemiData item)
        {
            if (await DeleteItemAsync("/katastralni_uzemi", item.Id))
            {
                LoadData();
            }
        }
    }
}

