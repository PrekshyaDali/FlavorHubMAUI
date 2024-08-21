using PexelsDotNetSDK.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Services
{
    public class PexelsService
    {
        private readonly PexelsClient _PexelsClient;
        private List<string> _CachedPhotos = new List<string>();
        private Random _random = new Random();

        public PexelsService(string apiKey) { 
        
        _PexelsClient = new PexelsClient(apiKey);

        }

        public async Task FetchAndCachePhotosAsync(string query, int count = 80)
        {
            const int photosPerPage = 15; 
            int pagesToFetch = (int)Math.Ceiling((double)count / photosPerPage);

            var allPhotos = new List<string>();

            for (int page = 1; page <= pagesToFetch; page++)
            {
                // Fetch a page of photos
                var result = await _PexelsClient.SearchPhotosAsync(query, page: page);

                if (result != null && result.photos != null && result.photos.Count > 0)
                {
                    allPhotos.AddRange(result.photos
                        .Select(photo => photo.source?.original)
                        .Where(url => url != null));
                }
                else
                {
                    break; 
                }
            }

            _CachedPhotos = allPhotos.Take(count).ToList();
        }

        //shuffling the cached photos
        public List<string> GetShuffledCachedPhotos()
        {
            return _CachedPhotos.OrderBy(x=> _random.Next()).ToList();
        }

        public async Task<List<string>> SearchPhotosAsync(string query, int count = 80)
        {
            try
            {
                var result = await _PexelsClient.SearchPhotosAsync(query);

                if (result != null && result.photos != null && result.photos.Count > 0)
                {
                    var urls = result.photos
                        .Select(photo => photo.source?.original)
                        .Where(url => url != null)
                        .Take(count)
                        .ToList();

                    return urls;
                }
                else
                {
                    Console.WriteLine("No photos found for this query.");
                    return new List<string>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
