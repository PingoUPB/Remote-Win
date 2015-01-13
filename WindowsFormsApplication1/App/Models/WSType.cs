namespace WinRemote.App.Models
{
    /// <summary>
    /// Provides the Type for the WSHelper. Requests ID and Name from its implementors.
    /// </summary>
	public interface IWsType 
	{
		// [DataMember(Name="_id")]
		/// <summary>
		/// 
		/// </summary>
		string Id { get; set; }
		
		// [DataMember(Name="name")]
		/// <summary>
		/// 
		/// </summary>
		string Name { get; set; }
	}
}

