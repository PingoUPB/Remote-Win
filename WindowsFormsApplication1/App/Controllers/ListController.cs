using System.Collections;
using System.Collections.Generic;
using WinRemote.App.Models;

namespace WinRemote.App.Controllers
{
    /// <summary>
    /// Organizes necessary lists. Contains lists for questions and a Hashtable mapping tags and questions.
    /// </summary>
    internal class ListController
    {
        #region fields

        /// <summary>
        /// All the questions a user has.
        /// </summary>
        public List<Question> AllQuestionList { get; set; }

        /// <summary>
        /// Maps tags and questions.
        /// </summary>
        public Hashtable TagTable { get; set; }

        #endregion fields

        #region methods
        /// <summary>
        /// Builds Lists
        /// </summary>
        public ListController()
        {
            BuildAllQuestionList();
            BuildTagHash();
        }

        /// <summary>
        /// Gets all Questions from Server and stores them into AllQuestionList.
        /// </summary>
        private void BuildAllQuestionList()
        {
            AllQuestionList = Question.All();
        }

        /// <summary>
        /// Builds a Hashtable which provides tag => question queries.
        /// </summary>
        private void BuildTagHash()
        {
            Hashtable tagtable = new Hashtable();
            tagtable.Add(Properties.translate.AllTags, AllQuestionList);

            //Check every question with every tag.
            foreach (Question q in AllQuestionList)
                foreach (string t in q.Tags)
                {
                    if (tagtable.ContainsKey(t)) //tag already in table
                        ((List<Question>)(tagtable[t])).Add(q);
                    else //tag is not in table
                    {
                        List<Question> l = new List<Question>();
                        l.Add(q);
                        tagtable.Add(t, l);
                    }
                }
            TagTable = tagtable;
        }
        #endregion
    }
}