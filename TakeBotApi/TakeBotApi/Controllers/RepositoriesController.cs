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
        public async Task<ActionResult<Dictionary<string, RepositoryDTO>>> GetRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Take Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/users/takenet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(
                await streamTask);

            var listRepositories = repositories.Where(x => x.Language == "C#")
                .OrderBy(y => y.CreatedDate)
                .Select(z => RepositoryToDTO(z))
                .ToList();

            return RepositoryDtoListToDictionary(listRepositories);

        }

        private static Dictionary<string, RepositoryDTO> RepositoryDtoListToDictionary(List<RepositoryDTO> listRepositories)
        {
            Dictionary<string, RepositoryDTO> repReturn;

            if (listRepositories.Any())
            {
                repReturn = new Dictionary<string, RepositoryDTO>()
                {
                    ["1"] = listRepositories[1]
                };
            }
            else
            {
                repReturn = new Dictionary<string, RepositoryDTO>();
            }

            for (int i = 1; i < 5; i++)
            {
                if (i < listRepositories.Count())
                {
                    repReturn[(i + 1).ToString()] = listRepositories[i];
                }
            }

            return repReturn;
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
