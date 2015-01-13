using System;

namespace WinRemote.App.Models
{
    class UrlSettings
    {
        public String BaseUrl {get;set;}
        public String BaseSocketUrl { get; set; }

        public UrlSettings(String baseUrl, String baseSocketUrl)
        {
            BaseUrl = baseUrl;
            BaseSocketUrl = baseSocketUrl;
        }
    }
}
