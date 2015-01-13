using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RestSharp;
using WinRemote.App.Controllers;
using WinRemote.App.Models;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Provides WebServices like get and post method. Furthermore json conversion.
    /// </summary>
    /// <typeparam name="T">JSONConversion will be done by converting into Type T</typeparam>
    public class WsHelper<T> where T : IWsType
    {
        #region JSONConversion

        /// <summary>
        /// Converts a JArray into a list of type T.
        /// </summary>
        /// <param name="json">The json string containing the JArray</param>
        /// <param name="del">the method to be executed on each object in the JArray</param>
        /// <returns></returns>
        public List<T> ConvertJsonToCollection(string json, Func<JObject, T> del)
        {
            JArray col = JArray.Parse(json);

            return (from JObject q in col select del(q)).ToList();
        }

        /// <summary>
        /// Converts a JObject into an object of type T.
        /// </summary>
        /// <param name="json">The json string representing the JObject.</param>
        /// <param name="del">The method to be executed on the JObject in order to transform it into a T object.</param>
        /// <returns></returns>
        public T ConvertJsontoObj(string json, Func<JObject, T> del)
        {
            JObject ob = JObject.Parse(json);
            return del(ob);
        }

        #endregion JSONConversion

        #region Webmethods

        /// <summary>
        /// Sends a Get request to the server.
        /// </summary>
        /// <param name="url">The url to send the get request to.</param>
        /// <param name="paramlist">A Hashtable with keys representing the parameters' names and values representing the parameters' values</param>
        /// <returns>the content of the html response</returns>
        public string Get(string url, System.Collections.Hashtable paramlist)
        {
            return Action(url, paramlist, Method.GET);
        }

        /// <summary>
        /// Sends a Post request to the server
        /// </summary>
        /// <param name="url">The url to send the post request to</param>
        /// <param name="paramlist">A Hashtable with keys representing the parameters' names and values representing the parameters' values</param>
        /// <returns>the content of the html response</returns>
        public string Post(string url, System.Collections.Hashtable paramlist)
        {
            return Action(url, paramlist, Method.POST);
        }

        /// <summary>
        /// The actions that need to be executed in the Get and Post method.
        /// </summary>
        /// <param name="url">The url to send the post request to</param>
        /// <param name="paramlist">A Hashtable with keys representing the parameters' names and values representing the parameters' values</param>
        /// <param name="m">Either Method.POST or Method.GET</param>
        /// <returns></returns>
        private string Action(string url, System.Collections.Hashtable paramlist, Method m)
        {
            var client = new RestClient(Settings.BaseUrl);
            var request = new RestRequest(url, m);
            addParams(request, paramlist);

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) //can't connect to server
                NotificationHelper.ShowErrorAndClose(Properties.translate.NoConnectionError);
            if (string.IsNullOrWhiteSpace(response.Content))
                return null;

            //only accept json on GET requests. Necessary to catch exceptions if the BASE_URL in the Settings.cs file is wrong.
            if (m == Method.GET && !response.ContentType.Contains("json")) NotificationHelper.ShowErrorAndClose(Properties.translate.NoConnectionError);

            return response.Content;
        }

        /// <summary>
        /// Adds parameter from a Hashtable to the RestRequest
        /// </summary>
        /// <param name="request">the RestRequest to which the parameters will be added.</param>
        /// <param name="paramlist">A Hashtable with keys representing the parameters' names and values representing the parameters' values</param>
        private void addParams(RestRequest request, System.Collections.Hashtable paramlist)
        {
            foreach (System.Collections.DictionaryEntry de in paramlist)
            {
                request.AddParameter((string)de.Key, de.Value);
            }
        }

        #endregion Webmethods
    }
}