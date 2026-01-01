﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class TypRizeniCrud : CrudPageBase
{
    private List<TypRizeniData>? _data;
    private TypRizeniData _newItem = new();

    public List<TypRizeniData>? Data
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

    public TypRizeniData NewItem
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

    public TypRizeniCrud()
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
        var data = await LoadDataAsync<TypRizeniData>("/typ_rizeni");
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
        if (await CreateItemAsync("/typ_rizeni", NewItem))
        {
            NewItem = new TypRizeniData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is TypRizeniData item)
        {
            if (await UpdateItemAsync("/typ_rizeni", item))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is TypRizeniData item)
        {
            if (await DeleteItemAsync("/typ_rizeni", item.Id))
            {
                LoadData();
            }
        }
    }
}

