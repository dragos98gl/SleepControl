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

namespace SleepControl.Scripts.Alarm
{
    [Activity(ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]
    class Alarm:Activity
    {
        FrameLayout Container;
        Alarm_AlarmsList AlarmsList;
        Alarm_NewAlarm NewAlarm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen,WindowManagerFlags.Fullscreen);
         
            Bundle Data =  Intent.Extras;
            string LaunchType = (Data!=null) ? Data.GetString("LaunchType") : string.Empty;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Alarm);

            Container = FindViewById<FrameLayout>(Resource.Id.frameLayout1);

            NewAlarm = new Alarm_NewAlarm();
            AlarmsList = new Alarm_AlarmsList(NewAlarm,this);
            
            NewAlarm.AlarmList = AlarmsList;

            FragmentTransaction transact = FragmentManager.BeginTransaction();
            transact.Add(Container.Id,NewAlarm,"NewAlarm");
            transact.Add(Container.Id,AlarmsList,"AlarmsList");

            if (LaunchType.Equals("NewAlarm"))
            {
                transact.Hide(AlarmsList);
                NewAlarm.OnFragmentShown();
            }
            else
                transact.Hide(NewAlarm);
            
            transact.Commit();
        }

        void ShowFragment(Fragment frag)
        {
            FragmentTransaction transact = FragmentManager.BeginTransaction();

            transact.Hide(AlarmsList);
            transact.Hide(NewAlarm);

            transact.Show(frag);

            transact.Commit();

        }

        public override void OnBackPressed()
        {
            if (!NewAlarm.IsHidden)
                ShowFragment(AlarmsList);
            else
                base.OnBackPressed();
        }
    }
}