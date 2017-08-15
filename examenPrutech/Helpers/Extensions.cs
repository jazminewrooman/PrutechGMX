using Xamarin.Forms;
using System;
using System.Reflection;

public static class Extensions
{
    public static void UpdateLayout(this View view)
    {
        if (view == null)
        {
            return;
        }

        var method = typeof(View).GetMethod("InvalidateMeasure", BindingFlags.Instance | BindingFlags.NonPublic);

        method.Invoke(view, null);
    }
}