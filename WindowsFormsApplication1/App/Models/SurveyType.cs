using System.Collections.Generic;
using System.Globalization;

namespace WinRemote.App.Models
{   
    /// <summary>
    /// Describes a possible survey type like Single Choice or Multiple Choice.
    /// </summary>
    internal class SurveyType : IWsType
    {
        
        /// <summary>
        /// Not in use. Requested by WSType.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Not in use. Requested by WSType.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type's name used in the German translation.
        /// </summary>
        public string NameDe { get; set; }
        /// <summary>
        /// The type's name used in the English translation.
        /// </summary>
        public string NameEn { get; set; }
        /// <summary>
        /// The type's name used by the PINGO server.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The possible options for this type.
        /// </summary>
        public List<TypeOption> Options { get; set; }
        
        /// <summary>
        /// Provides the type's name based on the users language settings.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
                return NameDe;
            return NameEn;
        }
    }
}