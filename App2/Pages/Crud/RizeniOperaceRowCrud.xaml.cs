﻿﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class RizeniOperaceRowCrud : CrudPageBase
{
    private List<RizeniOperaceRowData>? _data;
    private RizeniOperaceRowData _newItem = new();

    public List<RizeniOperaceRowData>? Data
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

    public RizeniOperaceRowData NewItem
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

    public RizeniOperaceRowCrud()
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
        var data = await LoadDataAsync<RizeniOperaceRowData>("/rizeni_operace", AppJsonContext.Default.RizeniOperaceRowDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/rizeni_operace", NewItem, AppJsonContext.Default.RizeniOperaceRowData))
        {
            NewItem = new RizeniOperaceRowData();
            LoadData();
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is RizeniOperaceRowData item)
        {
            try
            {
                var response = await HttpService.DeleteData($"/rizeni_operace?rizeni_id={item.RizeniId}&typ_operace_id={item.TypOperaceId}");
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
