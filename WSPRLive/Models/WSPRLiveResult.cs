using System;
using System.Text.Json.Serialization;

namespace WSPRLive.Models
{
	public class WSPRLiveResult
	{
		[JsonPropertyName("meta")]
		public List<Metadata> Metadata { get; set; } = new List<Metadata>();

		[JsonPropertyName("data")]
		public List<Spot> Spots { get; set; } = new List<Spot>();

		[JsonPropertyName("rows")]
		public long Rows { get; set; }

		[JsonPropertyName("rows_before_limit_at_least")]
		public long RowsBeforeLimitAtLeast { get; set; }

		[JsonPropertyName("statistics")]
		public Statistics Statistics { get; set; } = new Statistics();
	}
}

