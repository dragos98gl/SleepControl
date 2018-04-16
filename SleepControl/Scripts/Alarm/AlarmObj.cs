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
using Android.Support.V4.App;
using Android.Media;
using Android.Graphics.Drawables;
using SleepControl.Scripts.Alarm._safety;

namespace SleepControl.Scripts.Alarm
{
    class AlarmObj
    {
        public Context context;
        public string ID;
        public string Hour;
        public string Min;
        public string Name;
        public string Remind;
        public string FireAlert;
        public string EarthquakeAlert;
        public bool IsSynced = false;
        List<float> PulseList = new List<float>();

        public AlarmObj()
        { 
        }

        public AlarmObj(string Hour,string Min,string Name,string Remind, string FireAlert, string EarthquakeAlert, Context context)
        {
            this.context = context;
            ID = new Random().Next(1,999).ToString();
            this.Hour = Hour;
            this.Min = Min;
            this.Name = Name;
            this.Remind = Remind;
            this.FireAlert = FireAlert;
            this.EarthquakeAlert = EarthquakeAlert;

            Save();
            StartAlarm();
        }

        public AlarmObj(string Hour,string Min,string Name,string Remind,string ID,string FireAlert,string EarthquakeAlert ,Context context)
        {
            this.context = context;
            this.ID = ID;
            this.Hour = Hour;
            this.Min = Min;
            this.Name = Name;
            this.Remind = Remind;
            this.FireAlert = FireAlert;
            this.EarthquakeAlert = EarthquakeAlert;
        }

        void Save()
        {
            new SaveUsingSharedPreferences(context).SaveArray(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm, new string[] {
                            ID,
                            Hour,
                            Min,
                            Name,
                            Remind,
                            FireAlert,
                            EarthquakeAlert
                        });
        }

        public void Edit(string[] s)
        {
            new SaveUsingSharedPreferences(context).EditArray(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm,int.Parse(s[0]), s);

            AlarmObj THIS = AlarmObj.GetAlarmAt(int.Parse(s[0]),context);
            
            this.context = THIS.context;
            this.ID = THIS.ID;
            this.Hour = THIS.Hour;
            this.Min = THIS.Min;
            this.Name = THIS.Name;
            this.Remind = THIS.Remind;
            this.EarthquakeAlert = THIS.EarthquakeAlert;
            this.FireAlert = THIS.FireAlert;

            StartAlarm();
        }

        public void SyncAlarm(AlarmBluetoothHandle bta)
        {
            IsSynced = true;
            CancelAlarm();

            string H,M,D;

            if (int.Parse(Hour) < 10)
                H = "0" + Hour;
            else
                H = Hour;

            if (int.Parse(Min) < 10)
                M = "0" + Min;
            else
                M = Min;

            if (DateTime.Now.Day < 10)
                D = "0" + DateTime.Now.Day;
            else
                D = DateTime.Now.Day.ToString();

            DateTime AlarmTime = DateTime.ParseExact("0"+DateTime.Now.Month + "-" + D + " " + H + ":" + M, "MM-dd HH:mm", null);
            AlarmTime.AddMinutes(-45);

            if (DateTime.Compare(DateTime.Now, AlarmTime) > -1)
                AlarmTime.AddDays(1);

            foreach (float val in bta.ClientHandle())
            {
                float i = (int)(val/10);
                int IsFire = (int)val%10;

                if (FireAlert == "True"&&IsFire==2)
                {
                    Intent ii = new Intent(context, typeof(Alarm_Triggered));
                    ii.PutExtra("ID", ID);
                    ii.SetPackage(context.PackageName);

                    AlarmManager aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);

                    PendingIntent pIntent = PendingIntent.GetActivity(context.ApplicationContext, int.Parse(ID), ii, PendingIntentFlags.OneShot);
                    aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);
                    aManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1, pIntent);
                }

                PulseList.Add(i);

