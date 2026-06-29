using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASPA008_1.Filters
{
    public class InfoAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        public const string Wikipedia = "WIKI";
        public const string Facebook = "FACE";

        private readonly string _infoType;

        public InfoAsyncActionFilter(string infoType = "")
        {
            _infoType = infoType.ToUpper();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var repo = context.HttpContext.RequestServices.GetService<IRepository>();
            if (repo == null)
            {
                await next();
                return;
            }

            if (!context.ActionArguments.TryGetValue("id", out var idObj) || !(idObj is int id) || id <= 0)
            {
                await next();
                return;
            }

            var celebrity = repo.GetCelebrityById(id);
            if (celebrity == null)
            {
                await next();
                return;
            }

            if (_infoType.Contains(Wikipedia))
            {
                Dictionary<string, string> wikiReferences;
                try
                {
                    wikiReferences = await WikiInfoCelebrity.GetReferences(celebrity.FullName);
                }
                catch
                {
                    wikiReferences = new Dictionary<string, string>();
                }

                if (wikiReferences.Count == 0)
                {
                    wikiReferences[celebrity.FullName] = $"https://en.wikipedia.org/wiki/{Uri.EscapeDataString(celebrity.FullName)}";
                }
                context.HttpContext.Items[Wikipedia] = wikiReferences;
            }

            if (_infoType.Contains(Facebook))
            {
                context.HttpContext.Items[Facebook] = GetFromFacebook(celebrity.FullName);
            }

            await next();
        }

        private static string GetFromFacebook(string fullName)
        {
            return "Info from Face";
        }
    }
    public class WikiInfoCelebrity
    {
        private readonly HttpClient _client;
        private readonly Dictionary<string, string> _wikiReferences;
        private readonly string _wikiURI;

        private WikiInfoCelebrity(string fullName)
        {
            _client = new HttpClient();
            _wikiReferences = new Dictionary<string, string>();
            _wikiURI = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={Uri.EscapeDataString(fullName)}&prop=info&format=json";
        }

        public static async Task<Dictionary<string, string>> GetReferences(string fullName)
        {
            var info = new WikiInfoCelebrity(fullName);
            HttpResponseMessage message = await info._client.GetAsync(info._wikiURI);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await message.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonString);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() >= 4)
                {
                    var titles = root[1];
                    var urls = root[3];

                    if (titles.ValueKind == JsonValueKind.Array && urls.ValueKind == JsonValueKind.Array)
                    {
                        int count = Math.Min(titles.GetArrayLength(), urls.GetArrayLength());
                        for (int i = 0; i < count; i++)
                        {
                            var title = titles[i].GetString();
                            var url = urls[i].GetString();
                            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(url))
                            {
                                info._wikiReferences[title] = url;
                            }
                        }
                    }
                }
            }

            return info._wikiReferences;
        }
    }
}
