using DigimonDB.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigimonDB.App.Pages;

public partial class DigivolutionPlannerPage : ContentPage
{
    private EvolutionService? _evolutionService;

    public DigivolutionPlannerPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _evolutionService = Application.Current?.Handler?.MauiContext?.Services.GetService<EvolutionService>();
        if (_evolutionService is null)
        {
            DirectStatusLabel.Text = "Evolution service is unavailable.";
            PathStatusLabel.Text = "Evolution service is unavailable.";
            return;
        }

        var names = await _evolutionService.GetDigimonNamesAsync();
        FromPicker.ItemsSource = names;
        PathStartPicker.ItemsSource = names;
        PathTargetPicker.ItemsSource = names;

        if (names.Count == 0)
        {
            DirectStatusLabel.Text = "No Digimon found. Import data first.";
            PathStatusLabel.Text = "No Digimon found. Import data first.";
        }
    }

    private async void OnFromChanged(object? sender, EventArgs e)
    {
        if (_evolutionService is null || FromPicker.SelectedItem is not string fromName)
        {
            return;
        }

        var direct = await _evolutionService.GetDirectEvolutionsFromAsync(fromName);
        DirectEvolutionCollection.ItemsSource = direct;
        DirectStatusLabel.Text = direct.Count == 0
            ? "No direct evolutions found for this Digimon."
            : $"Direct evolutions: {direct.Count}";
    }

    private async void OnFindPathClicked(object? sender, EventArgs e)
    {
        if (_evolutionService is null)
        {
            return;
        }

        if (PathStartPicker.SelectedItem is not string start || PathTargetPicker.SelectedItem is not string target)
        {
            PathStatusLabel.Text = "Select both start and target Digimon.";
            PathCollection.ItemsSource = null;
            return;
        }

        var path = await _evolutionService.FindPathAsync(start, target);
        if (path.Count == 0)
        {
            PathStatusLabel.Text = "No path found.";
            PathCollection.ItemsSource = null;
            return;
        }

        var display = path.Select((link, index) => new PathStepViewModel(
            $"Step {index + 1}: {link.FromName} -> {link.ToName}",
            string.IsNullOrWhiteSpace(link.Condition) ? "Condition: none" : $"Condition: {link.Condition}"))
            .ToList();

        PathCollection.ItemsSource = display;
        PathStatusLabel.Text = $"Path length: {path.Count}";
    }

    private sealed record PathStepViewModel(string StepText, string ConditionText);
}
