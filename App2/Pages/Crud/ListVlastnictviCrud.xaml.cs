﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class ListVlastnictviCrud : CrudPageBase
{
    private List<ListVlastnictviData>? _data;
    private ListVlastnictviData _newItem = new();

    public List<ListVlastnictviData>? Data
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

    public ListVlastnictviData NewItem
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

    public ListVlastnictviCrud()
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
        var data = await LoadDataAsync<ListVlastnictviData>("/list_vlastnictvi", AppJsonContext.Default.ListVlastnictviDataList);
        if (data != null)
        {
            Data = data;
        }
    }

    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/list_vlastnictvi", NewItem, AppJsonContext.Default.ListVlastnictviData))
        {
            NewItem = new ListVlastnictviData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ListVlastnictviData item)
        {
            if (await UpdateItemAsync("/list_vlastnictvi", item, AppJsonContext.Default.ListVlastnictviData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ListVlastnictviData item)
        {
            if (await DeleteItemAsync("/list_vlastnictvi", item.Id))
            {
                LoadData();
            }
        }
    }
}

