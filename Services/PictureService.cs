using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Akan.Services
{
    public class PictureService
    {
        private readonly HttpClient _http;
        
        public PictureService(HttpClient http)
            => _http = http;

            public async Task<Image> GetPictureWithTagAsync(string tag)
            {
                var url = await _http.GetStringAsync($"https://nekos.life/api/v2/img/{tag}");
                return new Image(url.Substring(0, url.Length - 3).Replace("{\"url\":\"", ""));
            }

    }
}