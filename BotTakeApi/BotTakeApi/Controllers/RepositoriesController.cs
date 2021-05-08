using BotTake.Models;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace BotTake.Controllers
{
    [RoutePrefix("api")]
    public class RepositoriesController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();

        [AcceptVerbs("GET")]
        [Route("repositories")]
        public async Task<List<Repository>> GetOldestRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Take Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/users/takenet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(
                await streamTask);

            return repositories.Where(x => x.Language == "C#")
                .OrderBy(y => y.CreatedDate).ToList().GetRange(0,5);
        }
    }
}
