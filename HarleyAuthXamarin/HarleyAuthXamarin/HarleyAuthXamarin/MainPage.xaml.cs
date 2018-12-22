using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

		/// <summary>
		/// POST the GlobalSettings.FacebookToken to our facebook login endpoint on the api service.
		/// Save the JWT token we get back and popup the result.
		/// </summary>
		public async void OnServiceAuthenticateButtonClicked(object sender, EventArgs args)
		{
			var facebookToken = GlobalSettings.FacebookToken;

			if (facebookToken == null)
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
			else
			{
				await (Application.Current as App).MainPage.DisplayAlert("AuthResponse to service", responseMessage.StatusCode.ToString(), "Ok");
			}
		}

		/// <summary>
		/// GET request to an authorized endpoint using the GlobalSettings.AuthToken as the bearer token.
		/// Popup the response.
		/// </summary>
		public async void OnGetProtectedResourceButtonClicked(object sender, EventArgs args)
		{
			var authToken = GlobalSettings.AuthToken;
			if (authToken == null)
			{
				return;
			}

			var client = new HttpClient();
			client.BaseAddress = new Uri($"{GlobalSettings.BackendUrl}/");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

			var responseMessage = await client.GetAsync($"api/values/auth");
			var responseJson = await responseMessage.Content.ReadAsStringAsync();

			await (Application.Current as App).MainPage.DisplayAlert("Protected Resource response", responseJson, "Ok");
		}
	}
}
