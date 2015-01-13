using System;
using System.Collections.Generic;
using WinRemote.App.Controllers;
using WinRemote.App.Helpers;

namespace WinRemote.App.Models
{
    /// <summary>
    /// Event class similiar to PINGO's Rails Event model, contains Id,Name, Token and LatestSurvey
    /// </summary>
	public class Event : IWsType, IComparable
    {
        #region fields
        /// <summary>
        /// The event's ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The event's name. Given by the user in the PINGO Webapp.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The event's token. Is used by participants to participate in surveys.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The event's latest survey.
        /// </summary>
        public Survey LatestSurvey { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Gets all Events from Server.
        /// </summary>
        /// <returns>Sorted list of all Events the current user has access to</returns>
		public static List<Event> All() {
            
			WsHelper<Event> tws = new WsHelper<Event> ();
            var paramlist = new System.Collections.Hashtable();
            paramlist.Add("auth_token", Settings.AuthToken);
            //Convert Json into list of Events (without latest surveys)
            var list = tws.ConvertJsonToCollection(tws.Get("events",paramlist),   e =>
            {
                return new Event { Id = (string)e["_id"], Name = (string)e["name"], Token = (string)e["token"] };
            });

            list.Sort();
            return list;
            
		}

       /// <summary>
        /// Converts a JObject into an Event
       /// </summary>
       /// <returns>Event Object converted from JSON</returns>
        public static Func<Newtonsoft.Json.Linq.JObject, Event> FromJson()
        {
            return e =>
            {
                return new Event { Id = (string)e["_id"], Name = (string)e["name"], Token = (string)e["token"], LatestSurvey = Survey.FromJson(e["latest_survey"]) };
            };
        }
        /// <summary>
        /// 
        /// </summary>
        public void ReloadLatestSurvey()
        {
            var tws = new WsHelper<Event>();
            var paramlist = new System.Collections.Hashtable();
            paramlist.Add("auth_token", Settings.AuthToken);
            Settings.Session = tws.ConvertJsontoObj(tws.Get("events/" + Settings.Session.Token, paramlist), FromJson());
        }
        /// <summary>
        /// Overrides toString method displays the event in form: Name(Token)
        /// </summary>
        /// <returns>String in format: Name(Token)</returns>
       public override string ToString()
        {
            return Name+" ("+Token+")";
        }

        /// <summary>
       /// Compares two Events to enable alphabetical sorting.
        /// </summary>
        /// <param name="e">The Event to be compared with.</param>
        /// <returns>Comparison result based on comparing the Events' names.</returns>
       public int CompareTo(object e) { return Name.CompareTo(((Event)e).Name); }

        #endregion
    }
}

