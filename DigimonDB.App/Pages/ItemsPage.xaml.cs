using DigimonDB.Core.Models;
using DigimonDB.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigimonDB.App.Pages;

public partial class ItemsPage : ContentPage
{
    private List<Item> _allItems = [];

    public ItemsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var itemService = Application.Current?.Handler?.MauiContext?.Services.GetService<ItemService>();
        if (itemService is null)
        {
            return;
        }

        _allItems = await itemService.GetAllAsync();
        ApplyFilter(ItemSearchBar.Text);
    }

    private void OnSearchChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter(e.NewTextValue);
    }

    private void ApplyFilter(string? query)
    {
        IEnumerable<Item> filtered = _allItems;

        if (string.IsNullOrWhiteSpace(query) == false)
        {
            var search = query.Trim();
            filtered = filtered.Where(i =>
                i.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                i.Type.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                i.EvolvesFrom.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                i.EvolvesTo.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        ItemsCollection.ItemsSource = filtered.ToList();
    }
}
