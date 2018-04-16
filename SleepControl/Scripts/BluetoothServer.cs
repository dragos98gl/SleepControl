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
using Java.Lang;
using Android.Bluetooth;
using System.IO;
using Java.IO;

namespace SleepControl.Scripts
{
    class BluetoothServer:Thread
    {
        public bool ClientConnected = false;
        
        Activity context;

        public BluetoothServer(Activity context)
        {
            this.context = context;
        }

        public override void Run()
        {

            foreach (BluetoothDevice d in BluetoothAdapter.DefaultAdapter.BondedDevices)
                if (d.Address == Constants.ArduinoBluetoohMAC)
                {
                    BluetoothSocket s = d.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(Constants.ServerUUID));
                    s.Connect();
                    ClientHandle(s);
                }
        }

        void ClientHandle(BluetoothSocket Client)
        {
            ClientConnected = true;

            Thread.Sleep (5000);

            byte[] Buffer = Encoding.ASCII.GetBytes("1");
            Client.OutputStream.Write(Buffer, 0, Buffer.Length);


            Buffer = new byte[1];
            string Packet = string.Empty;

            while (true)
            {
                int PacketInt = Client.InputStream.Read(Buffer, 0, Buffer.Length);
                string OneStringPacket = Encoding.ASCII.GetString(Buffer,0,PacketInt);

                if (OneStringPacket == "!")
                {
                    System.Console.WriteLine(Packet);
                    Save();
                    Save(Packet);

                    Packet = string.Empty;
                }
                else
                    Packet += OneStringPacket;
            }
        }

        void Save()
        {
            string current;

            string path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "/SleepControl.txt";

            FileStream ReadStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader r = new StreamReader(ReadStream);
            current = r.ReadToEnd();
            r.Close();
            ReadStream.Close();

            FileStream WriteStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter w = new StreamWriter(WriteStream);
            w.WriteLine(current + System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString());
            w.Close();
            WriteStream.Close();
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
    }
}