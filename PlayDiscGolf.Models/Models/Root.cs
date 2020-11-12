using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models
{
    public class Root
    {
        [JsonPropertyName("courses")]
        public List<Cours> Courses { get; set; }
        public class Cours
        {
            [JsonPropertyName("ID")]
            public string ID { get; set; }

            [JsonPropertyName("ParentID")]
            public object ParentID { get; set; }

            [JsonPropertyName("Name")]
            public string Name { get; set; }

            [JsonPropertyName("Fullname")]
            public string Fullname { get; set; }

            [JsonPropertyName("Type")]
            public string Type { get; set; }

            [JsonPropertyName("CountryCode")]
            public string CountryCode { get; set; }

            [JsonPropertyName("Area")]
            public string Area { get; set; }

            [JsonPropertyName("City")]
            public object City { get; set; }

            [JsonPropertyName("Location")]
            public object Location { get; set; }

            [JsonPropertyName("X")]
            public string X { get; set; }

            [JsonPropertyName("Y")]
            public string Y { get; set; }

            [JsonPropertyName("Enddate")]
            public string Enddate { get; set; }
        }
    }
}
