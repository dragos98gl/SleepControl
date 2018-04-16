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

namespace SleepControl
{
    [BroadcastReceiver] ///VERY IMP
    class AlarmObj_Notification_BroadcastReceiver:BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            NotificationManager nManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            Notification n = (Notification)intent.GetParcelableExtra("NOTIFICAITON");
            nManager.Notify(0,n);
        }
    }
}