using System;
using NDatabase;
using WinRemote.App.Models;
using NDatabase.Api;

namespace WinRemote.App.Controllers
{
    /// <summary>
    ///  Organizes NDatabase. Saves and retrieves user's authentication token and URLs to sockets and website.
    /// </summary>
    class DbController
    {
        #region StoreData
        /// <summary>
        /// Stores auth_token in DB.
        /// </summary>
        /// <param name="authToken">Authentication Token to be stored.</param>
        public static void StoreAuthToken(String authToken)
        {
            using (var odb = OdbFactory.Open(Settings.DbName)) //open DB
            {
                //Delete old database entries
                foreach (User u in odb.QueryAndExecute<User>())
                    odb.Delete(u);
                odb.Store(new User(authToken));
            }
        }

        /// <summary>
        /// Stores the given URLs in the data base as an URLSettings object.
        /// </summary>
        /// <param name="baseUrl">URL to PINGO website</param>
        /// <param name="baseSocketUrl">URL to socket website</param>
        public static void StoreUrLs(String baseUrl, String baseSocketUrl)
        {
            using (var odb = OdbFactory.Open(Settings.DbName)) //open DB
            {
                //Delete old database entries
                foreach (UrlSettings u in odb.QueryAndExecute<UrlSettings>())
                    odb.Delete(u);
                odb.Store(new UrlSettings(baseUrl, baseSocketUrl));
            }
        }

        /// <summary>
        /// Stores the given version number in the database as an VersionInfo object.
        /// </summary>
        /// <param name="versionNumber">Database relevant version number</param>
        public static void StoreVersionInfo(double versionNumber)
        {
            using (var odb = OdbFactory.Open(Settings.DbName)) //open DB
            {
                //Delete old database entries
                foreach (var u in odb.QueryAndExecute<VersionInfo>())
                    odb.Delete(u);
                odb.Store(new VersionInfo(versionNumber));
            }
        }

        #endregion

        #region QueriesAndUpdates

        /// <summary>
        /// Returns the stored URL to the PINGO website.
        /// </summary>
        /// <returns></returns>
        public static string RetrieveBaseUrl()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                var u = odb.QueryAndExecute<UrlSettings>().GetFirst(); //Query first URLSettings Obj in DB
                if (u != null) return u.BaseUrl;
                return "";
            }

        }

        /// <summary>
        /// Returns the stored URL to the socket website.
        /// </summary>
        /// <returns></returns>
        public static string RetrieveBaseSocketUrl()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                var u = odb.QueryAndExecute<UrlSettings>().GetFirst(); //Query first User Obj in DB
                if (u != null) return u.BaseSocketUrl;
                return "";
            }

        }

     /// <summary>
     ///  Gets auth_token from DB.
     /// </summary>
     /// <returns>The current authentication token. null if no User is saved in DB.</returns>
        public static string RetrieveAuthToken()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                var u= odb.QueryAndExecute<User>().GetFirst(); //Query first User Obj in DB
                return u != null ? u.AuthToken : null;
            }
                
        }

        public static double RetrieveVersionInfo()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                var v = odb.QueryAndExecute<VersionInfo>().GetFirst(); //Query first User Obj in DB
                return v != null ? v.VersionNumber : 0.0;
            }

        }

        /// <summary>
        /// Deletes all saved Users from DB. Called on log out.
        /// </summary>
        public static void DeleteAllUsers()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                foreach (User u in odb.QueryAndExecute<User>())
                    odb.Delete(u);
                
            }
        }

        /// <summary>
        /// Deletes all saved Urls from DB.
        /// </summary>
        public static void DeleteAllUrls()
        {
            using (var odb = OdbFactory.Open(Settings.DbName))
            {
                foreach (User u in odb.QueryAndExecute<User>())
                    odb.Delete(u);

            }
        }

        public static void CheckVersion()
        {
           // using (var odb = OdbFactory.Open(Settings.DbName))
            //{
            var v = RetrieveVersionInfo();
                if (v == 0.0) StoreVersionInfo(Settings.DatabaseRelevantVersion);
                else if(Settings.DatabaseRelevantVersion>v)
                {
                    
                    StoreVersionInfo(Settings.DatabaseRelevantVersion);
                    DeleteAllUsers();
                    DeleteAllUrls();
                }
            //}
        }
        #endregion
    }
        
}
