using AngularProject.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace AngularProject.Helpers
{
    public static class APIHelper
    {
        public static List<Post> GetDataFromAPI(string apiPath,string searchKey)
        {
            using (var client = new HttpClient())
            {
                string apiEndpoint = apiPath + searchKey.Trim();
                try
                {
                    var apiResponse = client.GetAsync(apiEndpoint).Result;
                    dynamic apiResponseDynamic = apiResponse.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<PostList>(apiResponseDynamic);
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
