using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App2;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace App2.Pages.Crud;

public class CrudPageBase : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected InfoBar? MessageInfoBar;
    public InfoBar? PageInfoBar { get => MessageInfoBar; set => MessageInfoBar = value; }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected async Task<List<T>?> LoadDataAsync<T>(string endpoint)
    {
        try
        {
            var response = await HttpService.GetData(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            ShowMessage("Error", $"Failed to load data: {ex.Message}", InfoBarSeverity.Error);
            return null;
        }
    }

    protected async Task<bool> CreateItemAsync<T>(string endpoint, T item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpService.PostData(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Success", "Item created successfully.", InfoBarSeverity.Success);
                return true;
            }
            else
            {
                ShowMessage("Error", "Failed to create item.", InfoBarSeverity.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            return false;
        }
    }

    protected async Task<bool> UpdateItemAsync<T>(string endpoint, T item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpService.PutData(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Success", "Item updated successfully.", InfoBarSeverity.Success);
                return true;
            }
            else
            {
                ShowMessage("Error", "Failed to update item.", InfoBarSeverity.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            return false;
        }
    }

    protected async Task<bool> DeleteItemAsync(string endpoint, long id)
    {
        try
        {
            var response = await HttpService.DeleteData($"{endpoint}?id={id}");
            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Success", "Item deleted successfully.", InfoBarSeverity.Success);
                return true;
            }
            else
            {
                ShowMessage("Error", "Failed to delete item.", InfoBarSeverity.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            ShowMessage("Error", $"Exception: {ex.Message}", InfoBarSeverity.Error);
            return false;
        }
    }

    protected void ShowMessage(string title, string message, InfoBarSeverity severity)
    {
        if (MessageInfoBar == null) return;

        MessageInfoBar.Title = title;
        MessageInfoBar.Message = message;
        MessageInfoBar.Severity = severity;
        MessageInfoBar.IsOpen = true;

        _ = Task.Delay(2000).ContinueWith(_ =>
        {
            DispatcherQueue.TryEnqueue(() => MessageInfoBar.IsOpen = false);
        });
    }
}
