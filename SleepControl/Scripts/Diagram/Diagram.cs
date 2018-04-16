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

namespace SleepControl.Scripts.Diagram
{
   [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class Diagram : Activity
    {
       List<Diagrama> Diagrame = new List<Diagrama>();
       Spinner Alarme;
       PulseDiagram PulseDiagram;
       Button DeleteAlarm;

       protected override void OnCreate(Bundle savedInstanceState)
       {
           RequestWindowFeature(WindowFeatures.NoTitle);
           Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

           base.OnCreate(savedInstanceState);

           SetContentView(Resource.Layout.Diagram);

           Alarme = FindViewById<Spinner>(Resource.Id.spinner1);
           PulseDiagram = FindViewById<PulseDiagram>(Resource.Id.pulseDiagram1);
           DeleteAlarm = FindViewById<Button>(Resource.Id.button1);

           string Storage = new SaveUsingSharedPreferences(this).LoadString(SaveUsingSharedPreferences.Tags.Diagram.NewDiagram);

           if (Storage != "")
           {
               foreach (string a in Storage.Split('@'))
               {
                   string[] b = a.Split('_');

                   Diagrama d = new Diagrama();
                   d.Name = b[0];
                   d.Time = b[1];
                   d.Date = b[2];

                   List<float> p = new List<float>();

                   foreach (string c in b[3].Split(','))
                       try { p.Add(float.Parse(c)); }
                       catch { }

                   d.Puls = p.ToArray();

                   Diagrame.Add(d);
               }

               Alarme.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, Diagrame.Select(s => s.Name + " " + s.Time + " " + s.Date).ToArray());
               PulseDiagram.vals = Diagrame.Select(s => s.Puls).ToArray()[Alarme.SelectedItemPosition];

               Alarme.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
               {
                   PulseDiagram.vals = Diagrame.Select(s => s.Puls).ToArray()[Alarme.SelectedItemPosition];
                   PulseDiagram.Invalidate();
               };
           }
       }

       struct Diagrama
       {
           public string Name;
           public string Time;
           public string Date;
           public float[] Puls;
       }
    }
}