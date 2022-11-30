using System.Net;
using System.Text.Json;
using WSPRLive.Models;
using static WSPRLive.JsonConverters;

namespace WSPRLive;

public class Client
{
    private const string DEFAULT_HOST = "db1.wspr.live";

    private List<string> _wsprLiveHosts;
    private JsonSerializerOptions _jsonSerializerOptions;

    public Client()
    {
        _wsprLiveHosts = GetHosts();

        _jsonSerializerOptions = InitJsonSettings();
    }

    public Client(string host = DEFAULT_HOST)
    {
        _wsprLiveHosts = new List<string>();
        _wsprLiveHosts.Add(host);

        _jsonSerializerOptions = InitJsonSettings();
    }

    public async Task<WSPRLiveResult> Test()
    {
        var result = await ExecuteGet("SELECT * FROM wspr.rx LIMIT 500");

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end)
    {
        var query = $"SELECT * FROM wspr.rx WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}'";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, List<ulong> frequencies)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND frequency IN ({string.Join(",", frequencies)})";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, List<uint> distances)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND distance IN ({string.Join(",", distances)})";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, List<ulong> frequencies, List<uint> distances)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND frequency IN ({string.Join(",", frequencies)}) " +
            $"AND distance IN ({string.Join(",", distances)})";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, ulong minFrequency, ulong maxFrequency)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND frequency >= {minFrequency} AND frequency <= {maxFrequency}";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, uint minDistance, uint maxDistance)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND distance >= {minDistance} AND distance <= {maxDistance}";

        var result = await ExecuteGet(query);

        return result;
    }

    public async Task<WSPRLiveResult> GetSpots(DateTime start, DateTime end, ulong minFrequency, ulong maxFrequency, uint minDistance, uint maxDistance)
    {
        var query = $"SELECT * FROM wspr.rx " +
            $"WHERE time >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND time <= '{end.ToString("yyyy-MM-dd HH:mm:ss")}' " +
            $"AND frequency >= {minFrequency} AND frequency <= {maxFrequency} " +
            $"AND distance >= {minDistance} AND distance <= {maxDistance}";

        var result = await ExecuteGet(query);

        return result;
    }

    private JsonSerializerOptions InitJsonSettings()
    {
        return new JsonSerializerOptions
        {
            Converters = 
            {
                new LongConverter(),
                new DateTimeConverter()
            }
        };
    }

    private List<string> GetHosts()
    {
        using (var _httpClient = new HttpClient())
        {
            var result = _httpClient.GetStringAsync("https://wspr.live/endpoints.txt")
            .GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(result))
                result = DEFAULT_HOST;

            var list = result
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            return list;
        }
    }

    private async Task<WSPRLiveResult> ExecuteGet(string query)
    {
        HttpResponseMessage response = new HttpResponseMessage();

        foreach(var host in _wsprLiveHosts)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri($"https://{host}");
                
                var path = $"/?query={query} FORMAT JSON";
                response = await _httpClient.GetAsync(path);


                if (response.StatusCode == HttpStatusCode.OK)
                    break;
            }
        }

        if (response == null || response == default(HttpResponseMessage))
            return new WSPRLiveResult();

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<WSPRLiveResult>(responseContent, _jsonSerializerOptions);

        if (result == null)
            return new WSPRLiveResult();

        return result;
    }
}

