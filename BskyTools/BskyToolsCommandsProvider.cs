using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace BskyTools;

public partial class BskyToolsCommandsProvider : CommandProvider
{
    private readonly ListItem _listItem = new(new BskyToolsPage());
    private readonly CommandItem _fetchDidCommandItem;

    public BskyToolsCommandsProvider()
    {
        Id = "BskyTools";
        DisplayName = "Bsky Toolbox"; // Name of the command provider (displayed in settings)
        Icon = BskyToolsPage.BlueskyIcon;

        var fetchDidCommand = new FetchDidCommand();
        _fetchDidCommandItem = new CommandItem
        {
            Icon = BskyToolsPage.BlueskyIcon,
            Title = "DID to Clipboard",
            Subtitle = "Replaces Bsky handle with DID",
            Command = fetchDidCommand // this is the command that will be executed when the item is clicked
        };
    }

    public override ICommandItem[] TopLevelCommands() => new ICommandItem[] { _listItem, _fetchDidCommandItem };
}

