using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SleepControl.Scripts.Alarm
{
    public class Alarm_AlarmsList : Fragment
    {
        Alarm_NewAlarm NewAlarm;
        Activity context;
        ListView ListViewContainer = null;

        public Alarm_AlarmsList(Alarm_NewAlarm NewAlarm, Activity context)
        {
            this.NewAlarm = NewAlarm;
            this.context = context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Alarm_AlarmsList,null,false);

            ListViewContainer = v.FindViewById<ListView>(Resource.Id.listView1);

            return v;
        }

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);

            if (ListViewContainer != null)
                ListViewContainer.Adapter = new Alarm_AlarmsList_ArrayAdapter(Activity, AlarmObj.GetAllAlarms(Activity), NewAlarm, this);
        }

        public override void OnResume()
        {
            ListViewContainer.Adapter = new Alarm_AlarmsList_ArrayAdapter(Activity, AlarmObj.GetAllAlarms(Activity), NewAlarm, this);

            base.OnResume();
        }

        /* void b_Click(object sender, EventArgs e)
         {
             Intent i = new Intent(context,typeof(Alarm_Triggered));
             i.SetPackage(context.PackageName);

             PendingIntent pIntent = PendingIntent.GetActivity(context.ApplicationContext,1,i,PendingIntentFlags.OneShot);
             AlarmManager aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);
             aManager.Set(AlarmType.ElapsedRealtime,SystemClock.ElapsedRealtime()+10000,pIntent);

             FragmentTransaction transact = FragmentManager.BeginTransaction();
             transact.Hide(this);
             transact.Show(NewAlarm);
             transact.Commit();
         }*/
    }
}