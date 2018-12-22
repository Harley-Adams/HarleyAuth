using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Facebook;
using Android.Content;

namespace HarleyAuthXamarin.Droid
{
	[Activity(Label = "HarleyAuthXamarin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static ICallbackManager CallbackManager;

		protected override void OnActivityResult(
			int requestCode,
			Result resultCode,
			Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			CallbackManager.OnActivityResult(requestCode, Convert.ToInt32(resultCode), data);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			// Create callback manager using CallbackManagerFactory
			CallbackManager = CallbackManagerFactory.Create();

			base.OnCreate(savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
		}
	}
}