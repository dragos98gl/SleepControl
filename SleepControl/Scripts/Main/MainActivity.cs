using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Util;
using System.Timers;
using Android.Bluetooth;
using Java.Lang;
using SleepControl.Scripts;
using SleepControl.Scripts.Alarm;
using SleepControl.Scripts.Settings;
using SleepControl.Scripts.AlarmsChart;
using Android.Support.V4.Widget;
using SleepControl.Scripts.Diagram;

namespace SleepControl
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        ImageView Clock;
        LinearLayout Alarm;
        LinearLayout Diagram;
        LinearLayout Info;
        LinearLayout Settings;
        BluetoothServer Server;
        DrawerLayout DL;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            string StorageBackup = "Test-01_11:22_4.5.2017_63,63,63,63,63,62,63,62,63,62,63,63,62,63,63,62,63,64,64,62,63,63,62,66,61,62,62,62,63,63,61,63,63,61,63,64,64,65,67,71,69,64,68,68,66,67,67,70,71,74,78,71,67,68,67,65,66,67,68,66,67,67,68,66,67,65,68,67,67,68,66,67,67,65,65,64,64,64,64,64,65,66,66,66,65,65,65,66,66,66,66,64,66,65,67,66,66,65,66,67,67,68,66,69,63,63,63,63,63,66,65,67,65,63,65,66,65,65,65,65,63,65,65,65,64,64,62,68,63,63,63,64,65,62,67,66,66,65,63,67,67,71,75,62,60,58,59,59,59,59,59,59,60,60,63,59,60,60,58,59,60,58,60,60,59,60,58,60,59,60,59,59,60,60,60,60,59,60,59,59,60,61,58,59,58,59,59,60,62,60,58,59,59,58,60,59,58,60,59,59,60,59,58,58,57,57,59,61,59,60,61,60,66,63,62,61,61,61,61,61,61,61,60,61,60,60,60,60,60,60,60,59,60,61,61,59,61,61,59,59,59,60,59,59,59,59,59,59,59,59,59,60,58,60,60,59,59,59,59,60,58,58,59,59,58,58,61,58,59,56,57,60,61,61,62,62,@Test-2_5:21_5.5.2017_61,62,62,61,62,62,62,62,61,62,61,62,61,62,61,62,61,62,62,63,62,63,62,63,63,62,63,62,63,62,62,63,62,63,63,63,63,63,64,63,63,63,62,62,62,62,62,62,63,63,62,63,64,63,67,67,70,69,72,70,66,66,68,66,66,65,62,63,62,59,58,58,58,61,58,59,59,58,58,59,58,60,58,59,59,59,59,59,59,60,58,60,59,60,59,60,59,60,59,59,59,60,59,59,59,58,60,59,59,57,56,57,57,55,56,56,56,56,57,56,57,55,56,56,56,55,56,56,56,56,58,56,55,59,57,56,57,61,63,64,62,62,62,63,60,61,61,59,60,62,57,58,56,57,59,57,58,57,57,58,57,58,57,56,57,57,55,56,56,54,55,55,55,54,54,54,54,53,53,53,54,53,53,54,53,54,53,54,53,55,57,53,54,54,54,54,54,54,54,54,54,55,55,54,55,55,55,55,55,55,55,55,54,55,55,55,54,55,55,55,55,54,55,55,55,54,56,58,59,59,61,59,58,60,57,59,62,58,56,58,56,55,55,57,55,56,57,56,56,57,56,56,57,56,57,56,57,56,57,56,57,57,56,57,56,57,56,57,57,57,57,56,57,57,57,56,56,57,57,55,57,56,57,55,57,57,57,57,56,57,56,58,57,57,57,60,58,65,61,63,61,63,66,63,55,53,54,56,55,59,58,58,58,65,68,67,65,66,62,62,59,60,64,61,63,72,";
            //new SaveUsingSharedPreferences(this).Clear(SaveUsingSharedPreferences.Tags.Alarm.NewAlarm);
            new SaveUsingSharedPreferences(this).Clear(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram);
            new SaveUsingSharedPreferences(this).Save(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram, StorageBackup);

            DL = FindViewById<DrawerLayout>(Resource.Id.drawer);
            Clock = FindViewById<ImageView>(Resource.Id.imageView1);
            Alarm = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            Diagram = FindViewById<LinearLayout>(Resource.Id.linearLayout5);
            Info = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            Settings = FindViewById<LinearLayout>(Resource.Id.linearLayout7);

            DrawClock();

            Timer Refresh = new Timer();
            Refresh.Interval = 1000;
            Refresh.Start();

            Refresh.Elapsed += Refresh_Elapsed;
            Clock.Click += Clock_Click;
            Diagram.Click += Diagram_Click;
            Alarm.Click += Alarm_Click;
            Settings.Click += Settings_Click;
            Info.Click += Info_Click;
            DL.DrawerOpened += DL_DrawerOpened;
            DL.DrawerClosed += DL_DrawerClosed;
        }

        void Diagram_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Diagram));
        }

        void DL_DrawerClosed(object sender, DrawerLayout.DrawerClosedEventArgs e)
        {
            Clock.Clickable = true;
        }

        void DL_DrawerOpened(object sender, DrawerLayout.DrawerOpenedEventArgs e)
        {
            Clock.Clickable = false;
        }

        void Info_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AlarmsChart));
        }

        void Settings_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Settings));
        }

        void Alarm_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Alarm));
        }

        void Clock_Click(object sender, EventArgs e)
        {
            Intent NewAlarm = new Intent(this,typeof(Alarm));

            NewAlarm.PutExtra("LaunchType","NewAlarm");

            StartActivity(NewAlarm);
        }

        void Refresh_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isBluetoothOn())
            {
                if (Server == null)
                {
                  //  RunOnUiThread(() =>
                  //      Toast.MakeText(this, "Bluetooth on", ToastLength.Short).Show());

                 //   Server = new BluetoothServer(this);
                  //  Server.Start();
                }

                if (Server.ClientConnected)
                { 

                }
            }

            DrawClock();
        }

        void DrawClock()
        {
            Bitmap bitmap = Bitmap.CreateBitmap(300,300,Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);

            Paint Black = new Paint(PaintFlags.AntiAlias);
            Black.Color = Color.Black;
            Black.Alpha = 150;

            Paint White = new Paint(PaintFlags.AntiAlias);
            White.Color = Color.White;
            White.Alpha = 175;
            White.StrokeWidth = 5;
            White.TextSize = 100;

            canvas.DrawCircle(150,150,150,Black);
            canvas.DrawLine(150,225,150,75,White);

            string m=System.DateTime.Now.Minute.ToString();
            string h=System.DateTime.Now.Hour.ToString();

            if (int.Parse(m) < 10)
                m = "0" + m;

            if (int.Parse(h) < 10)
                h = "0" + h;

            canvas.DrawText(h,25,185,White);
            canvas.DrawText(m,165,185,White);

            Clock.SetImageBitmap(bitmap);

            bitmap.Dispose();
            canvas.Dispose();
            Black.Dispose();
            White.Dispose();
        }

        bool isBluetoothOn()
        {
            BluetoothAdapter bAdapter = BluetoothAdapter.DefaultAdapter;
            return bAdapter.IsEnabled;
        }
    }
}

