using System;
using System.Text.Json.Serialization;

namespace WSPRLive.Models
{
	public class Spot
	{
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("time")]
        public DateTime TimeReceived { get; set; }

        [JsonPropertyName("band")]
        public Band Band { get; set; }

        [JsonPropertyName("rx_sign")]
        public string RxSign { get; set; } = "";

        [JsonPropertyName("rx_lat")]
        public float RxLatitude { get; set; }

        [JsonPropertyName("rx_lon")]
        public float RxLongitude { get; set; }

        [JsonPropertyName("rx_loc")]
        public string RxLocation { get; set; } = "";

        [JsonPropertyName("tx_sign")]
        public string TxSign { get; set; } = "";

        [JsonPropertyName("tx_lat")]
        public float TxLatitude { get; set; }

        [JsonPropertyName("tx_lon")]
        public float TxLongitude { get; set; }

        [JsonPropertyName("tx_loc")]
        public string TxLocation { get; set; } = "";

        [JsonPropertyName("distance")]
        public uint Distance { get; set; }

        [JsonPropertyName("azimuth")]
        public uint Azimuth { get; set; }

        [JsonPropertyName("rx_azimuth")]
        public uint RxAzimuth { get; set; }

        [JsonPropertyName("frequency")]
        public ulong Frequency { get; set; }

        [JsonPropertyName("power")]
        public int Power { get; set; }

        [JsonPropertyName("snr")]
        public int SnR { get; set; }

        [JsonPropertyName("drift")]
        public int Drift { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; } = "";

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}

