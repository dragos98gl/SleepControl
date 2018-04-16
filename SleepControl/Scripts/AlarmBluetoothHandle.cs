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
using SleepControl.Scripts.Alarm;
using Android.Bluetooth;
using System.Threading;
using System.IO;

namespace SleepControl.Scripts
{
    class AlarmBluetoothHandle
    {
        AlarmObj Alarm;
        Dialog diag;
        BluetoothSocket Client;

        public AlarmBluetoothHandle(AlarmObj Alarm,Dialog diag)
        {
            this.Alarm = Alarm;
            this.diag = diag;

            foreach (BluetoothDevice d in BluetoothAdapter.DefaultAdapter.BondedDevices)
                if (d.Address == Constants.ArduinoBluetoohMAC)
                {
                    Client = d.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(Constants.ServerUUID));
                    Client.Connect();
                }

            Alarm.SyncAlarm(this);
        }

        public IEnumerable<float> ClientHandle()
        {
            diag.Cancel();

            Thread.Sleep(5000);
            CreateNewDiagram();

            byte[] Buffer = Encoding.ASCII.GetBytes("1");
            Client.OutputStream.Write(Buffer, 0, Buffer.Length);


            Buffer = new byte[1];
            string Packet = string.Empty; 

            while (true)
            {
                int PacketInt = Client.InputStream.Read(Buffer, 0, Buffer.Length);
                string OneStringPacket = Encoding.ASCII.GetString(Buffer, 0, PacketInt);

                if (OneStringPacket == "!")
                {
                    try
                    {
                        SaveToDiagram(Packet);

                        Save(Packet.ToString());

                        yield return float.Parse(Packet);
                    }
                    finally { }

                    Packet = string.Empty;
                }
                else
                    Packet += OneStringPacket;
            }
        }

        void Save(string Line)
        {
            string current;

            string path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "/SleepControl_Time.txt";

            FileStream ReadStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader r = new StreamReader(ReadStream);
            current = r.ReadToEnd();
            r.Close();
            ReadStream.Close();

            FileStream WriteStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter w = new StreamWriter(WriteStream);
            w.WriteLine(current + Line);
            w.Close();
            WriteStream.Close();
        }

        void CreateNewDiagram()
        {
            string Storage = new SaveUsingSharedPreferences(Alarm.context).LoadString(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram);
            string Time = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
            string Date = System.DateTime.Now.Day + "." + System.DateTime.Now.Month + "." + System.DateTime.Now.Year;

            if (Storage != string.Empty)
                Storage += Storage + "@" + Alarm.Name + "_" + Time + "_" + Date + "_";
            else
                Storage = Alarm.Name + "_" + Time + "_" + Date + "_";

            new SaveUsingSharedPreferences(Alarm.context).Save(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram, Storage);
        }

        void SaveToDiagram(string Packet)
        {
            string Storage = new SaveUsingSharedPreferences(Alarm.context).LoadString(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram);
            
            Storage += Packet + ",";

            new SaveUsingSharedPreferences(Alarm.context).Save(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram, Storage);
        }
    }
}