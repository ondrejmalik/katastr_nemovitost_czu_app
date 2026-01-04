﻿﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class BremenoParcelaParcelaCrud : CrudPageBase
{
    private List<BremenoParcelaParcelaData>? _data;
    private BremenoParcelaParcelaData _newItem = new();

    public List<BremenoParcelaParcelaData>? Data
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

    public BremenoParcelaParcelaData NewItem
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

    public BremenoParcelaParcelaCrud()
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
        var data = await LoadDataAsync<BremenoParcelaParcelaData>("/bremeno_parcela_parcela", AppJsonContext.Default.BremenoParcelaParcelaDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/bremeno_parcela_parcela", NewItem, AppJsonContext.Default.BremenoParcelaParcelaData))
        {
            NewItem = new BremenoParcelaParcelaData();
            LoadData();
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is BremenoParcelaParcelaData item)
        {
            try
            {
                var response = await HttpService.DeleteData($"/bremeno_parcela_parcela?parcela_id={item.ParcelaId}&parcela_povinna_id={item.ParcelaPovinnaId}");
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
