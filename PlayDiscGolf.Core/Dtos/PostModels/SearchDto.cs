using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PlayDiscGolf.Core.Dtos.PostModels
{
    public class SearchDto
    {
        public string Query { get; set; }
        public string Type { get; set; }
    }
}
