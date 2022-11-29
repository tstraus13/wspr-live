using System;
using System.Text.Json.Serialization;

namespace WSPRLive.Models
{
	public class Statistics
	{
        [JsonPropertyName("elapsed")]
        public double TimeElapsed { get; set; }

        [JsonPropertyName("rows_read")]
        public long RowsRead { get; set; }

		[JsonPropertyName("bytes_read")]
		public long BytesRead { get; set; }
	}
}

