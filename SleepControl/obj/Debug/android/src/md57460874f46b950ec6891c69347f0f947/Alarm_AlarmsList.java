package md57460874f46b950ec6891c69347f0f947;


public class Alarm_AlarmsList
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onHiddenChanged:(Z)V:GetOnHiddenChanged_ZHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("SleepControl.Scripts.Alarm.Alarm_AlarmsList, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Alarm_AlarmsList.class, __md_methods);
	}


	public Alarm_AlarmsList () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Alarm_AlarmsList.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm.Alarm_AlarmsList, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public Alarm_AlarmsList (md57460874f46b950ec6891c69347f0f947.Alarm_NewAlarm p0, android.app.Activity p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == Alarm_AlarmsList.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm.Alarm_AlarmsList, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "SleepControl.Scripts.Alarm.Alarm_NewAlarm, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


	public void onHiddenChanged (boolean p0)
	{
		n_onHiddenChanged (p0);
	}

	private native void n_onHiddenChanged (boolean p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
