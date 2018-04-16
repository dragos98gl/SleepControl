using System;
using Android.Content;
using Android.App;
using System.Collections.Generic;
using Android.Preferences;
using System.Text.RegularExpressions;

namespace SleepControl
{
    public class SaveUsingSharedPreferences
    {
        Context AppContext;

        public SaveUsingSharedPreferences(Activity context)
        {
            this.AppContext = context.ApplicationContext;
        }

        public SaveUsingSharedPreferences(Context context)
        {
            AppContext = context.ApplicationContext;
        }

        public void SaveArray(string Tag, string[] L)
        {
            string Last = LoadString(Tag);
            string New = string.Empty;

            foreach (string s in L)
                New += "_" + s;

            if (Last.Length == 0)
                New=New.Remove(0, 1);
            
            Save(Tag, Last + New);
        }

        void ReplaceArray(string Tag, List<string> List)
        {
            string Array = string.Empty;

            foreach (string s in List)
                Array += "_" + s;

            if (Array != string.Empty)
                Save(Tag, Array.Remove(0, 1));
            else
                Save(Tag,Array);
        }

        public void DeleteAlarm(string Tag, int index)
        {
            List<string> ActualList = LoadArray(Tag);
            ActualList.RemoveRange(index*7, 7);

            ReplaceArray(Tag, ActualList);
        }

        public void EditArray(string Tag, int ID, string[] L)
        {
            List<string> CurrentData = LoadArray(Tag);

            for (int i = 1; i < L.Length; i++)
                CurrentData[ID * 7 + i] = L[i];

            string New = string.Empty;

            foreach (string s in CurrentData)
                New += "_" + s;

            Save(Tag, New.Remove(0, 1));
        }

        public List<string> LoadArray(string Tag)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(AppContext);
            string Collection = prefs.GetString(Tag, string.Empty);

            return new List<string>((Regex.Split(Collection, "_").Length == 1) ? new string[0] : Regex.Split(Collection, "_"));
        }

        public void Save(string Tag, string Value)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(AppContext).Edit();
            editor.PutString(Tag, Value);
            editor.Apply();
        }

        public string LoadString(string Tag)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(AppContext);
            return prefs.GetString(Tag, string.Empty);
        }

        public void ClearAll()
        {
        }

        public void Clear(string Tag)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(AppContext).Edit();
            editor.Remove(Tag);
            editor.Apply();
        }

        public class Tags
        {
            public static class Settings
            {
                public const string AlarmRingTone = "Distanta";
            }

            public static class Alarm
            {
                public const string NewAlarm = "NewAlarm";
            }
                  
            public static class Diagram
            {
                public const string NewDiagram = "Diagram";
            }
        }
    }
}

