using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;

namespace SleepControl.Scripts.Settings
{
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        ListView SettingsList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen,WindowManagerFlags.Fullscreen);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);

            SettingsList = FindViewById<ListView>(Resource.Id.listView1);
            SettingsList.Adapter = new Settings_Adapter(this,
                new string[] {"Sound","Volume"},
                new Drawable[] {
                    Resources.GetDrawable(Resource.Drawable.Melody),
                    Resources.GetDrawable(Resource.Drawable.Volume)});
        }
    }
}