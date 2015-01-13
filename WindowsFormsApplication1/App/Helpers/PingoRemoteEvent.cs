using System;
using Newtonsoft.Json;
using SocketIOClient.Messages;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Describes the data send within a JuggernautMessage object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PingoRemoteEvent
    {
        #region fields
        /// <summary>
        /// The data's type, like countdown or voter_count
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// The data's payload describes the milliseconds of the countdown or the current number of voters.
        /// </summary>
        [JsonProperty("payload")]
        public string Payload { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("iteration")]
        public int Iteration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Transforms Event to a JSON String.
        /// </summary>
        /// <returns>The Event as JSON.</returns>
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// Transforms a JSON String to a PINGORemoteEvent. </summary>
        /// <param name="jsonString">The JSON to be transformed.</param>
        /// <returns>The resulting PingoRemoteEvent</returns>
        public static PingoRemoteEvent Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<PingoRemoteEvent>(jsonString);
        }

        /// <summary>
        /// Manually parses the recieved data.
        /// </summary>
        /// <param name="data">The data to be parsed in order to create the PingoRemoteEvent.</param>
        /// <returns>The resulting PingoRemoteEvent</returns>
        public static PingoRemoteEvent FromMsg(IMessage data)
        {
            Console.WriteLine("Message to be parsed:"+data.RawMessage);
            var pre = new PingoRemoteEvent();
            string msg = data.RawMessage; //split the raw message at the necessary points. just output the raw message once to understand this method
            string[] param = msg.Split(',');
            foreach (string s in param)
            {
                if (s.Contains("type"))
                {
                    string[] uparam = s.Split('"');
                    pre.Type = uparam[uparam.Length - 2];
                }
                else if (s.Contains("payload"))
                {
                    string[] uparam = s.Split(':');
                    pre.Payload = uparam[uparam.Length - 1];
                }
                else if (s.Contains("iteration"))
                {
                    string[] uparam = s.Split(':');
                    string[] uparam2 = uparam[uparam.Length - 1].Split('}');
                    pre.Iteration = Convert.ToInt32(uparam2[0]);
                }
            }

            return pre;
        }
        #endregion
    }
}