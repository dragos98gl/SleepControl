package md57460874f46b950ec6891c69347f0f947;


public class Alarm_AlarmsList_ArrayAdapter
	extends android.widget.ArrayAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"";
		mono.android.Runtime.register ("SleepControl.Scripts.Alarm.Alarm_AlarmsList_ArrayAdapter, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Alarm_AlarmsList_ArrayAdapter.class, __md_methods);
	}


	public Alarm_AlarmsList_ArrayAdapter (android.content.Context p0, int p1, java.lang.Object[] p2)
	{
		super (p0, p1, p2);
		if (getClass () == Alarm_AlarmsList_ArrayAdapter.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm.Alarm_AlarmsList_ArrayAdapter, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:T[], Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public Alarm_AlarmsList_ArrayAdapter (android.content.Context p0, int p1, java.util.List p2)
	{
		super (p0, p1, p2);
		if (getClass () == Alarm_AlarmsList_ArrayAdapter.class)
			mono.android.TypeManager.Activate ("SleepControl.Scripts.Alarm.Alarm_AlarmsList_ArrayAdapter, SleepControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Collections.Generic.IList`1<T>, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);

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
