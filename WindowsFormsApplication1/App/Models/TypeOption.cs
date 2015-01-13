using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace WinRemote.App.Models
{   
    /// <summary>
    /// Represents a one possible option of a survey type
    /// </summary>
    internal class TypeOption
    {
        #region fields
        /// <summary>
        /// The option's name used by the PINGO Server
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The option's name used by the German translation in the webapp
        /// </summary>
        public string NameDe { get; set; }
        /// <summary>
        /// The option's name used by the English translation in the webapp
        /// </summary>
        public string NameEn { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Transforms JArrays into a list of TypeOptions. Is based on the assumption that the first name in the first parameter
        /// belongs to the same TypeOption as the first parameters in the second and third parameters and so on...
        /// </summary>
        /// <param name="json">JArray containing the names</param>
        /// <param name="jsonDe">JArray containing the</param>
        /// <param name="jsonEn"></param>
        /// <returns></returns>
        public static List<TypeOption> ToList(JArray json, JArray jsonDe, JArray jsonEn)
        {
            var result = new List<TypeOption>();
            if (jsonDe[0].ToString().Equals(""))
                /*if the first German name is set to "" then the names in the first parameter
                 * are used for the German and English names as well, is used when e.g. every option of a type is just a number like SC or MC */
                foreach (var jToken in json)
                {
                    var jv = (JValue) jToken;
                    result.Add(new TypeOption(jv, jv, jv));
                }
            else
                for (int i = 0; i < json.Count; i++)
                {
                    result.Add(new TypeOption((JValue)json[i], (JValue)jsonDe[i], (JValue)jsonEn[i]));
                }
            return result;
        }

        /// <summary>
        /// Sets the TypeOption's names based on the provided JValues
        /// </summary>
        /// <param name="jv">JValue containing the name</param>
        /// <param name="jvDe">JValue containing the German name</param>
        /// <param name="jvEn">JValue containing the English name</param>
        public TypeOption(JValue jv, JValue jvDe, JValue jvEn)
        {
            Name = jv.ToString();
            NameDe = jvDe.ToString();
            NameEn = jvEn.ToString();
        }

        /// <summary>
        /// Provides the Option's name depending on the users language settings. Default is English.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
                return NameDe;
            return NameEn;
        }
        #endregion
    }
}