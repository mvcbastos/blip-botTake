using BotTake.Models;

namespace TakeBotApi.Models
{
    public class RepositoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Owner Owner { get; set; }
    }
}
