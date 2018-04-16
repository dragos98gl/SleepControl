namespace SleepControl.Scripts.Alarm
{
    using Android.App;
    using Android.Content;
    using Android.Views;
    using Android.Widget;
    using SleepControl;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class Alarm_AlarmsList_ArrayAdapter : ArrayAdapter<SleepControl.Scripts.Alarm.AlarmObj>
    {
        private Activity context;
        private Fragment Current;
        private List<SleepControl.Scripts.Alarm.AlarmObj> ListOfLists;
        private Alarm_NewAlarm NewAlarm;

        public Alarm_AlarmsList_ArrayAdapter(Activity context, List<AlarmObj> ListOfLists, Alarm_NewAlarm NewAlarm, Fragment Current)
            : base(context, Resource.Layout.Alarm_AlarmsList_Adapter, ListOfLists)
        {
            this.context = context;
            this.ListOfLists = ListOfLists;
            this.NewAlarm = NewAlarm;
            this.Current = Current;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = context.LayoutInflater.Inflate(Resource.Layout.Alarm_AlarmsList_Adapter,null,false);

            RelativeLayout container = v.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            TextView NumeAlarma = v.FindViewById<TextView>(Resource.Id.textView1);
            TextView Ora = v.FindViewById<TextView>(Resource.Id.textView2);
            TextView Min = v.FindViewById<TextView>(Resource.Id.textView4);
            ImageButton SyncAlarm = v.FindViewById<ImageButton>(Resource.Id.imageButton1);

            SetTypeface.Normal.SetTypeFace(context, NumeAlarma);
            SetTypeface.Normal.SetTypeFace(context, Ora);
            SetTypeface.Normal.SetTypeFace(context, Min);

            AlarmObj Alarm = ListOfLists[position];

            NumeAlarma.Text = Alarm.Name;
            Ora.Text = Alarm.Hour;
            Min.Text = Alarm.Min;

            SyncAlarm.Click += (object sender, EventArgs e) => {
                AlertDialog.Builder ad = new AlertDialog.Builder(context);
                ad.SetTitle("Doriti sa sincronizati alarma cu bratara bluetooth?");
                
                ad.SetPositiveButton("Da", (object sender1, DialogClickEventArgs e1) => {
                    Dialog BluetoothSync = new Dialog(context);
                    BluetoothSync.Window.RequestFeature(WindowFeatures.NoTitle);
                    BluetoothSync.SetCancelable(false);

                    View CostumView = context.LayoutInflater.Inflate(Resource.Layout.AlarmBluetoothHandle_UserConnect, null);
                    BluetoothSync.SetContentView(CostumView);

                    BluetoothSync.Show();

                    new Thread(() => new AlarmBluetoothHandle(Alarm, BluetoothSync)).Start();
                });
                
                ad.SetNegativeButton("Nu", (object sender1, DialogClickEventArgs e1) => {
                });

                ad.Show();
            };

            container.LongClick += (object sender, View.LongClickEventArgs e) => {
                PopupMenu menu = new PopupMenu(context,v);
                menu.MenuItemClick += (object sender1, PopupMenu.MenuItemClickEventArgs e1) => {
                    switch (e1.Item.ToString())
                    {
                        case "Sterge alarma": {
                            AlarmObj.Delete(position,context);
                            ((ListView)parent).Adapter = new Alarm_AlarmsList_ArrayAdapter(context,AlarmObj.GetAllAlarms(context),NewAlarm,Current);
                        } break;

                        case "Editeaza alarma": {
                            FragmentTransaction transaction = context.FragmentManager.BeginTransaction();
                            
                            transaction.Hide(this.Current);
                            transaction.Show(this.NewAlarm);
                            
                            NewAlarm.Index = position.ToString();
                            NewAlarm.OnFragmentShown();
                            
                            transaction.Commit();

                        } break;
                    }
                };
                menu.MenuInflater.Inflate(Resource.Menu.OptiuniAlarma,menu.Menu);
                menu.Show();
            };

            return v;
        }
    }
}