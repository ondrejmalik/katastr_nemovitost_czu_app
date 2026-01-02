using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;


using App2.Pages.Crud;

namespace App2.Pages.Crud;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MajitelCrud : CrudPageBase
{
    private List<MajitelData>? _data;
    private MajitelData _newMajitel = new();

    public List<MajitelData>? Data
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

    public MajitelData NewMajitel
    {
        get => _newMajitel;
        set
        {
            if (_newMajitel != value)
            {
                _newMajitel = value;
                OnPropertyChanged();
            }
        }
    }

    public MajitelCrud()
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
        var data = await LoadDataAsync<MajitelData>("/majitel");
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
        if (await CreateItemAsync("/majitel", NewMajitel))
        {
            NewMajitel = new MajitelData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is MajitelData majitel)
        {
            if (await UpdateItemAsync("/majitel", majitel))
            {
                LoadData();
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is MajitelData majitel)
        {
            if (await DeleteItemAsync("/majitel", majitel.Id))
            {
                LoadData();
            }
        }
    }
}