using MyWork.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService
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

        public UserProfile GetUserProfile(string username)
        {
            var res = httpClient.GetAsync(string.Format(config.UserProfileUrl, username)).Result;
            var json = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<UserProfile>(json);
        }
    }

    public interface IRestServiceClient
    {
        UserProfile GetUserProfile(string username);
    }
}
