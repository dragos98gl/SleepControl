package md5fe2bfb889fceceaf4704f6882091f21d;


public class BluetoothServer
	extends java.lang.Thread
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler\n" +
			"";
		mono.android.Runtime.register ("SleepControl.Scripts.BluetoothServer, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BluetoothServer.class, __md_methods);
	}


	public BluetoothServer ()
	{
		super ();
		if (getClass () == BluetoothServer.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.BluetoothServer, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public BluetoothServer (java.lang.Runnable p0)
	{
		super (p0);
		if (getClass () == BluetoothServer.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.BluetoothServer, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	public BluetoothServer (android.app.Activity p0)
	{
		super ();
		if (getClass () == BluetoothServer.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.BluetoothServer, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

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
