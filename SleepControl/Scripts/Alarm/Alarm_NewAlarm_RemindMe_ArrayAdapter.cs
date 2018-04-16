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
    class Alarm_NewAlarm_RemindMe_ArrayAdapter : ArrayAdapter<string>
    {
        public int Checked;
        private Activity context;
        private string[] Intervale;

        public Alarm_NewAlarm_RemindMe_ArrayAdapter(Activity context, string[] Intervale)
            : base(context, Resource.Layout.Alarm_NewAlarm_RemindMe_Adapter, Intervale)
        {
            this.Checked = 0;
            this.context = context;
            this.Intervale = Intervale;
        }

        public Alarm_NewAlarm_RemindMe_ArrayAdapter(Activity context, string[] Intervale, int Checked)
            : base(context, Resource.Layout.Alarm_NewAlarm_RemindMe_Adapter, Intervale)
        {
            this.Checked = 0;
            this.context = context;
            this.Intervale = Intervale;
            this.Checked = Checked;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = context.LayoutInflater.Inflate(Resource.Layout.Alarm_NewAlarm_RemindMe_Adapter, null, false);
            
            RadioButton button = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
            button.Text = this.Intervale[position];
            
            if (position == Checked)
                button.Checked = true;

            button.CheckedChange += (object sender, CompoundButton.CheckedChangeEventArgs e)=>{
                ((ListView)parent).Adapter = new Alarm_NewAlarm_RemindMe_ArrayAdapter(context,Intervale,position);
            };
            
            return view;
        }
    }
}