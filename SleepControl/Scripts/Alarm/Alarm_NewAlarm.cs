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
    public class Alarm_NewAlarm : Fragment
    {
        public delegate void FragmentShownEventArgsHandler(object sender, EventArgs e);
        public event FragmentShownEventArgsHandler FragmentShown;

        public virtual void OnFragmentShown()
        {
            if (FragmentShown != null)
                FragmentShown(this, EventArgs.Empty);
        }


        public Fragment AlarmList;
        public string Index = null;

        ImageView SetAlarm;
        RelativeLayout SetAlarmContainer;
        TextView SetAlarmText;
        NumberPicker AlarmHour;
        NumberPicker AlarmMin;
        TextView Dots;
        EditText AlarmName;
        CheckBox RemindMe;
        CheckBox EarthquakeAlert;
        CheckBox FireAlert;
        string WantedSleepTime = string.Empty;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Alarm_NewAlarm, null, false);

            SetAlarm = v.FindViewById<ImageView>(Resource.Id.imageView1);
            SetAlarmContainer = v.FindViewById<RelativeLayout>(Resource.Id.relativeLayout2);
            SetAlarmText = v.FindViewById<TextView>(Resource.Id.textView2);
            AlarmHour = v.FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            AlarmMin = v.FindViewById<NumberPicker>(Resource.Id.numberPicker2);
            Dots = v.FindViewById<TextView>(Resource.Id.textView1);
            AlarmName = v.FindViewById<EditText>(Resource.Id.editText1);
            RemindMe = v.FindViewById<CheckBox>(Resource.Id.checkBox1);
            EarthquakeAlert = v.FindViewById<CheckBox>(Resource.Id.checkBox3);
            FireAlert = v.FindViewById<CheckBox>(Resource.Id.checkBox2);

            AlarmMin.MaxValue = 59;
            AlarmMin.MinValue = 00;

            AlarmHour.MaxValue = 23;
            AlarmHour.MinValue = 00;

            if (FragmentShown == null)
            {
                AlarmHour.Value = System.DateTime.Now.Hour;
                AlarmMin.Value = System.DateTime.Now.Minute + 1;
            }

            TextView Text = (TextView)AlarmHour.GetChildAt(0);

            SetAlarm.Touch += SetAlarm_Touch;
            FragmentShown += Alarm_NewAlarm_FragmentShown;
            RemindMe.Click += RemindMe_Click;
            FireAlert.CheckedChange += FireAlert_CheckedChange;

            SetTypeface.Italic.SetTypeFace(Activity, SetAlarmText);
            SetTypeface.Normal.SetTypeFace(Activity, AlarmName);
            SetTypeface.Normal.SetTypeFace(Activity, RemindMe);
            SetTypeface.Normal.SetTypeFace(Activity, EarthquakeAlert);
            SetTypeface.Normal.SetTypeFace(Activity, FireAlert);

            return v;
        }

        private void FireAlert_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (FireAlert.Checked)
            {
                Dialog diag = new Dialog(Activity);
                diag.Window.RequestFeature(WindowFeatures.NoTitle);
                diag.SetCancelable(false);
                View v = Activity.LayoutInflater.Inflate(Resource.Layout.Alarm_NewAlarm_FireAlert, null, false);

                Button ok = v.FindViewById<Button>(Resource.Id.button1);
                ok.Click += (object sender1, EventArgs e1)=> {
                    diag.Cancel();
                };

                diag.SetContentView(v);
                diag.Show();
            }
        }

        void RemindMe_Click(object sender, EventArgs e)
        {
            Dialog diag = new Dialog(Activity);
            diag.Window.RequestFeature(WindowFeatures.NoTitle);

            View v = Activity.LayoutInflater.Inflate(Resource.Layout.Alarm_NewAlarm_RemindMe, null, false);

            Button OK = v.FindViewById<Button>(Resource.Id.button1);
            Button Anuleaza = v.FindViewById<Button>(Resource.Id.button2);
            TextView Titlu = v.FindViewById<TextView>(Resource.Id.textView1);
            ListView Intervale = v.FindViewById<ListView>(Resource.Id.listView1);
            EditText TimpDorit = v.FindViewById<EditText>(Resource.Id.editText1);

            string[] VarianteTimp = new string[] { "0 minute","5 minute", "10 minute", "15 minute", "30 minute", "60 minute" }; 

            Intervale.Adapter = new Alarm_NewAlarm_RemindMe_ArrayAdapter(Activity, VarianteTimp);

            OK.Click += (object sender1,EventArgs e1) => {
                RemindMe.Checked = true;
                WantedSleepTime = TimpDorit.Text + " " + VarianteTimp[((Alarm_NewAlarm_RemindMe_ArrayAdapter)Intervale.Adapter).Checked].Split(' ')[0];
                diag.Cancel();
            };

            Anuleaza.Click += (object sender1, EventArgs e1) => {
                RemindMe.Checked = false;
                WantedSleepTime = string.Empty;
                diag.Cancel();
            };

            diag.SetContentView (v);
            diag.Show();
        }

        void Alarm_NewAlarm_FragmentShown(object sender, EventArgs e)
        {
            if (Index == null)
            {
                AlarmHour.Value = System.DateTime.Now.Hour;
                AlarmMin.Value = System.DateTime.Now.Minute + 1;
            }
            else
            {
                AlarmObj ob = AlarmObj.GetAlarmAt(int.Parse(Index), Activity);

                AlarmHour.Value = int.Parse(ob.Hour);
                AlarmMin.Value = int.Parse(ob.Min);

                if (ob.Name != "Alarma")
                    AlarmName.Text = ob.Name;

                if (ob.Remind != "")
                    RemindMe.Checked = true;

                if (ob.EarthquakeAlert == "true")
                    EarthquakeAlert.Checked = true;

                if (ob.FireAlert == "true")
                    FireAlert.Checked = true;
            }
        }

        void SetAlarm_Touch(object sender, View.TouchEventArgs e)
        {
            SetAlarm.SetX(e.Event.RawX - SetAlarm.Width/2);

            float AlphaVal = 1f - 1f/(592f/SetAlarm.GetX());

            SetAlarm.Alpha = AlphaVal;
            SetAlarmContainer.Alpha = AlphaVal;
            SetAlarmText.Alpha = AlphaVal;
            AlarmHour.Alpha = AlphaVal;
            AlarmMin.Alpha = AlphaVal;
            Dots.Alpha = AlphaVal;
            AlarmName.Alpha = AlphaVal;
            RemindMe.Alpha = AlphaVal;
            FireAlert.Alpha = AlphaVal;
            EarthquakeAlert.Alpha = AlphaVal;

            if (e.Event.Action.Equals(MotionEventActions.Up))
            {
                if (SetAlarm.GetX() > 450f)
                {
                    SaveUsingSharedPreferences susp = new SaveUsingSharedPreferences(Activity);

                    if (Index == null)
                    {
                        string LastID = GetLastID();
                        string Hour = AlarmHour.Value.ToString();
                        string Min = AlarmMin.Value.ToString();
                        string Name = (AlarmName.Text != string.Empty) ? AlarmName.Text : "Alarma";

                        AlarmObj ao = new AlarmObj(Hour,Min,Name,WantedSleepTime,FireAlert.Checked.ToString(),EarthquakeAlert.Checked.ToString(),Activity);
                    }
                    else
                    {
                        string Hour = AlarmHour.Value.ToString();
                        string Min = AlarmMin.Value.ToString();
                        string Name = (AlarmName.Text != string.Empty) ? AlarmName.Text : "Alarma";

                        AlarmObj.GetAlarmAt(int.Parse(Index),Activity).Edit(new string [] {
                            Index,
                            Hour,
                            Min,
                            Name,
                            WantedSleepTime,
                            FireAlert.Checked.ToString(),
                            EarthquakeAlert.Checked.ToString()
                        } );

                        Index = null;
                    }

                    FragmentTransaction transact = FragmentManager.BeginTransaction();
                    transact.Hide(this);
                    transact.Show(AlarmList);
                    transact.Commit();

                    Toast.MakeText(Activity, "Alarma a fost creata cu succes!", ToastLength.Long).Show();
                }

                SetAlarm.SetX(0f);

                SetAlarm.Alpha = 1f;
                SetAlarmContainer.Alpha = 1f;
                SetAlarmText.Alpha = 1f;
                AlarmHour.Alpha = 1f;
                AlarmMin.Alpha = 1f;
                Dots.Alpha = 1f;
                AlarmName.Alpha = 1f;
                RemindMe.Alpha = 1f;
                FireAlert.Alpha = 1f;
                EarthquakeAlert.Alpha = 1f;
            }
        }
    
        string GetLastID()
        {
            List<string> AList = new SaveUsingSharedPreferences(Activity).LoadArray(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm).ToList();
            if (AList.Count == 0)
                return "0";
            else
                return AList[AList.Count-7];

        }
    }
}