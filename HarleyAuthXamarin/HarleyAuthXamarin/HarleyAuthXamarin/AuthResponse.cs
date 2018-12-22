namespace HarleyAuthXamarin
{
	public class AuthResponse
	{
		public string id { get; set; }
		public string auth_token { get; set; }
		public int expires_in { get; set; }
	}
}
