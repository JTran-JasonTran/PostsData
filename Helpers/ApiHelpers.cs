using AngularProject.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace AngularProject.Helpers
{
    public static class APIHelper
    {
        public static List<PostDto> GetDataFromAPI(string searchKey)
        {
            using (var client = new HttpClient())
            {
                string endpoint = "https://api.assessment.skillset.technology/a74fsg46d/posts?tag=";
                string searchApi = endpoint + searchKey;
                try
                {
                    var apiResponse = client.GetAsync(searchApi).Result;
                    dynamic apiResponseDynamic = apiResponse.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<PostsDto>(apiResponseDynamic);
                    return result.Posts;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
