using System;
using System.Collections.Generic;
using System.Text;

namespace HarleyAuthXamarin
{
    public static class GlobalSettings
    {
		public static string BackendUrl = @"https://harleyauth.azurewebsites.net";
		public static string FacebookToken { get; set; }
		public static string AuthToken { get; set; }
	}
}
