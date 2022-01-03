using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService
{
    public class EmailConfig : IEmailConfig
    {
        private readonly NameValueCollection settings;
        private readonly string SECTION = "email";
        public EmailConfig()
        {
            this.settings = (NameValueCollection)ConfigurationManager.GetSection(SECTION);
        }

        public string Host
        {
            get { return Convert.ToString(settings["host"]); }
        }

        public int Port
        {
            get { return Convert.ToInt32(settings["port"]); }
        }

        public string Username
        {
            get { return Convert.ToString(settings["username"]); }
        }

        public string Password
        {
            get { return Convert.ToString(settings["password"]); }
        }
        public string Key
        {
            get { return Convert.ToString(settings["key"]); }
        }

        public bool IsActive
        {
            get { return Convert.ToBoolean(settings["active.flag"]); }
        }
    }

    public interface IEmailConfig
    {
        string Host { get; }
        int Port { get; }
        string Password { get; }
        string Username { get; }
        string Key { get; }
        bool IsActive { get; }
    }
}
