﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class BpejCrud : CrudPageBase
{
    private List<BpejData>? _data;
    private BpejData _newItem = new();

    public List<BpejData>? Data
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

    public BpejData NewItem
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

    public BpejCrud()
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
        var data = await LoadDataAsync<BpejData>("/bpej", AppJsonContext.Default.BpejDataList);
        if (data != null)
        {
            Data = data;
        }
    }

    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/bpej", NewItem, AppJsonContext.Default.BpejData))
        {
            NewItem = new BpejData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is BpejData item)
        {
            if (await UpdateItemAsync("/bpej", item, AppJsonContext.Default.BpejData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is BpejData item)
        {
            if (await DeleteItemAsync("/bpej", item.Id))
            {
                LoadData();
            }
        }
    }
}

