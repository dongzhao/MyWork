using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Service.RestClient
{
    public class RestServiceClient : IRestServiceClient
    {
        private readonly IRestClientConfig config;
        private readonly HttpClient httpClient;
        public RestServiceClient(IRestClientConfig cfg)
        {
            this.config = cfg;
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(config.BaseUrl);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public UserProfileDto GetUserProfile(string username)
        {
            var res = httpClient.GetAsync(string.Format(config.UserProfileUrl, username)).Result;
            var json = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<UserProfileDto>(json);
        }

        public UserProfileDto SaveUserProfile(UserProfileDto userProfile)
        {
            var content = new System.Net.Http.StringContext(JsonConvert.SerializeObject(userProfile), Encode.UTF8, "application/json");
            var res = httpClient.PostAsync("/api/v1/userprofile", content).Result;
            var json = res.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<UserProfileDto>(json);
            return GetUserProfile(userProfile.UserName);
        }

    }

    public interface IRestServiceClient
    {
        UserProfileDto GetUserProfile(string username);
    }
}
