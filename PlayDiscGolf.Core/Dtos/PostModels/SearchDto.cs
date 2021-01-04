using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PlayDiscGolf.Core.Dtos.PostModels
{
    public class SearchDto
    {
        [Required (ErrorMessage = "Can´t leave search field empty")]
        [JsonPropertyName("query")]
        public string Query { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
