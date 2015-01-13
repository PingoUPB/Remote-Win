using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WinRemote.App.Models
{   
    /// <summary>
    /// A Duration contains the time left in seconds.
    /// </summary>
    internal class Duration : IWsType
    {
        #region fields
        /// <summary>
        ///No use. Requested by WSType
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// No use. Requested by WSType
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The duration in seconds.
        /// </summary>
        public int Sec { get; set; }
        #endregion

        /// <summary>
        /// Sets the seconds.
        /// </summary>
        /// <param name="sec"></param>
        public Duration(int sec)
        {
            Sec = sec;
        }

        /// <summary>
        /// Provides the duration in Format of x min:yz or ys
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ConvertSeconds(Sec);
        }

        /// <summary>
        /// Transforms a JArray into a list of durations
        /// </summary>
        /// <param name="ja">the JArray to be transformed</param>
        /// <returns></returns>
        public static List<Duration> ToList(JArray ja)
        {
            var erg = new List<Duration>();
            foreach (var jToken in ja)
            {
                var jv = (JValue) jToken;
                erg.Add(new Duration((int)jv));
            }
            return erg;
        }

        /// <summary>
        /// Converts given seconds into 1:xx min or xx s format
        /// </summary>
        /// <param name="sec">The seconds to be converted.</param>
        /// <returns>sec in min:s Format</returns>
        public static string ConvertSeconds(int sec)
        {
            string m;
            string s;
            if (sec >= 60)
            {
                m = (sec / 60).ToString();
                if (sec % 60 < 10)
                    s = "0" + (sec % 60) + " min";
                else
                    s = (sec % 60) + " min";
            }
            else { return sec + "s"; }
            return m + ":" + s;
        }
    }
}