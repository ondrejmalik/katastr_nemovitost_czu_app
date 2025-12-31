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


namespace App2.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MajitelCrud : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MajitelCrud()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            var uri = $"/majitel";
            try
            {
                var response = await HttpService.GetData(uri);
                response.EnsureSuccessStatusCode();


                var json = await response.Content.ReadAsStringAsync();
                var fetchedData = JsonSerializer.Deserialize<List<MajitelData>>(json);


                DispatcherQueue.TryEnqueue(() => { Data = fetchedData; });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });


        base.OnNavigatedTo(e);
        if (e.Parameter is List<MajitelData> data)
        {
            Data = data;
        }
    }


    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ParcelaSearch), null, new SuppressNavigationTransitionInfo());
    }

    private async void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var json = JsonSerializer.Serialize(NewMajitel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpService.PostData("/majitel", content);

            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Success", "Majitel created successfully.", InfoBarSeverity.Success);
                NewMajitel = new MajitelData(); // Reset form
                // Refresh list
                var refreshResponse = await HttpService.GetData("/majitel");
                if (refreshResponse.IsSuccessStatusCode)
                {
                    var refreshJson = await refreshResponse.Content.ReadAsStringAsync();
                    var fetchedData = JsonSerializer.Deserialize<List<MajitelData>>(refreshJson);
                    DispatcherQueue.TryEnqueue(() => { Data = fetchedData; });
                }
            }
            else
            {
                ShowMessage("Error", "Failed to create majitel.", InfoBarSeverity.Error);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
        }
    }

    private async void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is MajitelData majitel)
        {
            try
            {
                var json = JsonSerializer.Serialize(majitel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await HttpService.PutData($"/majitel", content);

                if (response.IsSuccessStatusCode)
                {
                    ShowMessage("Success", "Majitel updated successfully.", InfoBarSeverity.Success);
                }
                else
                {
                    ShowMessage("Error", "Failed to update majitel.", InfoBarSeverity.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            }
        }
    }

    private async void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is MajitelData majitel)
        {
            try
            {
                var response = await HttpService.DeleteData($"/majitel?id={majitel.Id}");
                if (response.IsSuccessStatusCode)
                {
                    DispatcherQueue.TryEnqueue(() =>
                    {
                        if (Data != null)
                        {
                            var list = new List<MajitelData>(Data);
                            list.Remove(majitel);
                            Data = list;
                        }

                        ShowMessage("Success", "Majitel deleted successfully.", InfoBarSeverity.Success);
                    });
                }
                else
                {
                    ShowMessage("Error", "Failed to delete majitel.", InfoBarSeverity.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            }
        }
    }

    private async void ShowMessage(string title, string message, InfoBarSeverity severity)
    {
        MessageInfoBar.Title = title;
        MessageInfoBar.Message = message;
        MessageInfoBar.Severity = severity;
        MessageInfoBar.IsOpen = true;

        await Task.Delay(2000);

        MessageInfoBar.IsOpen = false;
    }
}