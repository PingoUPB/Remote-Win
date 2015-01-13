using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using WinRemote.App.Helpers;
using WinRemote.App.Models;

namespace WinRemote.App.Controllers
{
    /// <summary>
    /// API to PINGO's quickstart settings, namely the survey's duration possibilities and question types with their respective options.
    /// </summary>
    internal class Startsettings
    {
        #region fields

        /// <summary>
        /// Contains a survey's possible durations.
        /// </summary>
        public static List<Duration> DurationChoices { get; set; }

        /// <summary>
        /// Contains a survey's possible Types.
        /// </summary>
        public static List<SurveyType> TypeList { get; set; }

        #endregion fields

        #region methods
        /// <summary>
        ///Initializes DURATION_CHOICES and TypeList by connecting with the PINGO server.
        /// </summary>
        public static void Get()
        {
            GetQuestionTypes();
            GetDurationChoices();
        }

        /// <summary>
        /// Gets question types from PINGO server.
        /// Based on a convention with the development team. The question types are provided in an array called question_types.
        /// For every type there is a list of options, its german and english name and its type(how it is used in the PINGO code).
        /// </summary>
        private static void GetQuestionTypes()
        {
            var wsh = new WsHelper<SurveyType>();
            JObject json = JObject.Parse((wsh.Get("/api/question_types", new System.Collections.Hashtable())));//This request does not need any params
            TypeList = wsh.ConvertJsonToCollection(json["question_types"].ToString(), e => new SurveyType
            {
                NameDe = (string)e["name_de"],
                NameEn = (string)e["name_en"],
                Type = (string)e["type"],
                Options = TypeOption.ToList((JArray)e["options"], (JArray)e["options_de"], (JArray)e["options_en"])
            });
        }

        /// <summary>
        ///Gets possible durations for starting questions and surveys. By convention with development team, durations are provided as
        ///an array like [30,45,...] with integers representing the number of seconds.
        /// </summary>
        private static void GetDurationChoices()
        {
            var wsh = new WsHelper<Duration>();
            JObject json = JObject.Parse((wsh.Get("/api/duration_choices", new System.Collections.Hashtable())));
            var ja = (JArray)json["duration_choices"];
            DurationChoices = Duration.ToList(ja);
        }
        #endregion
    }
}