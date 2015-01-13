using System;
using System.Collections.Generic;
using WinRemote.App.Controllers;
using WinRemote.App.Helpers;

namespace WinRemote.App.Models
{
	/// <summary>
	/// Represents a question from the PINGO system
	/// </summary>
	public class Question : IWsType, IComparable
    {
        #region fields
        /// <summary>
        /// The question's ID
        /// </summary>
        public string Id { get; set; }
		/// <summary>
		/// The question's name, given by user in the Webapp.
		/// </summary>
		public string Name { get; set; }
        
		// [DataMember(Name="type")]
		/// <summary>
		/// Question's type
		/// </summary>
		public string Type { get; set; }

		// [DataMember(Name="question_options")]
		/// <summary>
		/// 
		/// </summary>
		public object Options { get; set; }

		// [DataMember(Name="tags_array")]
		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<string> Tags { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Gets all the user's questions from the PINGO Server and returns them in a sorted list.
        /// </summary>
        /// <returns></returns>
        public static List<Question> All() {
			WsHelper<Question> tws = new WsHelper<Question> ();
            var paramlist = new System.Collections.Hashtable();
            paramlist.Add("auth_token", Settings.AuthToken);
			var list= tws.ConvertJsonToCollection(tws.Get("questions",paramlist), q => {//use this method to create questions from every index in the JArray
				return new Question {Name=(string)q["name"], Type=(string)q["type"], Id=(string)q["_id"], Tags=q["tags_array"].ToObject<IEnumerable<string>>()};
			});
            list.Sort();
            return list;
		}

        /// <summary>
        /// Provides the Question's name as ToString, very useful in DropDown menus.
        /// </summary>
        /// <returns></returns>
       public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Posts a question to the server to set it up as survey.
        /// </summary>
        /// <param name="eventToken">the event the question is posed in</param>
        /// <param name="questionId">the id of the posted question</param>
        /// <param name="duration">the duration of the survey to be created</param>
       public static void PostQuestion(string eventToken, string questionId, string duration)
       {
           WsHelper<Question> wsh = new WsHelper<Question>();
           var paramlist = new System.Collections.Hashtable();
           paramlist.Add("auth_token", Settings.AuthToken);
           paramlist.Add("question",  questionId);
           paramlist.Add("duration", duration);
           wsh.Post("events/" + eventToken + "/add_question.js", paramlist);
       }

        /// <summary>
        /// Finds a all the questions with a specified tag.
        /// </summary>
        /// <param name="tag">the tag to be filtered by</param>
        /// <returns></returns>
       public static List<Question> Find_by_Tag(string tag)
       {
           var taggedList = new List<Question>();
           foreach (Question q in Settings.AllQuestionList){
               foreach (string t in q.Tags)
                   if (tag.Equals(t))//survey has the tag
                       taggedList.Add(q);

           }
           return taggedList;

       }

        /// <summary>
        /// Enables alphabetical sorting by comparing the questions' names.
        /// </summary>
        /// <param name="q">the question to be compared to</param>
        /// <returns>the comparison result based on the CompareTo method of strings</returns>
       public int CompareTo(Object q) { return this.Name.CompareTo(((Question)q).Name); }
        #endregion
    }
}

