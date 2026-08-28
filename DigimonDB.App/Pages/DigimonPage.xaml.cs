using DigimonDB.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigimonDB.App.Pages;

public partial class DigimonPage : ContentPage
{
    private DigimonService? _digimonService;

    public DigimonPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _digimonService = Application.Current?.Handler?.MauiContext?.Services.GetService<DigimonService>();
        if (_digimonService is null)
        {
            StatusLabel.Text = "Digimon service is unavailable.";
            return;
        }

        if (SortPicker.SelectedIndex < 0)
        {
            SortPicker.SelectedIndex = 0;
        }

        var digimons = await _digimonService.GetAllAsync();
        DigimonCollection.ItemsSource = digimons;
        StatusLabel.Text = $"Loaded {digimons.Count} Digimon";
    }

    private async void OnApplyFiltersClicked(object? sender, EventArgs e)
    {
        await RefreshFilteredAsync();
    }

    private async void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        await RefreshFilteredAsync();
    }

    private async void OnSortChanged(object? sender, EventArgs e)
    {
        await RefreshFilteredAsync();
    }

    private async Task RefreshFilteredAsync()
    {
        if (_digimonService is null)
        {
            return;
        }

        var minLevel = ParseLevel(MinLevelEntry.Text);
        var maxLevel = ParseLevel(MaxLevelEntry.Text);

        var sortBy = SortPicker.SelectedIndex switch
        {
            1 => DigimonSortBy.NameDesc,
            2 => DigimonSortBy.IdAsc,
            3 => DigimonSortBy.IdDesc,
            _ => DigimonSortBy.NameAsc
        };

        var digimons = await _digimonService.GetFilteredAsync(SearchBar.Text, minLevel, maxLevel, sortBy);
        DigimonCollection.ItemsSource = digimons;
        StatusLabel.Text = $"Showing {digimons.Count} Digimon";
    }

    private static int? ParseLevel(string? value)
    {
        return int.TryParse(value, out var parsed) ? parsed : null;
    }
}
