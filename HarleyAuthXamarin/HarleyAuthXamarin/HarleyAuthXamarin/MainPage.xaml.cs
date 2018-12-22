using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HarleyAuthXamarin
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		public async void OnServiceAuthenticateButtonClicked(object sender, EventArgs argss)
		{
			var facebookToken = GlobalSettings.FacebookToken;

			if(facebookToken == null)
			{
				return;
			}

			var client = new HttpClient();
			client.BaseAddress = new Uri($"{GlobalSettings.BackendUrl}/");

			var content = new StringContent("{\"AccessToken\":\"" + facebookToken + "\"}", Encoding.UTF8, "application/json");

			var responseMessage = await client.PostAsync($"/api/ExternalAuth/facebook", content);
			var responseJson = await responseMessage.Content.ReadAsStringAsync();
			if (responseMessage.IsSuccessStatusCode)
			{
				var responseObject = JsonConvert.DeserializeObject<AuthResponse>(responseJson);

				GlobalSettings.AuthToken = responseObject.auth_token;
				await (Application.Current as App).MainPage.DisplayAlert("AuthResponse to service", responseObject.auth_token, "Ok");
			}
		}
	}
}
