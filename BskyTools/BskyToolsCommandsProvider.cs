using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace BskyTools;

public partial class BskyToolsCommandsProvider : CommandProvider
{
    private readonly ListItem _listItem = new(new BskyToolsPage());

    public BskyToolsCommandsProvider()
    {
        Id = "BskyTools";
        DisplayName = "Bsky Tools";
        Icon = BskyToolsPage.BlueskyIcon;
    }

    public override ICommandItem[] TopLevelCommands() => new ICommandItem[] { _listItem };
}
