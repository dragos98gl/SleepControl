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
using System.Drawing;
using Android.Graphics;
using Android.Media;

namespace SleepControl.Scripts.Alarm
{
    [Activity(Label = "Alarm_Triggered",Theme="@android:style/Theme.Translucent.NoTitleBar",MainLauncher=false,NoHistory=true)]
    public class Alarm_Triggered : Activity
    {
        ImageView AlarmIcon;
        RelativeLayout AlarmOptions;
        TextView Time;
        MediaPlayer mp;
        string ID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Argb(200, 0, 0, 0)));
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            SetContentView(Resource.Layout.Alarm_AlarmPopUpDialog);

            Bundle bundle = Intent.Extras;
            ID = bundle.GetString("ID");

            AlarmIcon = FindViewById<ImageView>(Resource.Id.imageView1);
            AlarmOptions = FindViewById<RelativeLayout>(Resource.Id.relativeLayout2);
            Time = FindViewById<TextView>(Resource.Id.textView3);

            Time.SetTypeface(Typeface.CreateFromAsset(Resources.Assets, "Font/Font_Normal.otf"),TypefaceStyle.Normal);
            Time.Text = DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString();

            AlarmIcon.Touch += AlarmIcon_Touch;

            mp = new MediaPlayer();
            mp.Reset();
            mp.SetDataSource(new SaveUsingSharedPreferences(this).LoadString(SaveUsingSharedPreferences.Tags.Settings.AlarmRingTone));
            mp.Prepare();
            mp.Start();  
        }

        void AlarmIcon_Touch(object sender, View.TouchEventArgs e)
        {
            AlarmIcon.SetX(e.Event.RawX - AlarmIcon.Width);
            AlarmIcon.SetY(e.Event.RawY - AlarmOptions.GetY() - AlarmIcon.Height);

            if (e.Event.Action.Equals(MotionEventActions.Up))
            {
                float Up_LocationY = e.Event.RawY - AlarmOptions.GetY() - AlarmIcon.Height;

                if (Up_LocationY < 50f)
                {
                    mp.Stop();
                    AlarmObj.Delete(ID,this);
                    Finish();
                }
                else if (Up_LocationY > 380f)
                {
                    mp.Stop();
                    Finish();
                }
                else
                {
                    AlarmIcon.SetX(235f);
                    AlarmIcon.SetY(235f);
                }
            }

            Console.WriteLine(e.Event.RawY - AlarmOptions.GetY() - AlarmIcon.Height);
        }
    }
}