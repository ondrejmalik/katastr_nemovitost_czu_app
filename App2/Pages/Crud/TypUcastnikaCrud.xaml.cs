﻿using System.Collections.Generic;
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
        Loaded += (s, e) => MessageInfoBar = this.FindName("MessageInfoBar") as InfoBar;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        LoadData();
    }

    private async void LoadData()
    {
        var data = await LoadDataAsync<TypUcastnikaData>("/typ_ucastnika");
        if (data != null)
        {
            Data = data;
        }
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(CrudMenuPage), null, new SuppressNavigationTransitionInfo());
    }

    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/typ_ucastnika", NewItem))
        {
            NewItem = new TypUcastnikaData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is TypUcastnikaData item)
        {
            if (await UpdateItemAsync("/typ_ucastnika", item))
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

