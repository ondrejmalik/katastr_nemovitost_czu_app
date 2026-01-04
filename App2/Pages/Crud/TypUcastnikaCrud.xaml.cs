﻿﻿﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class TypUcastnikaCrud : CrudPageBase
{
    private List<TypUcastnikaData>? _data;
    private TypUcastnikaData _newItem = new();

    public List<TypUcastnikaData>? Data
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

    public TypUcastnikaData NewItem
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

    public TypUcastnikaCrud()
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
        var data = await LoadDataAsync<TypUcastnikaData>("/typ_ucastnika", AppJsonContext.Default.TypUcastnikaDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/typ_ucastnika", NewItem, AppJsonContext.Default.TypUcastnikaData))
        {
            NewItem = new TypUcastnikaData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is TypUcastnikaData item)
        {
            if (await UpdateItemAsync("/typ_ucastnika", item, AppJsonContext.Default.TypUcastnikaData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is TypUcastnikaData item)
        {
            if (await DeleteItemAsync("/typ_ucastnika", item.Id))
            {
                LoadData();
            }
        }
    }
}

