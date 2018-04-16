package md57460874f46b950ec6891c69347f0f947;


public class Alarm_Triggered
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("SleepControl.Scripts.Alarm.Alarm_Triggered, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Alarm_Triggered.class, __md_methods);
	}


	public Alarm_Triggered () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Alarm_Triggered.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm.Alarm_Triggered, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
