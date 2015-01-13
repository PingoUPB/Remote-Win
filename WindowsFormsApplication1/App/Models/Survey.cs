using Newtonsoft.Json.Linq;
using WinRemote.App.Controllers;
using WinRemote.App.Helpers;

namespace WinRemote.App.Models
{
    /// <summary>
    /// Survey class similiar to the survey model in PINGO Webapp
    /// </summary>
    public class Survey : IWsType
    {
        #region fields
        /// <summary>
        /// survey's ID
        /// </summary>
        public string Id { get; set; }
        /// <summary/>

        /// <summary>
        /// survey's name/title
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region methods
        /// <summary>
        /// Converts a JToken into a survey.
        /// </summary>
        /// <param name="s">the JToken to be converted</param>
        /// <returns>survey object with name and id</returns>
        public static Survey FromJson(JToken s)
        {
            var su = new Survey {Name = (string) s["name"], Id = (string) s["_id"]};
            return su;
        }

        /// <summary>
        /// Posts a survey to the PINGO server
        /// </summary>
        /// <param name="eventToken">the event's token where the survey is posted in</param>
        /// <param name="type">the survey's type</param>
        /// <param name="duration">the survey's duration</param>
        /// <param name="options">the survey's options</param>
        public static void PostSurvey(string eventToken, string type, string duration, string options)
        {
            var wsh = new WsHelper<Question>();
            var paramlist = new System.Collections.Hashtable
            {
                {"id", eventToken},
                {"options", options},
                {"auth_token", Settings.AuthToken},
                {"q_type", type},
                {"duration", duration}
            };
            wsh.Post("events/" + eventToken + "/quick_start.js", paramlist);
        }

        /// <summary>
        /// Stops a the latest survey in a given event
        /// </summary>
        /// <param name="e">the event, where the latest survey shall be stopped</param>
        public static void StopSurvey(Event e)
        {
            var wsh = new WsHelper<Survey>();
            var paramlist = new System.Collections.Hashtable {{"auth_token", Settings.AuthToken}, {"stoptime", "0"}};
            wsh.Post("events/" + e.Token + "/surveys/" + e.LatestSurvey.Id + "/stop", paramlist);
        }
        #endregion
    }
}