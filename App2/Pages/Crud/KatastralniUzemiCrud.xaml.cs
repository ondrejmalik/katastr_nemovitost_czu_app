﻿using System.Collections.Generic;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public sealed partial class KatastralniUzemiCrud : CrudPageBase
{
    private List<KatastralniUzemiData>? _data;
    private KatastralniUzemiData _newItem = new();

    public List<KatastralniUzemiData>? Data
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

    public KatastralniUzemiData NewItem
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

    public KatastralniUzemiCrud()
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
        var data = await LoadDataAsync<KatastralniUzemiData>("/katastralni_uzemi");
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
        if (await CreateItemAsync("/katastralni_uzemi", NewItem))
        {
            NewItem = new KatastralniUzemiData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KatastralniUzemiData item)
        {
            if (await UpdateItemAsync("/katastralni_uzemi", item))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is KatastralniUzemiData item)
        {
            if (await DeleteItemAsync("/katastralni_uzemi", item.Id))
            {
                LoadData();
            }
        }
    }
}

