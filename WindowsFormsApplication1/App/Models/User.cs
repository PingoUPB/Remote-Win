using WinRemote.App.Controllers;
using WinRemote.App.Helpers;

namespace WinRemote.App.Models
{
    /// <summary>
    /// User class, similiar to user model in PINGO Webapp
    /// </summary>
    public class User : IWsType
    {
        #region fields
        /// <summary>
        /// Not used. Requested by WSType
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Not used. Requested by WSType
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The User's Authentication Token. Enables authentication without giving e-mail and password to the server.
        /// </summary>
        public string AuthToken { get; set; }
        /// <summary>
        /// true if the user's auth token is valid.
        /// </summary>
        public string Valid { get; set; }

        #endregion
        
        /// <summary>
        /// Sets the authentication token
        /// </summary>
        /// <param name="authToken">the user's authentication token.</param>
        public User(string authToken)
        {
            AuthToken = authToken;
        }
        /// <summary>
        /// Default.
        /// </summary>
        public User() { }

        /// <summary>
        /// Requests the user's authentication token from server.
        /// </summary>
        /// <param name="email">the user's e-mail adress</param>
        /// <param name="password">the user's password</param>
        /// <returns>the user's auth token</returns>
        public static string Authenticate(string email, string password)
        {
            var wsh = new WsHelper<User>();
            //Add parameters
            var paramlist = new System.Collections.Hashtable {{"email", email}, {"password", password}};
            User u = wsh.ConvertJsontoObj(wsh.Post("api/get_auth_token", paramlist), e => new User { Id = "", Name = "", AuthToken = (string)e["authentication_token"] });

            return u.AuthToken;
        }
        /// <summary>
        /// Determines if the given token is a valid one, i.e. you can connect with it to the server.
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public static bool valid_token(string authToken)
        {
            var wsh = new WsHelper<User>();
            var paramlist = new System.Collections.Hashtable {{"auth_token", authToken}};

            User u = wsh.ConvertJsontoObj(wsh.Post("api/check_auth_token", paramlist), e => new User { Id = "", Name = "", AuthToken = (string)e["authentication_token"], Valid = (string)e["valid"] });

            if (u.Valid.Equals("True")) return true;
            return false;
        }

        /// <summary>
        /// Logs the User out. Deletes the token from Database and Settings.
        /// </summary>
        public static void Logout()
        {
            Settings.AuthToken = "";
            DbController.DeleteAllUsers();
        }
    }
}