using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using KNApp.Types;

namespace KNApp.Pages.Crud;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MajitelCrud
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
        var data = await LoadDataAsync("/majitel", AppJsonContext.Default.MajitelDataList);
        if (data != null)
        {
            Data = data;
        }
    }


    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (await CreateItemAsync("/majitel", NewMajitel, AppJsonContext.Default.MajitelData))
        {
            NewMajitel = new MajitelData();
            LoadData();
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is MajitelData majitel)
        {
            if (await UpdateItemAsync("/majitel", majitel, AppJsonContext.Default.MajitelData))
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