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
using Android.Util;
using Android.Graphics;

namespace SleepControl.Scripts.Graphics
{
    class SleepChart:View
    {
        public delegate void ValsChangedEventArgsHandler(object sender, EventArgs e);
        public event ValsChangedEventArgsHandler ValsChanged;

        public virtual void OnValsChanged()
        {
            if (ValsChanged != null)
                ValsChanged(this, EventArgs.Empty);
        }



        public float[] vals =null;

        public SleepChart(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.ValsChanged += SleepChart_ValsChanged;
        }

        void SleepChart_ValsChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);

            Paint White = new Paint();
            White.Color = Color.White;

            float min=100;
            float max=0;

            float xOffset = Width / (vals.Length - 1);

            if (vals != null)
            {

                for (int i = 0; i < vals.Length; i++)
                {
                    if (min > vals[i])
                        min = vals[i];

                    if (max < vals[i])
                        max = vals[i];
                }

                max++;

                for (int i = 1; i < vals.Length; i++)
                {
                    float startX = xOffset * (i - 1);
                    float stopX = xOffset * i;

                    float startY = Height / ((max - min) / (max - vals[i - 1]));
                    float stopY = Height / ((max - min) / (max - vals[i]));

                    canvas.DrawLine(startX, startY, stopX, stopY, White);
                }
            }
            else 
                canvas.DrawLine(0,Height/2,Width,Height/2,White);
        }
    }
}