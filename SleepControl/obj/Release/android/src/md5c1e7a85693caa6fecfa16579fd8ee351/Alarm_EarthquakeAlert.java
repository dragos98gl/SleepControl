package md5c1e7a85693caa6fecfa16579fd8ee351;


public class Alarm_EarthquakeAlert
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.SensorEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAccuracyChanged:(Landroid/hardware/Sensor;I)V:GetOnAccuracyChanged_Landroid_hardware_Sensor_IHandler:Android.Hardware.ISensorEventListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onSensorChanged:(Landroid/hardware/SensorEvent;)V:GetOnSensorChanged_Landroid_hardware_SensorEvent_Handler:Android.Hardware.ISensorEventListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("SleepControl.Scripts.Alarm._safety.Alarm_EarthquakeAlert, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Alarm_EarthquakeAlert.class, __md_methods);
	}


	public Alarm_EarthquakeAlert ()
	{
		super ();
		if (getClass () == Alarm_EarthquakeAlert.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm._safety.Alarm_EarthquakeAlert, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public Alarm_EarthquakeAlert (android.content.Context p0)
	{
		super ();
		if (getClass () == Alarm_EarthquakeAlert.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm._safety.Alarm_EarthquakeAlert, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onAccuracyChanged (android.hardware.Sensor p0, int p1)
	{
		n_onAccuracyChanged (p0, p1);
	}

	private native void n_onAccuracyChanged (android.hardware.Sensor p0, int p1);


	public void onSensorChanged (android.hardware.SensorEvent p0)
	{
		n_onSensorChanged (p0);
	}

	private native void n_onSensorChanged (android.hardware.SensorEvent p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}