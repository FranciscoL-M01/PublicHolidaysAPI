using System.Text.Json;

// implement the IHolidaysApiService
public class HolidaysApiService : IHolidaysApiService
{
    // declare client as an instance of  HttpClient 
    private static readonly HttpClient client;

    static HolidaysApiService()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://date.nager.at")
        };
    }

    public async Task<List<HolidayModel>?> GetHolidays(string countryCode, int year)
    {
        // building the Url of Nager.Date API
        var url = string.Format("/api/v3/PublicHolidays/{0}/{1}", year, countryCode);
        // Send an asynchronous GET request to the API endpoint
        var result = new List<HolidayModel>();
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // calling ReadAsStringAsync method to serialize the HTTP content to a string
            var stringResponse = await response.Content.ReadAsStringAsync();
            // use JsonSerializer to deserialize the JSON response string into a List of HolidayModel objects
            result = JsonSerializer.Deserialize<List<HolidayModel>>(stringResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        else
        {
            throw new HttpRequestException(response.ReasonPhrase);
        }

        return result;
    }
}