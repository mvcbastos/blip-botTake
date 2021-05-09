using BotTake.Models;
using System;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }
    }
}