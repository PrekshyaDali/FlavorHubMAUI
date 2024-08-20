using PexelsDotNetSDK.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Services
{
    public class PexelsService
    {
        private readonly PexelsClient _PexelsClient;
       public PexelsService(string apiKey) { 
        
        _PexelsClient = new PexelsClient(apiKey);

        }

        //fetching the user query data from the pexel api
        public async Task<List<string>> SearchPhotosAsync(string query)
        {
            try
            {
                var result = await _PexelsClient.SearchPhotosAsync(query);

                if (result != null && result.photos != null && result.photos.Count > 0) 
                {
                    // Ensure the expected types
                    var urls = result.photos.Select(photo => photo.source?.original ?? "default_url").ToList();
                    return urls;
                }
                else
                {
                    Console.WriteLine("No photos of this query");
                    return new List<string>();
                }

            }catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data:{ex.Message}");
                return new List<string>();
            }
        }
    }
}
