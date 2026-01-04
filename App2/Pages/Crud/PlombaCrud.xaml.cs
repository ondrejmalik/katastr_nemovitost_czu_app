﻿﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class PlombaCrud : CrudPageBase
{
    private List<PlombaData>? _data;
    private PlombaData _newItem = new();

    public List<PlombaData>? Data
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

    public PlombaData NewItem
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

    public PlombaCrud()
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
        var data = await LoadDataAsync<PlombaData>("/plomba", AppJsonContext.Default.PlombaDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/plomba", NewItem, AppJsonContext.Default.PlombaData))
        {
            NewItem = new PlombaData();
            LoadData();
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is PlombaData item)
        {
            try
            {
                var response = await HttpService.DeleteData($"/plomba?rizeni_id={item.RizeniId}&parcela_id={item.ParcelaId}");
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
