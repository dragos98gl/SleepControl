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
using Android.Graphics;
using Android.Util;

namespace SleepControl.Scripts.Graphics
{
    public class Clock : View
    {
        public Clock(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            int h = System.DateTime.Now.Hour;
            int m = System.DateTime.Now.Minute;

            if (h >= 12)
                h -= 12;

            float angle = h * 30 + m / 2;

            RectF rect = new RectF();

            Paint White = new Paint();
            White.SetStyle(Paint.Style.Stroke);
            White.Color = Color.White;
            White.Alpha = 175;
            White.StrokeWidth = 10;

            rect.Set(5, 5, Width - 5, Height - 5);
            canvas.DrawArc(rect, 270, angle, false, White);

            Invalidate();
        }
    }
}