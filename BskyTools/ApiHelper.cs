using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace BskyTools
{
    public static class ApiHelper
    {
        private const string BaseUrl = "https://public.api.bsky.app/xrpc/app.bsky.actor.getProfile?actor=";

        public static async Task<string> GetDidFromClipboardAsync()
        {
            string? clipboardText = ClipboardHelper.GetText();
            if (string.IsNullOrEmpty(clipboardText))
            {
                throw new InvalidOperationException("Clipboard empty or not text.");
            }

            // Normalize the handle
            string normalizedHandle = HandleNormalizer.NormalizeHandle(clipboardText);

            string requestUrl = BaseUrl + Uri.EscapeDataString(normalizedHandle);
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement) &&
                doc.RootElement.TryGetProperty("message", out JsonElement messageElement))
            {
                throw new InvalidOperationException($"[{errorElement.GetString()}] {messageElement.GetString()}");
            }

            if (doc.RootElement.TryGetProperty("did", out JsonElement didElement))
            {
                return didElement.GetString() ?? throw new InvalidOperationException("DID not found in response");
            }
            else
            {
                throw new InvalidOperationException("DID not found in response");
            }
        }
    }
}
