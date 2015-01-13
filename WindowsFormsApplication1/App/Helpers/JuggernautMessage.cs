using Newtonsoft.Json;

namespace WinRemote.App.Helpers
{
    /// <summary>
    /// Describes a Message recieved from Websocket.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class JuggernautMessage
    {
        #region fields

        /// <summary>
        /// The channel the message is from.
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }
        /// <summary>
        /// The type of the message.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// The message's content.
        /// </summary>
        [JsonProperty("data")]
        public PingoRemoteEvent Data { get; set; }

        #endregion fields

        /// <summary>
        /// Converts into JSON.
        /// </summary>
        /// <returns>A JSON String</returns>
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Converts from JSON into String.
        /// </summary>
        /// <param name="jsonString">the JSON to be converted</param>
        /// <returns>the representing string</returns>
        public static JuggernautMessage Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<JuggernautMessage>(jsonString);
        }

        
        /// <summary>
        /// only serializes data and type if they are present:
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeData()
        {
            return (Data != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeType()
        {
            return (Type != null);
        }
    }
}