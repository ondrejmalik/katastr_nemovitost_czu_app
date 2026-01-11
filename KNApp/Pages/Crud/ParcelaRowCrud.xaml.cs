using System.Collections.Generic;
using KNApp.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace KNApp.Pages.Crud;

public sealed partial class ParcelaRowCrud
{
    private List<ParcelaRowData>? _data;
    private ParcelaRowData _newItem = new();

    public List<ParcelaRowData>? Data
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

    public ParcelaRowData NewItem
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

    public ParcelaRowCrud()
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
        var data = await LoadDataAsync<ParcelaRowData>("/parcela_row", AppJsonContext.Default.ParcelaRowDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/parcela_row", NewItem, AppJsonContext.Default.ParcelaRowData))
        {
            NewItem = new ParcelaRowData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ParcelaRowData item)
        {
            if (await UpdateItemAsync("/parcela_row", item, AppJsonContext.Default.ParcelaRowData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ParcelaRowData item)
        {
            if (await DeleteItemAsync("/parcela_row", item.Id))
            {
                LoadData();
            }
        }
    }
}