                if (UserStartSleep())
                    if (UserEnterInREM())
                    {
                        if (DateTime.Compare(DateTime.Now, AlarmTime) != -1)
                        {  
                            Intent ii = new Intent(context, typeof(Alarm_Triggered));
                            ii.PutExtra("ID", ID);
                            ii.SetPackage(context.PackageName);

                            AlarmManager aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);

                            PendingIntent pIntent = PendingIntent.GetActivity(context.ApplicationContext, int.Parse(ID), ii, PendingIntentFlags.OneShot);
                            aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);
                            aManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1, pIntent);
                        }
                    }
            }
        }

        int PulseCounter_01 = 0;
        bool UserStartSleep()
        {
            int PulseListLength = PulseList.Count - 1;

            if (PulseListLength > 1)
            {
                float PulseOffset = PulseList[PulseListLength] - PulseList[PulseListLength - 1];

                if (PulseCounter_01 == 10)
                    return true;

                if (Math.Abs(PulseOffset) == 1)
                    PulseCounter_01++;
                else
                    PulseCounter_01 = 0;
            }

            return false;
        }

        bool UserEnterInREM()
        {
            int PulseListLength = PulseList.Count - 1;
            int i = PulseListLength - 2;

            if (PulseListLength > 4)
                if (PulseList[i - 1] < PulseList[i] && PulseList[i - 2] < PulseList[i] && PulseList[i + 1] >= PulseList[i] && PulseList[i + 2] >= PulseList[i])
                    return true;
                else
                    return false;

            return false;
        }

        void StartAlarm()
        {
            int ActualHour = System.DateTime.Now.Hour;
            int ActualMin = System.DateTime.Now.Minute;

            int RamainHours;
            int RamainMins;

            if (ActualHour < int.Parse(Hour))
                RamainHours = int.Parse(Hour) - ActualHour;
            else
                RamainHours = ActualHour - int.Parse(Hour);

            if (ActualMin > int.Parse(Min))
            {
                RamainMins = 60 - ActualMin + int.Parse(Min);
                RamainHours--;
            }
            else
                RamainMins = int.Parse(Min) - ActualMin;

            Intent i = new Intent(context, typeof(Alarm_Triggered));
            i.PutExtra("ID",ID);
            i.SetPackage(context.PackageName);

            PendingIntent pIntent = PendingIntent.GetActivity(context.ApplicationContext, int.Parse(ID), i, PendingIntentFlags.OneShot);
            AlarmManager aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);
            aManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + RamainMins * 1000 * 60 + RamainHours * 1000 * 60 * 60, pIntent);

            if (Remind != "")
            {
                try
                {
                    Notification.Builder builder = new Notification.Builder(context)
                        .SetAutoCancel(true)
                        .SetStyle(new Notification.BigTextStyle()
                            .BigText("Somnul reprezinta o activitate importanta si nu vei fii productiv daca nu esti odihnit!")
                            .SetBigContentTitle("Sleep Control - " + Name + " " + Remind.Split(' ')[1] + " minute pana la somn"))
                        .SetPriority((int)NotificationPriority.High)
                        .SetSmallIcon(Resource.Drawable.IconNotification)
                        .SetLargeIcon(((BitmapDrawable)context.Resources.GetDrawable(Resource.Drawable.IconNotification)).Bitmap)
                        .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

                    Notification n = builder.Build();

                    Intent nIntent = new Intent(context, typeof(AlarmObj_Notification_BroadcastReceiver));
                    nIntent.PutExtra("NOTIFICAITON", n);
                    PendingIntent nPending = PendingIntent.GetBroadcast(context, int.Parse(ID) + 1, nIntent, PendingIntentFlags.OneShot);

                    int RemainTime = RamainHours * 60 + RamainMins - Remind[0] * 60 - Remind[1];

                    aManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
                    aManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + RemainTime * 1000 * 60, nPending);
                }
                catch (Exception e)
                {
                    Toast.MakeText(context, e.Message, ToastLength.Long).Show();
                }
            }

            Alarm_EarthquakeAlert ee;

            if (EarthquakeAlert== "True")
            {
                ee = new Alarm_EarthquakeAlert(context);
                ee.EarthquakeAlert += (object sender, EarthquakeAlartEventArgs e) =>
                {
                    CancelAlarm();

                    i = new Intent(context, typeof(Alarm_Triggered));
                    i.SetPackage(context.PackageName);

                    pIntent = PendingIntent.GetActivity(context.ApplicationContext, int.Parse(ID), i, PendingIntentFlags.OneShot);
                    aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);
                    aManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1, pIntent);

                    Toast.MakeText(context, "EARTHQUAKE", ToastLength.Long).Show();
                };
            }
            else {
                ee = null;
            }
        }

        public void CancelAlarm()
        {
            Intent i = new Intent(context, typeof(Alarm_Triggered));
            i.PutExtra("ID", ID);
            i.SetPackage(context.PackageName);

            PendingIntent pIntent = PendingIntent.GetActivity(context.ApplicationContext, int.Parse(ID), i, PendingIntentFlags.OneShot);
            AlarmManager aManager = (AlarmManager)context.ApplicationContext.GetSystemService(SleepControl.MainActivity.AlarmService);

            aManager.Cancel(pIntent);
        }

        public static List<AlarmObj> GetAllAlarms(Context context)
        {
            List<string> AList = new SaveUsingSharedPreferences(context).LoadArray(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm).ToList();

            List<AlarmObj> AlarmsList = new List<AlarmObj>();

            for (int i = 0; i < AList.Count / 7; i++)
            {
                AlarmObj ao = new AlarmObj();
                ao.ID = (AList[i * 7]);
                ao.Hour = (AList[i * 7+1]);
                ao.Min = (AList[i * 7+2]);
                ao.Name = (AList[i * 7+3]);
                ao.Remind = (AList[i * 7+4]);
                ao.FireAlert = (AList[i*7+5]);
                ao.EarthquakeAlert = (AList[i*7+6]);
                ao.context = context;

                AlarmsList.Add(ao);
            }

            return AlarmsList;
        }

        public static AlarmObj GetAlarmAt(int index,Context context)
        {
            return GetAllAlarms(context)[index];
        }

        public static void Delete(string ID,Context context)
        {
            var AllAlarms = GetAllAlarms(context);

            for (int i=0;i<AllAlarms.Count;i++)
            {
                if (AllAlarms[i].ID == ID)
                {
                    new SaveUsingSharedPreferences(context).DeleteAlarm(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm, i);
                }
            }
        }

        public static void Delete(int index,Context context)
        {
            AlarmObj.GetAlarmAt(index, context).CancelAlarm();
            new SaveUsingSharedPreferences(context).DeleteAlarm(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm, index);
        }
    }
}