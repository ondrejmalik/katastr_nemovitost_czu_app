﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class ObecCrud : CrudPageBase
{
    private List<ObecData>? _data;
    private ObecData _newItem = new();

    public List<ObecData>? Data
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

    public ObecData NewItem
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

    public ObecCrud()
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
        var data = await LoadDataAsync<ObecData>("/obec");
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
        if (await CreateItemAsync("/obec", NewItem))
        {
            NewItem = new ObecData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ObecData item)
        {
            if (await UpdateItemAsync("/obec", item))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ObecData item)
        {
            if (await DeleteItemAsync("/obec", item.Id))
            {
                LoadData();
            }
        }
    }
}

