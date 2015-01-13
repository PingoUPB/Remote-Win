using System.Collections.Generic;
using WinRemote.App.Models;
using WinRemote.App.Views;

namespace WinRemote.App.Controllers
{
    /// <summary>
    ///  Saves all the general Settings. Connects most of the other controllers with GUI 
    /// </summary>
	public class Settings
    {
        /// <summary>
        /// This version is checked everytime with the version stored in the database. If this one is newer, the old database is deleted.
        /// This is necessary for changes in the Database model.
        /// </summary>
        public static double DatabaseRelevantVersion = 1.0;

        /// <summary>
        /// Reference to the running instance of MainForm in this application.
        /// </summary>
        public static MainForm F1;
        /// <summary>
        /// URL to the target PINGO Server.
        /// </summary>
		public static string BaseUrl = "https://pingo.upb.de";
        /// <summary>
        /// The URL to the target PINGO WebSocket.
        /// </summary>
        public static string BaseSocketUrl = "http://socket.pingo.cc:8080/";


        /// <summary>
        /// URL to the default PINGO Server.
        /// </summary>
        public static string DefaultUrl = "http://pingo.upb.de";
        /// <summary>
        /// The URL to the default PINGO WebSocket.
        /// </summary>
        public static string DefaultSocketUrl = "http://socket.pingo.cc:8080/";

        /// <summary>
        /// The URL To Sign Up page.
        /// </summary>
        public static string BaseSignUpUrl = "http://pingo.upb.de/users/sign_up";

        /// <summary>
        /// Name for the NDatabase file.
        /// </summary>
        public static string DbName = "Database.db";
        /// <summary>
        /// Current User's authentication token.
        /// </summary>
        public static string AuthToken;
        /// <summary>
        /// Event which is currently picked. Null if none is chosen.
        /// </summary>
        public static Event Session; 
        /// <summary>
        /// Instance of ListController for building necessary lists.
        /// </summary>
        private static ListController _lc;
        
        /// <summary>
        /// Stores all questions of current user.
        /// </summary>
        public static List<Question> AllQuestionList;
        /// <summary>
        /// Maps tags and questions of current user.
        /// </summary>
        public static System.Collections.Hashtable Tagtable;

        /// <summary>
        ///  Initializes Questionlist and Tagtable by using ListController.
        /// </summary>
        public static void ListBuilding()
        {
         _lc = new ListController();
      
         AllQuestionList = _lc.AllQuestionList;
         Tagtable = _lc.TagTable;
        }
        
	}
}

