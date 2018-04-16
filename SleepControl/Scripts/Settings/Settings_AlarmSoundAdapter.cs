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
using Android.Media;

namespace SleepControl.Scripts.Settings
{
    class Settings_AlarmSoundAdapter:ArrayAdapter<SoundFile>
    {
        Activity context;
        SoundFile[] SoundFiles;
        Dialog Parent;

        public Settings_AlarmSoundAdapter(Activity context, SoundFile[] SoundFiles,Dialog Parent)
            : base(context, Resource.Layout.Settings_SoundLineAdapter, SoundFiles)
        {
            this.context = context;
            this.SoundFiles = SoundFiles;
            this.Parent = Parent;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = context.LayoutInflater.Inflate(Resource.Layout.Settings_SoundLineAdapter,null,false);

            TextView AudioTitle = v.FindViewById<TextView>(Resource.Id.textView1);

            AudioTitle.Text = SoundFiles[position].Title;
            AudioTitle.Click += (object sender, EventArgs e) => {
                Toast.MakeText(context,AudioTitle.Text,ToastLength.Short).Show();

                new SaveUsingSharedPreferences(context).Save(SaveUsingSharedPreferences.Tags.Settings.AlarmRingTone,SoundFiles[position].Path);

                Parent.Cancel();
            };

            return v;
        }
    }

    public struct SoundFile
    {
        public string Title;
        public string Path;
    }
}