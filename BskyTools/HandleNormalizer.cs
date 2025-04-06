using System;

namespace BskyTools
{
    public class HandleNormalizer
    {
        public static string NormalizeHandle(string handle)
        {
            if (string.IsNullOrEmpty(handle))
            {
                throw new ArgumentException("Handle cannot be null or empty", nameof(handle));
            }

            // Check if the handle is a URL
            if (Uri.TryCreate(handle, UriKind.Absolute, out Uri? uri))
            {
                // Extract the user handle from the URL
                if (uri.Host == "bsky.app" && uri.AbsolutePath.StartsWith("/profile/", StringComparison.Ordinal))
                {
                    return uri.AbsolutePath.Replace("/profile/", "");
                }
            }

            // Check if the handle is a mention
            if (handle.StartsWith('@'))
            {
                return handle.Substring(1);
            }

            // Return the handle as is if it doesn't match any known format
            return handle;
        }
    }
}
