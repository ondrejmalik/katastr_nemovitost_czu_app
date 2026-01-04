﻿﻿﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class UcastnikRizeniCrud : CrudPageBase
{
    private List<UcastnikRizeniData>? _data;
    private UcastnikRizeniData _newItem = new();

    public List<UcastnikRizeniData>? Data
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

    public UcastnikRizeniData NewItem
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

    public UcastnikRizeniCrud()
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
        var data = await LoadDataAsync<UcastnikRizeniData>("/ucastnik_rizeni", AppJsonContext.Default.UcastnikRizeniDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/ucastnik_rizeni", NewItem, AppJsonContext.Default.UcastnikRizeniData))
        {
            NewItem = new UcastnikRizeniData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UcastnikRizeniData item)
        {
            if (await UpdateItemAsync("/ucastnik_rizeni", item, AppJsonContext.Default.UcastnikRizeniData))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UcastnikRizeniData item)
        {
            if (await DeleteItemAsync("/ucastnik_rizeni", item.Id))
            {
                LoadData();
            }
        }
    }
}

