using System.Collections.Generic;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.IO;

namespace BskyTools;

internal sealed partial class BskyToolsPage : DynamicListPage
{
    public static readonly IconInfo BlueskyIcon = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "Assets\\Bluesky-Logo.png"));
    private readonly List<IListItem> _items = new();
    private readonly FetchDidCommand _fetchDidCommand;
    private readonly CommandContextItem _fetchDidContextMenuItem;

    public BskyToolsPage()
    {
        Icon = BlueskyIcon;
        Title = "Bluesky Toolbox"; //Displays bottom left of list
        Name = "Bsky Toolbox"; //Displays as main name
        PlaceholderText = "No input needed. Runnning a command will extract valid handles from clipboard"; // Search bar ghost text

        _fetchDidCommand = new FetchDidCommand();
        _fetchDidContextMenuItem = new CommandContextItem(_fetchDidCommand);

        InitializeItems();
    }

    private void InitializeItems()
    {
        var firstItem = new ListItem(this)
        {
            Icon = BskyToolsPage.BlueskyIcon,
            Title = "DID to Clipboard",
            Subtitle = "Replaces Bsky handle with DID",
            Command = _fetchDidCommand // this is the command that will be executed when the item is clicked
        };

        _items.Add(firstItem);
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        // Implement the method to update search text if needed
    }

    public override IListItem[] GetItems() => _items.ToArray();
}
