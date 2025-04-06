using System;
using System.Net.Http;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace BskyTools
{
    public partial class FetchDidCommand : InvokableCommand
    {
        public FetchDidCommand() { }

        public override ICommandResult Invoke()
        {
            CommandResult.ShowToast("Processing...");
            try
            {
                string did = ApiHelper.GetDidFromClipboardAsync().Result;
                ClipboardHelper.SetText(did);
                return CommandResult.ShowToast(did);
            }
            catch (AggregateException ex)
            {
                // Unwrap the AggregateException to get the actual exception
                Exception actualException = ex.GetBaseException();
                System.Diagnostics.Debug.WriteLine($"Exception: {actualException.Message}");
                return CommandResult.ShowToast(actualException.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                return CommandResult.ShowToast(ex.Message);
            }
        }
    }
}
