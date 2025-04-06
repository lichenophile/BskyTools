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
            var toastArgs = new ToastArgs
            {
                Result = CommandResult.Hide() // Override the default behavior to hide the command palette
            };

            toastArgs.Message = "Processing...";
            CommandResult.ShowToast(toastArgs);
            try
            {
                string did = ApiHelper.GetDidFromClipboardAsync().Result;
                ClipboardHelper.SetText(did);
                toastArgs.Message = did;
                return CommandResult.ShowToast(toastArgs);
            }
            catch (AggregateException ex)
            {
                // Unwrap the AggregateException to get the actual exception
                Exception actualException = ex.GetBaseException();
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                toastArgs.Message = actualException.Message;
                return CommandResult.ShowToast(toastArgs);
            }
            catch (Exception ex)
            {
                // Log the exception details
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                toastArgs.Message = ex.Message;
                return CommandResult.ShowToast(toastArgs);
            }
        }
    }
}
