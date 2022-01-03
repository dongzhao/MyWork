using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService
{
    public class RestClientConfig : IRestClientConfig
    {
        private readonly NameValueCollection settings;
        private readonly string REST_SETTING = "restClient";
        public RestClientConfig()
        {
            this.settings = (NameValueCollection)ConfigurationManager.GetSection(REST_SETTING);
        }

        public string BaseUrl
        {
            get { return Convert.ToString(settings["base.url"]); }
        }

        public string UserProfileUrl
        {
            get { return Convert.ToString(settings["profile.url"]); }
        }
    }

    public interface IRestClientConfig
    {
        string BaseUrl { get; }
        string UserProfileUrl { get; }
    }
}
