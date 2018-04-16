package md555b39b37a4c7573561b90f4eea09ca83;


public class Settings
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
		mono.android.Runtime.register ("SleepControl.Scripts.Settings.Settings, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Settings.class, __md_methods);
	}


	public Settings () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Settings.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Settings.Settings, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
