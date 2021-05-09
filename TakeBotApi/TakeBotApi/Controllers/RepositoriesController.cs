using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TakeBotApi.Models;
using TodoApi.Models;

namespace TakeBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        // GET: api/Repositories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepositoryDTO>>> GetRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Take Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/users/takenet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(
                await streamTask);

            return repositories.Where(x => x.Language == "C#")
                .OrderBy(y => y.CreatedDate)
                .Select(x => RepositoryToDTO(x))
                .ToList()
                .GetRange(0, 5);
        }

        private static RepositoryDTO RepositoryToDTO(Repository repository) =>
            new RepositoryDTO
            {
                Name = repository.Name,
                Description = repository.Description,
                Owner = repository.Owner
            };
    }
}
