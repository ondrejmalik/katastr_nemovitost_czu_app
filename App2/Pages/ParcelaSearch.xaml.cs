using System;
using System.Text.Json;
using App2.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;

namespace App2.Pages;

public sealed partial class ParcelaSearch
{
    public ParcelaSearch()
    {
        InitializeComponent();
        Loaded += ParcelaSearch_Loaded;
    }

    private void ParcelaSearch_Loaded(object sender, RoutedEventArgs e)
    {
        KatastralniUzemiTextBox.Text = "Město Brno";
        ParcelniCisloTextBox.Text = "1";
        CastParcelyTextBox.Text = "1";
        StavebniRadioButton.IsChecked = true;
        PozemkovyRadioButton.IsChecked = false;
        Loaded -= ParcelaSearch_Loaded;
    }


    private void SearchParcela(object sender, RoutedEventArgs e)
    {
        var katastralniUzemi = KatastralniUzemiTextBox.Text;
        var parcelniCislo = ParcelniCisloTextBox.Text;
        var castParcely = CastParcelyTextBox.Text;
        var jeStavebni = (bool)StavebniRadioButton.IsChecked!;
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            var uri =
                $"/parcela?katastralni_uzemi={katastralniUzemi}&parcelni_cislo={parcelniCislo}&cast_parcely={castParcely}&je_stavebni={jeStavebni.ToString().ToLower()}";
            try
            {
                var response = await HttpService.GetData(uri);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<System.Collections.Generic.List<ParcelaData>>(json);

                data!.ForEach(parcela => { parcela.KatastralniUzemi = katastralniUzemi; });

                DispatcherQueue.TryEnqueue(() =>
                {
                    Frame.Navigate(typeof(ParcelaDisplay), data, new SuppressNavigationTransitionInfo());
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception();
            }
        });
    }


    private void GoBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage), new SuppressNavigationTransitionInfo());
    }
}