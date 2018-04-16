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
using Android.Hardware;

namespace SleepControl.Scripts.Alarm._safety
{
    public class EarthquakeAlartEventArgs : EventArgs { }
    class Alarm_EarthquakeAlert : Java.Lang.Object, ISensorEventListener
    {
        public delegate void EarthquakeAlertHandler(object sender,EarthquakeAlartEventArgs e);
        public event EarthquakeAlertHandler EarthquakeAlert;

        public virtual void OnEarthquakeAlert()
        {
            if (EarthquakeAlert != null)
                EarthquakeAlert(this,new EarthquakeAlartEventArgs());
        }

        float Sum;

        public Alarm_EarthquakeAlert(Context context)
        {
            SensorManager sManager = (SensorManager)context.GetSystemService(Context.SensorService);
            Sensor LinearAcceleration = sManager.GetDefaultSensor(SensorType.LinearAcceleration);

            sManager.RegisterListener(this,LinearAcceleration,SensorDelay.Fastest);
        }

        void ISensorEventListener.OnSensorChanged(SensorEvent e)
        {
            if (Sum < 0.5f)
            {
                Sum = Math.Abs(e.Values[0]) + Math.Abs(e.Values[1]) + Math.Abs(e.Values[2]);

                if (Sum>0.5f)
                    OnEarthquakeAlert();
            }
        }

        void ISensorEventListener.OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }
    }
}