using System.Reflection;
using System.Text.Json;

namespace MarkdownThing
{
    public enum UpdateCheckResult
    {
        UpToDate,
        UpdateAvailable,
        CheckFailed
    }

    public readonly record struct UpdateCheckOutcome(UpdateCheckResult Result, string? ReleaseUrl = null);

    public static class UpdateChecker
    {
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(15);

        public static async Task<UpdateCheckOutcome> CheckAsync()
        {
            using var client = new HttpClient { Timeout = RequestTimeout };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MarkdownThing-UpdateCheck");

            try
            {
                var json = await client.GetStringAsync(AppConstants.ReleasesApiUrl);
                using var doc = JsonDocument.Parse(json);
                var tag = doc.RootElement.GetProperty("tag_name").GetString();
                if (string.IsNullOrEmpty(tag))
                    return new UpdateCheckOutcome(UpdateCheckResult.CheckFailed);

                var latestText = tag.TrimStart('v', 'V');
                if (!Version.TryParse(latestText, out var latest))
                    return new UpdateCheckOutcome(UpdateCheckResult.CheckFailed);

                var currentText = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0";
                if (!Version.TryParse(currentText, out var current))
                    current = new Version(0, 0, 0);

                if (latest > current)
                {
                    var url = doc.RootElement.GetProperty("html_url").GetString();
                    return new UpdateCheckOutcome(UpdateCheckResult.UpdateAvailable, url);
                }

                return new UpdateCheckOutcome(UpdateCheckResult.UpToDate);
            }
            catch
            {
                return new UpdateCheckOutcome(UpdateCheckResult.CheckFailed);
            }
        }
    }
}
