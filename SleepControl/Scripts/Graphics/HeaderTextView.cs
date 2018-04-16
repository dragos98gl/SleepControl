using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SleepControl.Scripts.Graphics
{
    class HeaderTextView:TextView
    {
        public HeaderTextView(Context context,Android.Util.IAttributeSet attrs):base(context,attrs)
        {
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);

            Paint Black = new Paint();
            Black.Color = Color.Black;
            Black.StrokeWidth = 10;

            canvas.DrawLine(0,Height,Width,Height,Black);
        }
    }
}