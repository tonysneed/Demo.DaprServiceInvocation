using Dapr.Client;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DaprConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create Dapr HttpClient
            var client = DaprClient.CreateInvokeHttpClient(appId: "my-web-api");

            // Send Get request
            var response = await client.GetAsync("weatherforecast");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

            // Show results
            foreach (var item in result)
            {
                Console.WriteLine($"Date: {item.Date} TempC: {item.TemperatureC} TempF: {item.TemperatureF} Summary: {item.Summary}");
            }
        }
    }
}
