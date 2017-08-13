using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Support.V4.App;
using Android.Widget;
using GMX;
using Android.Support.V7.Graphics.Drawable;

[assembly: ExportRenderer(typeof(GMX.Controls.MyMasterDetail), typeof(MyMasterDetailRenderer))]
public class MyMasterDetailRenderer : MasterDetailPageRenderer
{
	protected override void OnLayout(bool changed, int l, int t, int r, int b)
	{
		base.OnLayout(changed, l, t, r, b);
		var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
		for (var i = 0; i < toolbar.ChildCount; i++)
		{
			var imageButton = toolbar.GetChildAt(i) as ImageButton;

			var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
			if (drawerArrow == null)
				continue;

            imageButton.SetImageDrawable(Forms.Context.GetDrawable(Resource.Drawable.slideout));
		}
	}
}