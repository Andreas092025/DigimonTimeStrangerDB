using DigimonDB.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigimonDB.App.Pages;

public partial class DatabasePage : ContentPage
{
    private DashboardService? _dashboard;
    private ImportService? _importService;

    public DatabasePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _dashboard = Application.Current?.Handler?.MauiContext?.Services.GetService<DashboardService>();
        _importService = Application.Current?.Handler?.MauiContext?.Services.GetService<ImportService>();

        await RefreshCountsAsync();
    }

    private async Task RefreshCountsAsync()
    {
        if (_dashboard is null)
        {
            return;
        }

        var counts = await _dashboard.GetCountsAsync();
        DigimonCountLabel.Text = counts.Digimon.ToString();
        MoveCountLabel.Text = counts.Moves.ToString();
        ItemCountLabel.Text = counts.Items.ToString();
        EvolutionCountLabel.Text = counts.Evolutions.ToString();
    }

    private async void OnImportClicked(object? sender, EventArgs e)
    {
        if (_importService is null)
        {
            ImportStatusLabel.Text = "Import service is unavailable.";
            return;
        }

        var dataRoot = Path.Combine(AppContext.BaseDirectory, "SeedData", "Data");

        try
        {
            ImportButton.IsEnabled = false;
            ImportActivity.IsVisible = true;
            ImportActivity.IsRunning = true;
            ImportStatusLabel.Text = "Importing data...";

            var summary = await _importService.ImportFromFolderAsync(dataRoot);
            ImportStatusLabel.Text =
                $"Import complete. Added Digimon: {summary.DigimonAdded}, Moves: {summary.MovesAdded}, Items: {summary.ItemsAdded}.";

            await RefreshCountsAsync();
        }
        catch (Exception ex)
        {
            ImportStatusLabel.Text = $"Import failed: {ex.Message}";
        }
        finally
        {
            ImportActivity.IsRunning = false;
            ImportActivity.IsVisible = false;
            ImportButton.IsEnabled = true;
        }
    }
}
