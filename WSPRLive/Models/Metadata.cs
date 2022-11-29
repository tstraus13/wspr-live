using System;
using System.Text.Json.Serialization;

namespace WSPRLive.Models
{
	public class Metadata
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";

		[JsonPropertyName("type")]
		public string Type { get; set; } = "";
	}
}

