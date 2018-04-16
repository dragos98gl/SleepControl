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
using Android.Provider;
using Android.Database;

namespace SleepControl.Scripts.Settings
{
    class Settings_Adapter:ArrayAdapter<string>
    {
        Activity context;
        string[] Campuri;
        Drawable[] Imagini;

        public Settings_Adapter(Activity context,string[] Campuri,Drawable[] Imagini):base(context,Resource.Layout.Settings_Adapter,Campuri)
        {
            this.context = context;
            this.Campuri = Campuri;
            this.Imagini = Imagini;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = context.LayoutInflater;
            View v = inflater.Inflate(Resource.Layout.Settings_Adapter,null,true);

            ImageView Icon = v.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView Camp = v.FindViewById<TextView>(Resource.Id.textView1);
            RelativeLayout Container = v.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);

            Icon.Background=Imagini[position];
            Camp.Text = Campuri[position];

            Container.Click += (object sender, EventArgs e) => {
                switch (position)
                {
                    case 0:
                        {
                            Dialog diag = new Dialog(context);
                            diag.Window.RequestFeature(WindowFeatures.NoTitle);

                            View CostumView = inflater.Inflate(Resource.Layout.Settings_SoundAdapter, null);

                            ListView MelodiesList = CostumView.FindViewById<ListView>(Resource.Id.listView1);

                            ContentResolver cr = context.ContentResolver;

                            Android.Net.Uri uri = MediaStore.Audio.Media.ExternalContentUri;
                            String seletion = MediaStore.Audio.Media.InterfaceConsts.IsMusic + "!= 0";
                            String sortOrder = MediaStore.Audio.Media.InterfaceConsts.Title + " ASC";

                            ICursor cur = cr.Query(uri,null,seletion,null,sortOrder);
                            int count = 0;

                            List<SoundFile> AudioFiles = new List<SoundFile>();

                            if (cur != null)
                            {
                                count = cur.Count;

                                if (count > 0)
                                {
                                    while (cur.MoveToNext())
                                    {
                                        string path = cur.GetString(cur.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data));
                                        string title = cur.GetString(cur.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Title));

                                        SoundFile sf = new SoundFile();
                                        sf.Path = path;
                                        sf.Title = title;

                                        AudioFiles.Add(sf);
                                    }
                                }
                            }

                            MelodiesList.Adapter = new Settings_AlarmSoundAdapter(context,AudioFiles.ToArray(),diag);

                            diag.SetContentView(CostumView);
                            diag.Show();
                        } break;
                    case 1:
                        {
                            Dialog diag = new Dialog(context);
                            diag.Window.RequestFeature(WindowFeatures.NoTitle);

                            View CostumView = inflater.Inflate(Resource.Layout.Settings_VolumeAdapter, null);

                            Button Ok = CostumView.FindViewById<Button>(Resource.Id.button1);
                            Ok.Click += (object sender1, EventArgs e1) => {
                                diag.Cancel();
                            };

                            diag.SetContentView(CostumView);
                            diag.Show();
                        } break;
                }
            };

            return v;
        }
    }
}