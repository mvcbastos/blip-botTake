using System;
using System.Text.Json.Serialization;

namespace BotTake.Models
{
    public class Owner
    {
        [JsonPropertyName("avatar_url")]
        public Uri AvatarUrl { get; set; }
    }
}