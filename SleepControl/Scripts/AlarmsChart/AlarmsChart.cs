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
using SleepControl.Scripts.Graphics;
using System.IO;
using Java.Lang;
using Android.Bluetooth;

namespace SleepControl.Scripts.AlarmsChart
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class AlarmsChart : Activity
    {
        List<float> f = new List<float>() { 0,0};
        SleepChart Chart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SleepChart);

            Chart = FindViewById<SleepChart>(Resource.Id.sleepChart1);
            Chart.vals = f.ToArray();

            foreach (BluetoothDevice d in BluetoothAdapter.DefaultAdapter.BondedDevices)
                if (d.Address == Constants.ArduinoBluetoohMAC)
                {
                    BluetoothSocket s = d.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(Constants.ServerUUID));
                    s.Connect();

                    ClientHandle(s);
                }
        }

        public bool ClientConnected = false;
        void ClientHandle(BluetoothSocket Client)
        {
            new Thread(() => {
                ClientConnected = true;

                Thread.Sleep(5000);

                byte[] Buffer = Encoding.ASCII.GetBytes("1");
                Client.OutputStream.Write(Buffer, 0, Buffer.Length);


                Buffer = new byte[1];
                string Packet = string.Empty;

                Random r = new Random();

                while (true)
                {
                    int PacketInt = Client.InputStream.Read(Buffer, 0, Buffer.Length);
                    string OneStringPacket = Encoding.ASCII.GetString(Buffer, 0, PacketInt);

                    if (OneStringPacket == "!")
                    {
                        try
                        {
                            AddNewVal(ref f, float.Parse(Packet));
                            Chart.vals = f.ToArray();
                            RunOnUiThread(() => Chart.Invalidate());
                        }
                        catch { }
                        Packet = string.Empty;
                    }
                    else
                        Packet += OneStringPacket;
                }
            }).Start();
        }

        void AddNewVal(ref List<float> f, float val)
        {
            if (f.Count > 320)
            {
                for (int i = 0; i < f.Count - 1; i++)
                {
                    float temp = f[i];
                    f[i] = f[i + 1];
                    f[i + 1] = temp;
                }

                f[0] = val;
            }
            else
                f.Add(val);
        }
    }
}